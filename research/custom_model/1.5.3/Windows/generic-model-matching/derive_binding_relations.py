"""Derive relation/ambiguity evidence from the completed audit, without rescanning assets."""
import json
import sys
from collections import defaultdict
from pathlib import Path

sys.stdout.reconfigure(encoding="utf-8")
ROOT = Path(__file__).resolve().parents[2]
AUDIT = ROOT / "artifacts/world-binding-20261003/existing-renderer-mesh-name-audit.json"
data = json.loads(AUDIT.read_text(encoding="utf-8-sig"))
rows = data["renderer_rows"]

def project(row):
    return {key: row[key] for key in (
        "character_id", "root", "path", "scope", "renderer_name", "mesh_name", "mesh_id", "renderer_id", "catalog_component"
    )}

by_scoped_mesh = defaultdict(list)
by_scoped_name = defaultdict(list)
by_role_lod0_mesh = defaultdict(list)
visible_by_root_mesh = defaultdict(list)
for row in rows:
    by_scoped_mesh[(row["character_id"], row["root"], row["scope"], row["mesh_id"])].append(row)
    by_scoped_name[(row["character_id"], row["root"], row["scope"], row["mesh_name"])].append(row)
    if row["scope"] == "lod0":
        by_role_lod0_mesh[(row["character_id"], row["mesh_id"])].append(row)
    if row["scope"] in ("lod0", "lod1", "lod2", "lod3"):
        visible_by_root_mesh[(row["character_id"], row["root"], row["mesh_id"])].append(row)

same_mesh_ambiguities = [group for group in by_scoped_mesh.values() if len(group) > 1]
same_name_ambiguities = [group for group in by_scoped_name.values() if len(group) > 1]
lod0_primary_ambiguities = [group for group in same_mesh_ambiguities if group[0]["scope"] == "lod0"]
lod0_pairs = []
lod0_unpaired = []
for group in by_role_lod0_mesh.values():
    ui = [row for row in group if row["root"].endswith("_uimodel")]
    world = [row for row in group if row["root"].endswith("_postmodel")]
    if len(ui) == len(world) == 1:
        lod0_pairs.append({"ui": project(ui[0]), "world": project(world[0])})
    else:
        lod0_unpaired.append([project(row) for row in group])

proxy_relations = defaultdict(list)
for proxy in rows:
    if proxy["scope"] != "shadow-proxy":
        continue
    owners = visible_by_root_mesh[(proxy["character_id"], proxy["root"], proxy["mesh_id"])]
    category = "one_visible_renderer" if len(owners) == 1 else "multiple_visible_renderers" if owners else "no_visible_renderer"
    proxy_relations[category].append({"proxy": project(proxy), "visible": [project(row) for row in owners]})

result = {
    "purpose": "New relation and ambiguity derivation only; completed name audit is reused unchanged.",
    "input": str(AUDIT),
    "platform": "Windows offline snapshot",
    "manifest_versions": sorted({source["manifest_version"] for source in data["sources"]}),
    "summary": {
        "same_mesh_multiple_renderer_groups_within_role_root_scope": len(same_mesh_ambiguities),
        "same_mesh_multiple_renderer_lod0_groups": len(lod0_primary_ambiguities),
        "same_mesh_name_multiple_renderer_groups_within_role_root_scope": len(same_name_ambiguities),
        "unique_ui_world_lod0_same_mesh_id_pairs": len(lod0_pairs),
        "lod0_mesh_id_groups_without_unique_ui_world_pair": len(lod0_unpaired),
        "proxy_direct_mesh_reference_counts": {key: len(value) for key, value in sorted(proxy_relations.items())},
    },
    "same_mesh_ambiguities": [[project(row) for row in group] for group in same_mesh_ambiguities],
    "same_name_ambiguities": [[project(row) for row in group] for group in same_name_ambiguities],
    "lod0_unpaired": lod0_unpaired,
    "proxy_examples": {key: value[:2] for key, value in sorted(proxy_relations.items())},
    "limits": [
        "Same Mesh reference pairs prove a shared offline asset, not bone/material/space equivalence.",
        "No Android asset names or cross-LOD ownership is inferred from Windows references.",
        "No suffix deletion, cross-LOD name rewrite or first-match selection is used.",
    ],
}
output = Path(__file__).with_name("binding-relation-evidence.json")
output.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
print(json.dumps(result["summary"], ensure_ascii=False, indent=2))
for category, examples in result["proxy_examples"].items():
    print(category, json.dumps(examples[:1], ensure_ascii=False))
print("OUTPUT", output)
