"""Rebuild Snowshine creator data only after current direct world/UI capture.

Uses the reviewed importers with fresh source identities. Stages portable data
for both standalone BEM Tools and BE's tools/BemConverter; never deploys them.
"""
import argparse
import copy
import json
from pathlib import Path
import shutil
import sys

HERE = Path(__file__).resolve().parent
ROOT = HERE.parents[4]
TOOL = ROOT / "tools/CustomModel"
sys.path.insert(0, str(TOOL))
from import_runtime_catalog import build_role, load_observations, validate_observation
from import_efmi_identities import build as build_identities
from parse_native_models import apply_observations
from build_bem14_target import build_profile
from bem_tool import TOOL_VERSION

CHARACTER = "chr_0014_aurora"
REVISION = "20261015-1.6.3-snowshine1"
PREFIX = "chr_0014_aurora-full"


def read(path):
    return json.loads(path.read_text(encoding="utf-8-sig"))


def write(path, value):
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2, allow_nan=False) + "\n", encoding="utf-8")


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--captures", type=Path, required=True)
    parser.add_argument("--runs", nargs="+", required=True)
    parser.add_argument("--output", type=Path, required=True)
    parser.add_argument("--promote", action="store_true", help="Also copy reviewed staged catalog/profiles into repository tools")
    args = parser.parse_args()
    output = args.output.resolve()
    if output.exists() and any(output.iterdir()):
        raise ValueError("Staging output must be empty")
    version, observations, provenance = load_observations(args.captures, args.runs)
    database = read(HERE / "current/database.json")
    raw = read(HERE / "current/raw.identities.json")
    baseline = HERE / "current/previous-catalog.json"
    old = read(baseline if baseline.exists() else TOOL / "catalog" / (CHARACTER + ".json"))
    world, ui = CHARACTER + "_postmodel", CHARACTER + "_uimodel"
    rows = {(r["resource_root"], r["path"]): r for r in database["renderers"]}
    attached = []
    for key, observation in observations.items():
        if key[0] not in (world, ui):
            continue
        native = rows.get(key)
        if native is None:
            raise ValueError("Captured renderer is absent from current offline graph: " + repr(key))
        row = observation["row"]
        validate_observation(native, database["meshes"][native["mesh_id"]], row)
        attached.append({**{k: row[k] for k in ("resource_root", "path", "mesh_name", "original_index_count", "attributes", "strides")},
                         "id": native["id"], "runtime_id": row["id"]})
    normalized = dict(schema=1, evidence=", ".join(args.runs), manifest_version=version,
                      perforce_cl=database["source"]["snapshot"].get("perforce_cl"), renderers=attached)
    apply_observations(database, normalized)
    # The old contract intentionally fails the standard importer's reuse gate.
    # Build fresh contracts and preserve component numbers only by exact names.
    catalog = build_role(database, observations, CHARACTER, old["name"], version)
    fresh = {c["mesh_name"]: c for c in catalog["components"].values()}
    if set(fresh) != {c["mesh_name"] for c in old["components"].values()}:
        raise ValueError("Component set changed; review numbering before promotion")
    catalog["components"] = {cid: fresh[c["mesh_name"]] for cid, c in old["components"].items()}
    for component in catalog["components"].values():
        if set(component["evidence"]["observations"]) != {world, ui} or any(
                e["kind"] != "direct" for e in component["evidence"]["observations"].values()):
            raise ValueError("Both current roots need direct observations before publishing Snowshine")
    catalog["profile_id"] = old["profile_id"]
    catalog = build_identities(catalog, database, raw, observations)
    # These are source-content-preservation checks, not a target mesh mapping.
    # The converter still checks the full source payload against this evidence.
    for identity, evidence in old["preserved_globals"].items():
        if not evidence.get("source_payload_sha256") or identity in catalog["textures"]:
            raise ValueError("Invalid preserved source-global evidence")
        catalog["preserved_globals"][identity] = copy.deepcopy(evidence)
    catalog["revision"] = REVISION
    catalog["evidence"] = ["../profiles/endfield-1.6.3/SNOWSHINE.md", "../profiles/endfield-1.6.3/evidence/snowshine-summary.json"]
    catalog["note"] += " Windows 1.6.3 direct world/UI observations; old C1 source identities were not retained. Native verification does not establish arbitrary Mod conversion or replacement rendering."
    for component in catalog["components"].values():
        for evidence in component["evidence"]["observations"].values():
            evidence["file"] = "../profiles/endfield-1.6.3/evidence/" + Path(evidence["file"]).name

    spec = dict(schema=1, kind="bem-resource-target-spec", id=CHARACTER, target_kind="character",
                profile_id=CHARACTER + "-full-pc", revision="1.6.3-20261015", snapshot=version,
                resources=[dict(id="world", asset_path="assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0014_aurora_postmodel.prefab", platforms=["windows-x64"], lod=0),
                           dict(id="ui", asset_path="assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0014_aurora_uimodel.prefab", platforms=["windows-x64"], lod=0)])
    profile, manifest = build_profile(database, spec)
    if not all(item["runtime_layout_observed"] for item in profile["evidence"]):
        raise ValueError("Explicit LOD0 target still lacks direct runtime declarations")
    renderers = {row["id"]: row for row in database["renderers"]}
    resources = {}
    for resource in profile["target"]["resources"]:
        local = dict(source_snapshot={"manifest_version": version}, components={}, entries={}, textures={})
        layouts = []
        for item in profile["evidence"]:
            if item["resource"] != resource["id"]:
                continue
            native = renderers[item["renderer_id"]]
            observed = observations[(native["resource_root"], native["path"])]["row"]
            local["components"][str(item["component"])] = dict(mesh_name=native["mesh_name"], original_index_count=native["original_index_count"],
                evidence=dict(mesh_id=native["mesh_id"], observations={native["resource_root"]:dict(renderer_id=native["id"], path=native["path"])}))
            layouts.append(dict(component=item["component"], renderer_path=item["renderer_path"], mesh_id=native["mesh_id"], mesh_name=native["mesh_name"],
                                attributes=observed["attributes"], strides=observed["strides"], vertex_count=observed["vertex_count"], original_index_count=observed["original_index_count"]))
        identity = build_identities(local, database, raw, observations)
        resources[resource["id"]] = dict(layouts=layouts, entries=identity["entries"], entry_identity_evidence=identity["entry_identity_evidence"],
                                        textures=identity["textures"], identity_issues=identity["identity_issues"])
    profile["name"] = "昼雪 · 1.6.3 世界与详情资源"
    profile["evidence_file"] = "evidence/" + PREFIX + ".native.json"
    profile["limitations"] = ["Direct Windows 1.6.3 native observations; no Android donor evidence.",
        "All rules are keep-only. No replacement payload or automatic arbitrary-source mapping is supplied.",
        "Replacement rendering and lifecycle are not established by native capture; readiness flags remain false."]
    manifest["name"] = profile["name"] + "（创作起点，无替换）"

    for directory in ("catalog", "profiles"):
        shutil.copytree(TOOL / directory, output / directory, ignore=shutil.ignore_patterns("__pycache__", "*.pyc"))
    destination = output / "profiles/endfield-1.6.3"
    evidence_directory = destination / "evidence"
    for filename in {Path(v["file"]).name for v in observations.values()}:
        shutil.copy2(args.captures / filename, evidence_directory / filename)
    changes = {cid: {field:dict(before=old["components"][cid].get(field), after=component.get(field))
                    for field in ("original_index_count", "bone_names", "strides", "attributes", "vertex_count")
                    if old["components"][cid].get(field) != component.get(field)}
               for cid, component in catalog["components"].items()}
    summary = dict(schema=1, game_version="1.6.3", character_id=CHARACTER, runs=args.runs, provenance=provenance,
                   matched_renderers=len(attached), catalog_components=len(catalog["components"]), entries=len(catalog["entries"]),
                   textures=len(catalog["textures"]), identity_issues=catalog["identity_issues"],
                   supported_source_layouts=sum(c["source_layout_supported"] for c in catalog["components"].values()),
                   changes={cid:value for cid,value in changes.items() if value}, render_verified=False, artifact_hashes_computed=False)
    write(destination / "evidence/snowshine-summary.json", summary)
    write(destination / profile["evidence_file"], dict(schema=1, kind="bem-explicit-resource-evidence", profile_id=profile["target"]["profile_id"],
        platform="windows-x64", source_snapshot={"manifest_version":version}, runs=args.runs, identity_policy=catalog["identity_policy"], resources=resources,
        legacy_conversion_ready=False))
    write(destination / (PREFIX + ".spec.json"), spec)
    write(destination / (PREFIX + ".profile.json"), profile)
    write(destination / (PREFIX + ".project.json"), dict(manifest=manifest, payload_files=[]))
    write(output / "catalog" / (CHARACTER + ".json"), catalog)
    coverage = read(output / "catalog/coverage.json")
    coverage["runs"] = [r for r in coverage["runs"] if r["run"] not in args.runs] + provenance
    row = next(row for row in coverage["characters"] if row["character_id"] == CHARACTER)
    row.update(components=len(catalog["components"]), mapping_status=catalog["mapping_status"], excluded=catalog["excluded_components"],
               unsupported_source_layouts=[c["mesh_name"] for c in catalog["components"].values() if not c["source_layout_supported"]],
               observations={root:sum(c["evidence"]["observations"][root]["kind"] == "direct" for c in catalog["components"].values()) for root in (world,ui)},
               source_entries=len(catalog["entries"]), source_textures=len(catalog["textures"]), identity_issues=catalog["identity_issues"])
    write(output / "catalog/coverage.json", coverage)
    identities = read(output / "catalog/source-identities.json")
    row = next(row for row in identities["characters"] if row["character_id"] == CHARACTER)
    row.update(entries=len(catalog["entries"]), textures=len(catalog["textures"]), mapping_status=catalog["mapping_status"], issues=catalog["identity_issues"])
    write(output / "catalog/source-identities.json", identities)
    index = read(destination / "index.json")
    index["targets"] = [item for item in index["targets"] if item["profile"] != PREFIX + ".profile.json"]
    index["targets"].append(dict(name=profile["name"], owner_id=CHARACTER, profile=PREFIX + ".profile.json", project=PREFIX + ".project.json",
        components=len(profile["target"]["components"]), platforms=["windows-x64"], preparation="keep-only", runtime_verified=False, conversion_ready=False, render_verified=False))
    write(destination / "index.json", index)
    note = ("# 昼雪 Windows 1.6.3 创作资料\n\n"
        "当前昼雪世界/详情原生合同均已直接采集；catalog 保留原 12 个组件编号，更新 C1 的索引数与骨骼顺序，以及 C5 的原生声明。"
        "EFMI 索引与纹理身份重新从当前游戏资源及实际绑定计算，不沿用旧 C1 的源身份。\n\n"
        f"目录包含 {len(catalog['entries'])} 个索引身份、{len(catalog['textures'])} 个纹理身份，{summary['supported_source_layouts']}/12 个部件符合现有来源布局规则。"
        "其余布局与多子网格身份缺项仍明确保留，不能据此承诺任意旧 EFMI Mod 自动转换。\n\n"
        f"[{PREFIX}.project.json]({PREFIX}.project.json) 为世界/详情共 {len(profile['target']['components'])} 个部件的 BEM 1.4 keep-only 工程；不含替换网格或原游戏像素。"
        "原生采集不等于替换渲染验收，runtime_verified/conversion_ready/render_verified 保持 false；没有新增 Android 证据。\n\n"
        "证据：[采集与变更摘要](evidence/snowshine-summary.json)、[资源局部身份与布局](evidence/" + PREFIX + ".native.json)。\n")
    (destination / "SNOWSHINE.md").write_text(note, encoding="utf-8")
    readme = destination / "README.md"
    readme_text = readme.read_text(encoding="utf-8")
    if "## 昼雪 1.6.3 修订" not in readme_text:
        readme.write_text(readme_text + "\n## 昼雪 1.6.3 修订\n\n[昼雪资料](SNOWSHINE.md) 更新当前世界/详情合同、EFMI 身份及 keep-only 工程；与既有祀、明河资料一并分发。\n", encoding="utf-8")
    if not baseline.exists():
        write(baseline, old)
    write(HERE / "current/runtime-observations.json", normalized)
    write(HERE / "current/database.observed.json", database)
    write(output / "data-update.json", dict(schema=1, revision=REVISION, tool_version=TOOL_VERSION, binary_payloads=False,
          targets=["Standalone BEM-Tools root", "BE tools/BemConverter"], includes=["catalog", "profiles"], summary=summary))
    if args.promote:
        for directory in ("catalog", "profiles"):
            shutil.copytree(output / directory, TOOL / directory, dirs_exist_ok=True)
    print(json.dumps({key:summary[key] for key in ("matched_renderers", "catalog_components", "entries", "textures", "supported_source_layouts", "identity_issues")}, ensure_ascii=False))


if __name__ == "__main__":
    main()
