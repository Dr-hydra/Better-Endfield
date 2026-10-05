"""Summarize locally extracted metadata; no game writes or artifact hashes."""
import collections
import json
import sys
from pathlib import Path

HERE = Path(__file__).resolve().parent
REPO = next(p for p in HERE.parents if (p / "tools/CustomModel/parse_native_models.py").is_file())
sys.path.insert(0, str(REPO / "tools/CustomModel"))
from parse_native_models import Graph, parse
from prepare_native_profile import paired_resource


def main():
    raw = json.loads((HERE / "native-extended.json").read_text(encoding="utf-8-sig"))
    graph, database = Graph(raw), parse(raw)
    roots = {}
    for root in database["resources"]:
        rows = [r for r in database["renderers"] if r["resource_root"] == root]
        primary = [r for r in rows if r["path"].removeprefix(root + "/").startswith("Mesh_all/lod0/")]
        roots[root] = {
            "skinned_renderers": len(rows),
            "references_complete": sum(r["offline_references_complete"] for r in rows),
            "lod0_components": [
                {"mesh": r["mesh_name"], "indices": r["original_index_count"],
                 "bones": len(r["bones"]), "materials": [m["name"] for m in r["materials"]],
                 "runtime_layout_observed": r["runtime_layout"] is not None}
                for r in sorted(primary, key=lambda r: r["mesh_name"])
            ],
        }
    static = []
    for obj in raw["objects"]:
        if obj["type"] != "MeshRenderer":
            continue
        go = graph.get(obj["game_object"], "GameObject")
        filters = [graph.objects[p["id"]] for p in go["components"]
                   if p.get("id") in graph.objects and graph.objects[p["id"]]["type"] == "MeshFilter"]
        if len(filters) != 1:
            raise ValueError("Static receiver has no unique MeshFilter")
        mesh = graph.get(filters[0]["mesh"], "Mesh")
        static.append({
            "path": graph.path(graph.transforms[go["id"]])["path"], "mesh": mesh["name"],
            "vertices": mesh["vertex_count"], "indices": sum(s["index_count"] for s in mesh["submeshes"]),
            "bindposes": len(mesh["bindposes"]),
            "channels": [{k: c[k] for k in ("attribute", "stream", "format", "dimension_raw")}
                         for c in mesh["serialized_channels"] if c["dimension_raw"]],
            "materials": [graph.get(p, "Material")["name"] for p in obj["materials"]],
        })
    actor, entity = "chr_0030_zhuangfy_ult_postmodel", "abilityentity_chr_0030_zhuangfy_ult_postmodel"
    pairs = [{r["mesh_name"]: r for r in database["renderers"]
              if r["resource_root"] == root and "/lod0/" in r["path"]} for root in (actor, entity)]
    assert pairs[0].keys() == pairs[1].keys()
    pair_errors = {name: paired_resource(row, pairs[1][name], database["meshes"])
                   for name, row in pairs[0].items()}
    model_table = json.loads((REPO / "inputs/1.5.3/Windows/json/GameplayConfig/ModelTable.json").read_text(encoding="utf-8-sig"))["data"]
    result = {
        "schema": 1,
        "scope": "Windows offline metadata; no game/device runtime verification",
        "manifest_version": raw["snapshot"]["manifest_version"],
        "selected_assets": [a["path"] for a in raw["snapshot"]["assets"]],
        "bundle_count": len(raw["snapshot"]["bundles"]),
        "missing_bundles": raw["snapshot"]["missing"],
        "object_types": dict(collections.Counter(o["type"] for o in raw["objects"])),
        "backend_issue_first_lines": [e.splitlines()[0] for e in raw["backend_errors"]],
        "skinned_parse_summary": database["summary"],
        "roots_without_skinned_renderers": database["missing_resources"],
        "resources": roots, "static_renderers": sorted(static, key=lambda r: r["path"]),
        "ultimate_actor_entity_pair": {"component_count": len(pairs[0]),
                                       "errors": {k: v for k, v in pair_errors.items() if v}},
        "model_table": {k: v for k, v in model_table.items() if "zhuangfy" in k},
        "artifact_hashes_computed": False,
    }
    (HERE / "summary.json").write_text(json.dumps(result, indent=2, ensure_ascii=False) + "\n", encoding="utf-8")
    print(json.dumps({"resources": len(roots), "skinned_renderers": database["summary"]["renderers"],
                      "static_renderers": len(static), "paired_components": len(pairs[0]),
                      "pair_errors": result["ultimate_actor_entity_pair"]["errors"]}, ensure_ascii=False))


if __name__ == "__main__":
    main()
