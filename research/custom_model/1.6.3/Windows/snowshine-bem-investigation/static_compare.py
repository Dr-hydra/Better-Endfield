"""Compare existing and targeted native Snowshine graphs without artifact hashes."""
import argparse
import importlib.util
import json
from collections import Counter
from pathlib import Path

WORKSPACE = Path(__file__).resolve().parents[5]
OLD = WORKSPACE / "research/custom_model/1.5.3/shared/postmodel-native-parser-history/sweep-identities/chr_0014_aurora.json"
CATALOG = WORKSPACE / "tools/CustomModel/catalog/chr_0014_aurora.json"


def read(path):
    return json.loads(Path(path).read_text(encoding="utf-8-sig"))


def parser():
    spec = importlib.util.spec_from_file_location("snowshine_native_parser", WORKSPACE / "tools/CustomModel/parse_native_models.py")
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    return module


def renderer_rows(raw, parsed):
    raw_objects = {obj["id"]: obj for obj in raw["objects"]}
    result = {}
    for renderer in parsed["renderers"]:
        if renderer.get("resource_root") not in ("chr_0014_aurora_postmodel", "chr_0014_aurora_uimodel"):
            continue
        path = renderer["path"]
        if path in result:
            raise ValueError("Ambiguous renderer path: " + path)
        mesh = parsed["meshes"][renderer["mesh_id"]]
        root = renderer["resource_root"] + "/"
        result[path] = {
            "renderer_name": renderer["name"],
            "renderer_kind": renderer["renderer_kind"],
            "mesh_name": mesh["name"],
            "vertex_count": mesh["vertex_count"],
            "index_count": mesh["original_index_count"],
            "submeshes": mesh["submeshes"],
            "bones": [b["path"].removeprefix(root) for b in renderer["bones"]],
            "bone_names": [b["name"] for b in renderer["bones"]],
            "bindposes": mesh["bindposes"],
            "bone_name_hashes": mesh.get("bone_name_hashes"),
            "serialized_layout": mesh["serialized_layout"],
            # Original game index-buffer identities supplied by NativeAssetReader;
            # the comparison itself computes no hash of inputs or outputs.
            "source_index_identity": raw_objects[renderer["mesh_id"]].get("resource_identity"),
            "materials": [m.get("name") for m in renderer["materials"]],
            "material_texture_names": [[(t["property"], t.get("name")) for t in m["textures"]] for m in renderer["materials"]],
            "shader_asset_paths": [m.get("shader", {}).get("asset_paths", []) for m in renderer["materials"]],
            "offline_references_complete": renderer["offline_references_complete"],
        }
    return result


def compact(row):
    result = {k: row[k] for k in ("renderer_name", "mesh_name", "vertex_count", "index_count", "materials")}
    result["serialized_storage_strides"] = [stream["aligned_storage_stride"] for stream in row["serialized_layout"]["streams"]]
    return result


def pose_delta_by_bone_name(before, after):
    if len(set(before["bone_names"])) != len(before["bone_names"]) or set(before["bone_names"]) != set(after["bone_names"]):
        return None
    old = dict(zip(before["bone_names"], before["bindposes"]))
    new = dict(zip(after["bone_names"], after["bindposes"]))
    return max((abs(x - y) for name in old for x, y in zip(old[name], new[name])), default=0)


def main():
    args = argparse.ArgumentParser()
    args.add_argument("current_graph", type=Path)
    args.add_argument("--output", type=Path, default=Path(__file__).with_name("static_comparison.json"))
    options = args.parse_args()
    old, new, catalog = read(OLD), read(options.current_graph), read(CATALOG)
    native = parser()
    old_db, new_db = native.parse(old), native.parse(new)
    before, after = renderer_rows(old, old_db), renderer_rows(new, new_db)
    changed = []
    field_counts = Counter()
    for path in sorted(before.keys() & after.keys()):
        fields = [field for field in before[path] if before[path][field] != after[path][field]
                  and not (field == "source_index_identity" and (before[path][field] is None or after[path][field] is None))]
        if fields:
            field_counts.update(fields)
            old_row, new_row = before[path], after[path]
            old_bound = [[entry for entry in material if entry[1] is not None] for material in old_row["material_texture_names"]]
            new_bound = [[entry for entry in material if entry[1] is not None] for material in new_row["material_texture_names"]]
            changed.append({"path": path, "fields": fields, "before": compact(old_row), "after": compact(new_row),
                            "serialized_layout_details": {"old": old_row["serialized_layout"], "current": new_row["serialized_layout"]}
                                                         if "serialized_layout" in fields else None,
                            "bindpose_max_abs_delta_after_bone_name_alignment": pose_delta_by_bone_name(old_row, new_row),
                            "bound_texture_names_changed": old_bound != new_bound,
                            "bone_order_changes": [{"slot": slot, "old": x, "current": y}
                                                   for slot, (x, y) in enumerate(zip(old_row["bone_names"], new_row["bone_names"])) if x != y],
                            "bone_names_added": sorted(set(new_row["bone_names"]) - set(old_row["bone_names"])),
                            "bone_names_removed": sorted(set(old_row["bone_names"]) - set(new_row["bone_names"]))})
    components = []
    for component, entry in catalog["components"].items():
        old_matching = [path for path, row in before.items() if row["mesh_name"] == entry["mesh_name"] and "/lod0/" in path]
        paths = {path: {"mesh_name": after[path]["mesh_name"], "vertex_count": after[path]["vertex_count"], "index_count": after[path]["index_count"],
                       "mesh_name_matches_catalog": after[path]["mesh_name"] == entry["mesh_name"],
                       "index_count_matches_catalog": after[path]["index_count"] == entry["original_index_count"],
                       "bone_names_match_catalog": after[path]["bone_names"] == entry["bone_names"],
                       "materials_match_catalog": after[path]["materials"] == entry["materials"]}
                 for path in old_matching if path in after}
        components.append({"component": component, "catalog_mesh": entry["mesh_name"], "catalog_vertex_count": entry["vertex_count"],
                           "catalog_index_count": entry["original_index_count"], "current_renderers": paths})
    old_bundles = {b["path"]: b for b in old["snapshot"]["bundles"]}
    new_bundles = {b["path"]: b for b in new["snapshot"]["bundles"]}
    result = {
        "schema": 1, "character": "chr_0014_aurora", "old_graph": str(OLD.relative_to(WORKSPACE)),
        "current_graph": str(options.current_graph.resolve().relative_to(WORKSPACE)), "catalog": str(CATALOG.relative_to(WORKSPACE)),
        "catalog_revision": catalog["revision"], "catalog_source_snapshot": catalog["source_snapshot"],
        "old_summary": old_db["summary"], "current_summary": new_db["summary"],
        "old_backend_issues": old.get("backend_errors", []), "current_backend_issues": new.get("backend_errors", []),
        "old_manifest_version": old["snapshot"]["manifest_version"], "current_manifest_version": new["snapshot"]["manifest_version"],
        "old_prefab_assets": old["snapshot"]["assets"], "current_prefab_assets": new["snapshot"]["assets"],
        "bundle_paths_added": sorted(new_bundles.keys() - old_bundles.keys()), "bundle_paths_removed": sorted(old_bundles.keys() - new_bundles.keys()),
        "bundle_sizes_changed": [{"path": path, "old": old_bundles[path]["size"], "current": new_bundles[path]["size"]}
                                 for path in sorted(old_bundles.keys() & new_bundles.keys()) if old_bundles[path]["size"] != new_bundles[path]["size"]],
        "renderer_paths_added": sorted(after.keys() - before.keys()), "renderer_paths_removed": sorted(before.keys() - after.keys()),
        "matched_renderer_count": len(before.keys() & after.keys()), "changed_renderer_count": len(changed),
        "all_matched_renderer_names_unchanged": all(before[path]["renderer_name"] == after[path]["renderer_name"] for path in before.keys() & after.keys()),
        "all_matched_mesh_names_unchanged": all(before[path]["mesh_name"] == after[path]["mesh_name"] for path in before.keys() & after.keys()),
        "max_bindpose_abs_delta_after_bone_name_alignment": max((pose_delta_by_bone_name(before[path], after[path]) or 0)
                                                               for path in before.keys() & after.keys()),
        "bound_texture_name_changes": sum(entry["bound_texture_names_changed"] for entry in changed),
        "source_index_identity_comparable_count": sum(before[path]["source_index_identity"] is not None and after[path]["source_index_identity"] is not None
                                                      for path in before.keys() & after.keys()),
        "changed_field_counts": dict(field_counts), "changed_renderers": changed, "catalog_components": components,
        "boundaries": ["Serialized layouts are not current runtime vertex-declaration observations.",
                       "Native metadata does not include all vertex payloads; unchanged counts do not prove unchanged positions.",
                       "Original source index identities are compared only if supplied; no generated-artifact hash is calculated.",
                       "Resource names and index counts are checked separately from serialized CAB/path IDs."]}
    options.output.write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding="utf-8")
    print(json.dumps({k: result[k] for k in ("old_summary", "current_summary", "matched_renderer_count", "changed_renderer_count", "changed_field_counts", "renderer_paths_added", "renderer_paths_removed")}, ensure_ascii=False))


if __name__ == "__main__":
    main()
