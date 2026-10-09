"""Generate actor-relative FP data from existing object graphs, never bundles.

Only the actor's primary Root/Bip001 skeleton and actual visible Mesh_all paths
are included. Foreign dependency roots, nested soldiers/props and shadow proxies
are excluded. Palette order is deliberately absent from generated data.
"""
from __future__ import annotations

import argparse
import json
import re
from pathlib import Path

REPO = Path(__file__).resolve().parents[2]
SPINE2 = "Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2"
NECK = SPINE2 + "/Bip001_Neck"
HEAD = NECK + "/Bip001_Head"
TAIL = re.compile(r"^(tail(?:$|_)|bip001[_ ]tail(?:$|_))", re.I)
HAT = re.compile(r"^(maozi(?:_|[a-z0-9])|hat(?:$|_)|brim(?:$|_))", re.I)
PART = re.compile(r"_(head|face|hair|fur|hat|maozi|brim)_", re.I)
LOD = re.compile(r"(?:^|_)lod(\d+)(?:_|$)", re.I)


def read_json(path):
    return json.loads(path.read_text(encoding="utf-8-sig"))


def graph_paths(data):
    objects = {obj["id"]: obj for obj in data["objects"]}
    if len(objects) != len(data["objects"]):
        raise ValueError("duplicate object identities")
    transforms = {key: obj for key, obj in objects.items() if obj["type"] == "Transform"}
    paths, active = {}, set()

    def path(key):
        if key in paths:
            return paths[key]
        if key in active:
            raise ValueError("cyclic Transform graph")
        active.add(key)
        transform = transforms[key]
        go = objects[transform["game_object"]["id"]]
        name = go["name"]
        if not name or "/" in name or "\\" in name or name in (".", ".."):
            raise ValueError(f"unsafe GameObject name: {name!r}")
        parent = transform.get("parent", {})
        parent_id = parent.get("id")
        if parent_id in transforms:
            value = path(parent_id) + "/" + name
        elif parent.get("path_id", "0") not in ("0", 0):
            raise ValueError(f"unresolved Transform parent: {key}")
        else:
            value = name
        active.remove(key)
        paths[key] = value
        return value

    for key in transforms:
        path(key)
    return objects, transforms, paths


def below(path, root):
    return path == root or path.startswith(root + "/")


def minimal_roots(paths):
    result = []
    for path in sorted(set(paths), key=lambda value: (value.count("/"), value)):
        if not any(below(path, root) for root in result):
            result.append(path)
    return sorted(result)


def native_profile(native_path, catalog_root):
    data = read_json(native_path)
    model = data["snapshot"]["character"]
    if model != native_path.parent.name:
        raise ValueError(f"source/model mismatch: {native_path}")
    bundles = data["snapshot"]["bundles"]
    if not bundles or any(not item["path"].startswith("Bundles/Windows/") for item in bundles):
        raise ValueError("platform must be proven by actual snapshot bundle paths")
    if data["snapshot"].get("missing"):
        raise ValueError(f"incomplete source: {model}")
    objects, transforms, paths = graph_paths(data)
    catalog_name = {"chr_0003_endminf": "endminf-pc.json",
                    "chr_0004_pelica": "pelica-pc.json"}.get(model, model + ".json")
    catalog = read_json(catalog_root / catalog_name)
    sources = []
    actor_roots = {model + "_postmodel": "world", model + "_uimodel": "ui"}
    for root, view in actor_roots.items():
        actor = {key: path[len(root) + 1:] for key, path in paths.items()
                 if path.startswith(root + "/")}
        if not actor:
            raise ValueError(f"missing own model root: {root}")
        skeleton = {key: path for key, path in actor.items() if below(path, "Root/Bip001")}
        if HEAD not in skeleton.values() or NECK not in skeleton.values():
            raise ValueError(f"missing verified primary Head/Neck chain: {root}")
        duplicates = sorted({path for path in skeleton.values() if list(skeleton.values()).count(path) > 1})
        hats = minimal_roots(path for path in skeleton.values()
                             if below(path, SPINE2) and not below(path, HEAD)
                             and HAT.match(path.rsplit("/", 1)[-1]))
        tails = minimal_roots(path for path in skeleton.values()
                              if not below(path, HEAD) and TAIL.match(path.rsplit("/", 1)[-1]))
        if set(duplicates) & {"Root/Bip001", HEAD, NECK, *hats, *tails}:
            raise ValueError(f"ambiguous semantic root: {root}")
        bones = [dict(path="Root/Bip001", semantic="Preserve", subtree=True, evidence="GraphVerified", region="Body"),
                 dict(path=NECK, semantic="Neck", subtree=False, evidence="GraphVerified", region="Neck"),
                 dict(path=HEAD, semantic="Hide", subtree=True, evidence="GraphVerified", region="Head")]
        bones += [dict(path=path, semantic="Hide", subtree=True,
                       evidence="NamePathInferred" if path in hats else "GraphVerified",
                       region="HeadAccessory" if path in hats else "Tail")
                  for path in sorted(set(hats + tails))]
        local_rules, lods, renderer_count = [], set(), 0
        for renderer in objects.values():
            if renderer["type"] != "SkinnedMeshRenderer":
                continue
            go = objects[renderer["game_object"]["id"]]
            transform_ids = [ref["id"] for ref in go["components"] if ref.get("id") in transforms]
            if len(transform_ids) != 1:
                raise ValueError("ambiguous renderer Transform")
            relative = actor.get(transform_ids[0], "")
            if not relative.startswith("Mesh_all/"):
                continue
            renderer_count += 1
            mesh = objects.get(renderer.get("mesh", {}).get("id"))
            if not mesh or mesh["type"] != "Mesh":
                raise ValueError(f"unresolved visible Mesh: {root}/{relative}")
            lod = LOD.search(mesh["name"])
            if not lod:
                continue  # Never invent a LOD from a missing declaration.
            lod = int(lod[1])
            if lod >= 32:
                raise ValueError("LOD outside representable mask")
            lods.add(lod)
            part = PART.search(mesh["name"])
            accessory_palette = any(gen_path and any(below(gen_path, hat) for hat in hats)
                                    for gen_path in (skeleton.get(ref.get("id"), "")
                                                     for ref in renderer["bones"]))
            if not part and not accessory_palette:
                continue
            # Scope is a named/path-inferred policy. Current per-vertex weights
            # AND spatial bounds are mandatory even for already analyzed meshes.
            scope = "HeadAccessory" if not part else {
                "hat": "HeadAccessory", "maozi": "HeadAccessory", "brim": "HeadAccessory"}.get(
                    part[1].lower(), part[1].capitalize())
            local_rules.append(dict(renderer_path=relative, original_mesh_name=mesh["name"],
                                    lod=lod, scope=scope, evidence="NamePathInferred",
                                    min_head_weight=1.0 / 65535 if scope in ("Fur", "HeadAccessory") else 0.0,
                                    evidence_source=f"identities/roles/{model}/native.json"))
        if len({rule["renderer_path"] for rule in local_rules}) != len(local_rules):
            raise ValueError(f"ambiguous local scope: {root}")
        sources.append(dict(model_root=root, platform="windows-x64", view=view,
                            revision=data["snapshot"]["manifest_version"],
                            source=f"identities/roles/{model}/native.json",
                            head_path=HEAD, neck_path=NECK, lod_mask=sum(1 << lod for lod in lods),
                            graph_verified=True, bone_rules=bones,
                            local_rules=sorted(local_rules, key=lambda rule: rule["renderer_path"]),
                            skeleton_transform_count=len(skeleton), visible_renderer_count=renderer_count,
                            duplicate_nonsemantic_transform_paths=duplicates,
                            additional_hat_roots=hats, tail_roots=tails))
    return dict(model_id=model, display_name=catalog["name"], sources=sources,
                source_warnings=[warning.splitlines()[0] for warning in data.get("backend_errors", [])],
                excluded_dependency_roots=sorted(set(path for path in paths.values() if "/" not in path)
                                                 - set(actor_roots)))


def supplemental_purrche(catalog_root):
    # Not one of the 32 complete native graphs. Its existing catalog supplies
    # direct world/UI LOD0 scope evidence; do not fabricate bone relative paths.
    data = read_json(catalog_root / "chr_0038_purrche.json")
    model = data["character_id"]
    sources = []
    for root, view in ((data["world_resource"], "world"), (data["ui_resource"], "ui")):
        local_rules = []
        for component in data["components"].values():
            name = component["mesh_name"]
            part = PART.search(name)
            observation = component["evidence"]["observations"].get(root)
            if not part or not observation or observation["kind"] != "direct":
                continue
            path = observation["path"]
            if not path.startswith(root + "/Mesh_all/"):
                raise ValueError("foreign supplemental renderer")
            lod = LOD.search(name)
            if not lod:
                continue
            verified_fur03 = name == "S_actor_purrchena_fur_03_lod0_20"
            scope = part[1].capitalize()
            local_rules.append(dict(renderer_path=path[len(root) + 1:], original_mesh_name=name,
                                    lod=int(lod[1]), scope=scope,
                                    evidence="DrawWeightsVerified" if verified_fur03 else "NamePathInferred",
                                    min_head_weight=1.0 / 65535 if scope == "Fur" else 0.0,
                                    evidence_source="docs/camera/research/1.5.3/first-person-geometry/BEM_HAIR_BONE_ANALYSIS.md" if verified_fur03
                                    else "tools/CustomModel/catalog/chr_0038_purrche.json"))
        sources.append(dict(model_root=root, platform=data["platform"], view=view,
                            revision=data["source_snapshot"]["manifest_version"],
                            source="tools/CustomModel/catalog/chr_0038_purrche.json",
                            head_path="", neck_path="", lod_mask=sum(1 << lod for lod in
                            {rule["lod"] for rule in local_rules}), graph_verified=False,
                            bone_rules=[], local_rules=sorted(local_rules, key=lambda rule: rule["renderer_path"])))
    return dict(model_id=model, display_name=data["name"], sources=sources,
                coverage_note="Supplemental direct LOD0 scopes only; no complete native bone graph.")


def cpp(data):
    quote = lambda value: json.dumps(value, ensure_ascii=False)
    lines = ["// Generated by tools/FirstPersonProfiles/generate_profiles.py. Do not edit.",
             "namespace Generated {"]
    for pi, profile in enumerate(data["profiles"]):
        for si, source in enumerate(profile["sources"]):
            prefix = f"p{pi}_s{si}"
            if source["bone_rules"]:
                lines.append(f"inline constexpr BoneRule {prefix}_bones[] = {{")
                for rule in source["bone_rules"]:
                    lines.append("    {" + quote(rule["path"]) + ", BoneSemantic::" + rule["semantic"] +
                                 ", " + str(rule["subtree"]).lower() + ", Evidence::" + rule["evidence"] +
                                 ", Region::" + rule["region"] + "},")
                lines.append("};")
            if source["local_rules"]:
                lines.append(f"inline constexpr LocalRule {prefix}_local[] = {{")
                for rule in source["local_rules"]:
                    lines.append("    {" + quote(rule["renderer_path"]) + ", " + quote(rule["original_mesh_name"]) +
                                 f", {rule['lod']}, Scope::{rule['scope']}, Evidence::{rule['evidence']}, 0.5, " +
                                 repr(rule["min_head_weight"]) + ", -0.5, 4.0, 4.0, " + quote(rule["evidence_source"]) + "},")
                lines.append("};")
        lines.append(f"inline constexpr Source p{pi}_sources[] = {{")
        for si, source in enumerate(profile["sources"]):
            prefix = f"p{pi}_s{si}"
            bones = prefix + "_bones" if source["bone_rules"] else "nullptr"
            local = prefix + "_local" if source["local_rules"] else "nullptr"
            values = [quote(source[key]) for key in
                      ("model_root", "platform", "view", "revision", "source", "head_path", "neck_path")]
            lines.append("    {" + ", ".join(values) + f", {source['lod_mask']}u, " +
                         str(source["graph_verified"]).lower() + f", {bones}, {len(source['bone_rules'])}, " +
                         f"{local}, {len(source['local_rules'])}" + "},")
        lines.append("};")
    lines.append("inline constexpr Profile kProfiles[] = {")
    for pi, profile in enumerate(data["profiles"]):
        lines.append("    {" + quote(profile["model_id"]) + ", " + quote(profile["display_name"]) +
                     f", p{pi}_sources, {len(profile['sources'])}" + "},")
    lines += ["};", "} // namespace Generated", ""]
    return "\n".join(lines)


def generate(roles_root, catalog_root):
    paths = sorted(roles_root.glob("*/native.json"))
    if len(paths) != 32:
        raise ValueError(f"expected 32 reviewed native graphs, found {len(paths)}")
    profiles = [native_profile(path, catalog_root) for path in paths]
    profiles.append(supplemental_purrche(catalog_root))
    if len({profile["model_id"] for profile in profiles}) != len(profiles):
        raise ValueError("duplicate model IDs")
    return dict(schema=1, native_graph_roles=32, supplemental_scope_roles=1,
                policy="FP view priority; exact accessory roots and local >=0.5 head/neck + spatial bounds",
                platform_note="Windows source evidence; Android live structural reuse is not Android geometry verification.",
                profiles=profiles)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--roles-root", type=Path, default=Path("F:/zmd_bem/research/identities/roles"))
    parser.add_argument("--catalog-root", type=Path, default=REPO / "tools/CustomModel/catalog")
    parser.add_argument("--output", type=Path, default=REPO / "native/modules/camera/first_person_profiles.generated.inc")
    parser.add_argument("--manifest", type=Path, default=Path(__file__).with_name("profiles.generated.json"))
    parser.add_argument("--check", action="store_true")
    args = parser.parse_args()
    data = generate(args.roles_root, args.catalog_root)
    outputs = {args.output: cpp(data), args.manifest: json.dumps(data, ensure_ascii=False, indent=2) + "\n"}
    for path, content in outputs.items():
        if args.check:
            if not path.exists() or path.read_text(encoding="utf-8") != content:
                raise SystemExit(f"stale generated data: {path}")
        elif not path.exists() or path.read_text(encoding="utf-8") != content:
            path.write_text(content, encoding="utf-8", newline="\n")
    print(f"{'Checked' if args.check else 'Generated'} 32 graph profiles + 1 supplemental scope profile")


if __name__ == "__main__":
    main()
