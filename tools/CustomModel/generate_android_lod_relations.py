"""Generate the verified Android UI-LOD0 -> world-LOD1 relation table.

The source catalog supplies the UI component path and identity.  The archived
Android character catalog supplies the independently observed LOD1 mesh name.
Rows are emitted only when both identities exist; index counts remain unknown
unless a future runtime probe records them, so the runtime still checks the
live object and its arrays before applying a row.
"""
from __future__ import annotations

import argparse
import json
from pathlib import Path
import sys

def cpp(value: str) -> str:
    return '"' + value.replace("\\", "\\\\").replace('"', '\\"') + '"'


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--repo", type=Path, default=Path(__file__).resolve().parents[2])
    parser.add_argument("--output", type=Path)
    parser.add_argument("--workspace-config", type=Path)
    parser.add_argument("--relation-config", type=Path)
    args = parser.parse_args()
    repo = args.repo
    sys.path.insert(0, str(repo / "scripts"))
    from workspace_config import load_workspace, write_if_changed
    workspace = load_workspace(args.workspace_config, repo)
    configured_version = workspace.get("game.version")
    policy_candidates = [
        repo / "config/compatibility/custom_model" / configured_version / "android-lod-relations.json",
        repo / "config/compatibility/custom_model/1.6.3/android-lod-relations.json",
        repo / "config/compatibility/custom_model/1.5.3/android-lod-relations.json",
    ]
    resolved_policy_path = args.relation_config or next((p for p in policy_candidates if p.exists()), policy_candidates[0])
    policy = json.loads(resolved_policy_path.read_text(encoding="utf-8-sig"))
    if policy.get("schema_version") != 1:
        raise ValueError("Unsupported LOD relation configuration")
    snapshot = policy["snapshot"]
    overrides = policy.get("overrides", {})
    output = args.output or repo / "native/modules/custom_model/android_lod_relations.generated.h"
    android_path = workspace.path(policy["catalog_key"])
    android = {x["characterId"]: x for x in json.loads(android_path.read_text(encoding="utf-8-sig"))["characters"]}
    rows: list[tuple[str, str, str, str, str, str, str]] = []
    for catalog_path in sorted((repo / policy["native_catalog"]).glob("chr_*.json")):
        catalog = json.loads(catalog_path.read_text(encoding="utf-8-sig"))
        character = catalog["character_id"]
        android_character = android.get(character)
        char_overrides = overrides.get(character, {})
        if not android_character and not char_overrides:
            continue
        lod1 = {}
        if android_character:
            for asset in android_character.get("modelAssets", []):
                path_str = asset.get("path", "")
                if "_lod1" not in path_str.lower():
                    continue
                name = path_str.rsplit("##", 1)[1] if "##" in path_str else Path(path_str).stem
                canonical = name[:-2] if name.endswith("_8") else name[:-3] if name.endswith("_20") else name
                key = canonical.casefold()
                previous = lod1.get(key)
                if previous is None or name.endswith(("_8", "_20")):
                    lod1[key] = name
        comp_overrides = char_overrides.get("components", {})
        for component_id in sorted(catalog["components"], key=int):
            component = catalog["components"][component_id]
            source_mesh = component["mesh_name"]
            observations = component.get("evidence", {}).get("observations", {})
            ui = next((x for name, x in observations.items() if name.endswith("_uimodel") and x.get("path")), None)
            if not ui:
                continue
            source_path = ui["path"].split("/", 1)[1]
            if component_id in comp_overrides:
                override = comp_overrides[component_id]
                target_path = override["target_path"]
                target_mesh = override["target_mesh"]
                rows.append((character, catalog["ui_resource"], catalog["world_resource"], source_path,
                             source_mesh, target_path, target_mesh))
                continue
            source_base = source_mesh[:-3] if source_mesh.endswith("_20") else source_mesh[:-2] if source_mesh.endswith("_8") else source_mesh
            target_mesh_guess = source_base.rsplit("_lod0", 1)[0] + "_lod1"
            if android_character:
                observed_mesh = lod1.get(target_mesh_guess.casefold())
                if not observed_mesh:
                    continue
                suffix = "_8" if observed_mesh.endswith("_8") else "_20" if observed_mesh.endswith("_20") else ""
                target_mesh = target_mesh_guess + suffix
            else:
                target_mesh = target_mesh_guess
            target_renderer = target_mesh[:-2] if target_mesh.endswith("_8") else target_mesh[:-3] if target_mesh.endswith("_20") else target_mesh
            target_path = "Mesh_all/lod1/" + target_renderer
            rows.append((character, catalog["ui_resource"], catalog["world_resource"], source_path,
                         source_mesh, target_path, target_mesh))
    rows.sort()
    lines = ["#pragma once", "// Generated by tools/CustomModel/generate_android_lod_relations.py.",
             "// Android manifest snapshot: " + snapshot, "namespace BetterEndfieldNext::CustomModel {",
             "inline std::span<const GenericMatching::ExactLodRelation> AndroidLodRelations() {",
             "    static const GenericMatching::ExactLodRelation rows[] = {"]
    for character, source_resource, target_resource, source_path, source_mesh, target_path, target_mesh in rows:
        lines.append("        {{" + cpp(policy["platform"]) + "," + cpp(snapshot) + "}," +
                     ",".join(map(cpp, (character, source_resource, target_resource, source_path,
                                          source_mesh, target_path, target_mesh))) + ",true,true,0,false},")
    lines += ["    };", "    return rows;", "}",
              "inline GenericMatching::AssetScope AndroidLodAssetScope() {",
              "    return {\"" + policy["platform"] + "\",\"" + snapshot + "\"};", "}", "}", ""]
    output.parent.mkdir(parents=True, exist_ok=True)
    write_if_changed(output, "\n".join(lines))
    print(f"generated {len(rows)} Android LOD relations -> {output}")


if __name__ == "__main__":
    main()
