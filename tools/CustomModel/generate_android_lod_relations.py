"""Generate the verified Android UI-LOD0 -> world-LOD1 relation table.

Two evidence sources feed this table, and the second one wins when both exist:

* ``android-lod1-index.json`` — renderer identities read from a character's own
  Android prefab (``tools/CustomModel/build_android_lod1_index.py``).  Built per
  character, so the table only grows evidence where someone looked.
* the pre-1.5.3 Windows catalog — the historical source, covering 32 characters.

Characters in neither source emit no row.  There is deliberately no fallback
that appends ``_lod1`` to the UI mesh stem: that guess is what produced verified
looking rows for characters whose Android fur meshes carry a variant suffix
(``S_actor_purrchena_fur_01_lod1_11``).

Rows are only emitted when both identities exist; index counts stay unknown
unless a prefab was parsed *and* ``--emit-index-counts`` is given, because a
known count turns the runtime index-count check on.
"""
from __future__ import annotations

import argparse
import json
from pathlib import Path
import sys
from typing import Any


def cpp(value: str) -> str:
    return '"' + value.replace("\\", "\\\\").replace('"', '\\"') + '"'


def strip_variant_suffix(name: str) -> str:
    """Drop the Android mesh variant suffix (``_8`` / ``_20``) from a mesh name."""
    if name.endswith("_20"):
        return name[:-3]
    if name.endswith("_8"):
        return name[:-2]
    return name


def index_by_renderer(renderers: dict[str, dict]) -> dict[str, tuple[str, dict]]:
    """Map a renderer leaf name to its full path, preferring ``Mesh_all/lod1``."""
    result: dict[str, tuple[str, dict]] = {}
    for path, entry in renderers.items():
        leaf = path.rsplit("/", 1)[-1].casefold()
        current = result.get(leaf)
        preferred = path.lower().startswith("mesh_all/lod1/")
        if current is None or (preferred and not current[0].lower().startswith("mesh_all/lod1/")):
            result[leaf] = (path, entry)
    return result


def observed_lod1_names(android_character: dict | None) -> dict[str, str]:
    """Index the archived Android catalog's LOD1 mesh names by canonical stem."""
    names: dict[str, str] = {}
    for asset in (android_character or {}).get("modelAssets", []):
        path_str = asset.get("path", "")
        if "_lod1" not in path_str.lower():
            continue
        name = path_str.rsplit("##", 1)[1] if "##" in path_str else Path(path_str).stem
        key = strip_variant_suffix(name).casefold()
        previous = names.get(key)
        if previous is None or name.endswith(("_8", "_20")):
            names[key] = name
    return names


def build_rows(policy: dict[str, Any], catalog_dir: Path, android_catalog: dict[str, dict],
               indexed_characters: dict[str, dict], emit_index_counts: bool = False
               ) -> tuple[list[dict], list[str]]:
    """Return the relation rows plus a list of components that had no evidence.

    A component is only turned into a row when its LOD1 identity is known: either
    read from that character's prefab, or observed in the archived catalog.  The
    caller renders whatever comes back and reports the dropped components.
    """
    overrides = policy.get("overrides", {})
    rows: list[dict] = []
    dropped: list[str] = []
    for catalog_path in sorted(catalog_dir.glob("chr_*.json")):
        catalog = json.loads(catalog_path.read_text(encoding="utf-8-sig"))
        character = catalog["character_id"]
        android_character = android_catalog.get(character)
        char_overrides = overrides.get(character, {})
        indexed = indexed_characters.get(character, {}).get("renderers", {})
        if not android_character and not char_overrides and not indexed:
            continue
        lod1 = observed_lod1_names(android_character)
        by_renderer = index_by_renderer(indexed)
        comp_overrides = char_overrides.get("components", {})
        for component_id in sorted(catalog["components"], key=int):
            component = catalog["components"][component_id]
            source_mesh = component["mesh_name"]
            observations = component.get("evidence", {}).get("observations", {})
            ui = next((x for name, x in observations.items() if name.endswith("_uimodel") and x.get("path")), None)
            if not ui:
                continue
            source_path = ui["path"].split("/", 1)[1]
            common = dict(character=character, source_resource=catalog["ui_resource"],
                          target_resource=catalog["world_resource"], source_path=source_path,
                          source_mesh=source_mesh)
            if component_id in comp_overrides:
                override = comp_overrides[component_id]
                rows.append(dict(common, target_path=override["target_path"],
                                 target_mesh=override["target_mesh"], indices=0,
                                 indices_known=False, confirmed=True, evidence="override"))
                continue
            source_base = strip_variant_suffix(source_mesh)
            target_renderer = source_base.rsplit("_lod0", 1)[0] + "_lod1"
            if indexed:
                hit = by_renderer.get(target_renderer.casefold())
                if hit is None:
                    dropped.append(f"{character} {component_id} {source_mesh}: no LOD1 renderer in prefab")
                    continue
                target_path, entry = hit
                indices = int(entry.get("indices") or 0) if emit_index_counts else 0
                rows.append(dict(common, target_path=target_path, target_mesh=entry["mesh"],
                                 indices=indices, indices_known=indices > 0, confirmed=True,
                                 evidence="prefab"))
                continue
            observed_mesh = lod1.get(target_renderer.casefold())
            if not observed_mesh:
                dropped.append(f"{character} {component_id} {source_mesh}: no evidence, no row emitted")
                continue
            suffix = "_8" if observed_mesh.endswith("_8") else "_20" if observed_mesh.endswith("_20") else ""
            rows.append(dict(common, target_path="Mesh_all/lod1/" + target_renderer,
                             target_mesh=target_renderer + suffix, indices=0,
                             indices_known=False, confirmed=True, evidence="catalog"))
    rows.sort(key=lambda r: (r["character"], r["source_resource"], r["target_resource"], r["source_path"],
                             r["source_mesh"], r["target_path"], r["target_mesh"]))
    return rows, dropped


def render_header(rows: list[dict], policy: dict[str, Any]) -> str:
    snapshot = policy["snapshot"]
    lines = ["#pragma once", "// Generated by tools/CustomModel/generate_android_lod_relations.py.",
             "// Android manifest snapshot: " + snapshot, "namespace BetterEndfieldNext::CustomModel {",
             "inline std::span<const GenericMatching::ExactLodRelation> AndroidLodRelations() {",
             "    static const GenericMatching::ExactLodRelation rows[] = {"]
    for row in rows:
        flags = "true,true" if row["confirmed"] else "false,false"
        lines.append("        {{" + cpp(policy["platform"]) + "," + cpp(snapshot) + "}," +
                     ",".join(map(cpp, (row["character"], row["source_resource"], row["target_resource"],
                                        row["source_path"], row["source_mesh"], row["target_path"],
                                        row["target_mesh"]))) +
                     "," + flags + "," + str(row["indices"]) + "," +
                     ("true" if row["indices_known"] else "false") + "},")
    lines += ["    };", "    return rows;", "}",
              "inline GenericMatching::AssetScope AndroidLodAssetScope() {",
              "    return {\"" + policy["platform"] + "\",\"" + snapshot + "\"};", "}", "}", ""]
    return "\n".join(lines)


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--repo", type=Path, default=Path(__file__).resolve().parents[2])
    parser.add_argument("--output", type=Path)
    parser.add_argument("--workspace-config", type=Path)
    parser.add_argument("--relation-config", type=Path)
    parser.add_argument("--lod1-index", type=Path, help="Override the per-character prefab index")
    parser.add_argument("--emit-index-counts", action="store_true",
                        help="Write parsed index counts into the table.  Off by default: a known count "
                             "turns the runtime index-count check on, and that has not been validated "
                             "against a live device yet.")
    args = parser.parse_args()
    repo = args.repo
    sys.path.insert(0, str(repo / "scripts"))
    from workspace_config import load_workspace, write_if_changed
    workspace = load_workspace(args.workspace_config, repo)
    configured_version = workspace.get("game.version")
    policy_candidates = [
        repo / "config/compatibility/custom_model" / str(configured_version) / "android-lod-relations.json",
        repo / "config/compatibility/custom_model/1.6.3/android-lod-relations.json",
        repo / "config/compatibility/custom_model/1.5.3/android-lod-relations.json",
    ]
    resolved_policy_path = args.relation_config or next((p for p in policy_candidates if p.exists()), policy_candidates[0])
    policy = json.loads(resolved_policy_path.read_text(encoding="utf-8-sig"))
    if policy.get("schema_version") != 1:
        raise ValueError("Unsupported LOD relation configuration")
    output = args.output or repo / "native/modules/custom_model/android_lod_relations.generated.h"
    android_path = workspace.path(policy["catalog_key"])
    android = {x["characterId"]: x for x in json.loads(android_path.read_text(encoding="utf-8-sig"))["characters"]}

    index_path = args.lod1_index or (repo / policy["lod1_index"] if policy.get("lod1_index") else None)
    indexed_characters: dict = {}
    if index_path is not None and index_path.exists():
        index_file = json.loads(index_path.read_text(encoding="utf-8-sig"))
        if index_file.get("schema_version") != 1:
            raise ValueError("Unsupported LOD1 index schema")
        if index_file.get("snapshot", policy["snapshot"]) != policy["snapshot"]:
            raise ValueError(f"LOD1 index snapshot {index_file.get('snapshot')} does not match "
                             f"policy {policy['snapshot']}")
        indexed_characters = index_file.get("characters", {})

    rows, dropped = build_rows(policy, repo / policy["native_catalog"], android,
                               indexed_characters, args.emit_index_counts)
    output.parent.mkdir(parents=True, exist_ok=True)
    write_if_changed(output, render_header(rows, policy))
    print(f"generated {len(rows)} Android LOD relations -> {output}")
    for label, source in (("prefab-derived", "prefab"), ("catalog-derived", "catalog"), ("override", "override")):
        count = sum(1 for row in rows if row["evidence"] == source)
        if count:
            print(f"  {label}: {count}")
    print(f"  characters with prefab evidence: {len(indexed_characters)}"
          f"; index counts active: {sum(1 for row in rows if row['indices_known'])}")
    if dropped:
        print(f"  dropped {len(dropped)} component(s) without evidence:")
        for item in dropped[:20]:
            print("    " + item)
        if len(dropped) > 20:
            print(f"    ... and {len(dropped) - 20} more")


if __name__ == "__main__":
    main()
