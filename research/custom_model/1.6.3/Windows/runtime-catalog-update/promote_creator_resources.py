"""Promote observed Windows 1.6.3 targets and independently derived EFMI identities.

Inputs are existing targeted captures/bundles. Never changes readiness flags to
hide missing source layouts or publishes Windows observations as Android data.
"""
import copy
import json
from pathlib import Path
import shutil
import sys

HERE = Path(__file__).resolve().parent
ROOT = HERE.parents[4]
TOOL = ROOT / "tools/CustomModel"
sys.path.insert(0, str(TOOL))
from import_runtime_catalog import load_observations
from import_efmi_identities import build as build_identities
from build_bem14_target import build_profile

RUN = "bem-1_6_3-login-20261015-091033-3bb4aa"
REVISION = "20261015-1.6.3-identities1"
DEST = TOOL / "profiles/endfield-1.6.3"


def load(path):
    return json.loads(path.read_text(encoding="utf-8-sig"))


def write(path, obj):
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(obj, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def main():
    version, observations, provenance = load_observations(HERE / "captures", [RUN])
    database = load(HERE / "chr_0036_jsspsi/database.observed.json")
    raw_jsspsi = load(HERE / "chr_0036_jsspsi/raw.identities.json")
    raw_minghe = load(HERE / "minghe-shared-inputs/raw.identities.json")
    catalog = build_identities(load(HERE / "catalog-staging/chr_0036_jsspsi.json"), database, raw_jsspsi, observations)
    # Existing importer shares these independently reviewed globals only within
    # the same manifest; their source-content checks still execute at conversion.
    globals_catalog = load(TOOL / "catalog/endminf-pc.json")
    if globals_catalog["source_snapshot"]["manifest_version"] != version:
        raise ValueError("global resource evidence snapshot differs")
    for identity, evidence in globals_catalog["preserved_globals"].items():
        if "source_payload_sha256" not in evidence or identity in catalog["textures"]:
            raise ValueError("global identity evidence missing or conflicting")
        catalog["preserved_globals"][identity] = copy.deepcopy(evidence)
    catalog["revision"] = REVISION
    catalog["evidence"] = ["../profiles/endfield-1.6.3/README.md", "../profiles/endfield-1.6.3/evidence/capture-summary.json"]
    catalog["note"] += " Native contracts are observed; source-layout support remains per-component. Custom replacement rendering is not newly verified."
    for component in catalog["components"].values():
        for observation in component["evidence"]["observations"].values():
            observation["file"] = "../profiles/endfield-1.6.3/evidence/" + Path(observation["file"]).name
    write(TOOL / "catalog/chr_0036_jsspsi.json", catalog)

    coverage = load(TOOL / "catalog/coverage.json")
    row = load(HERE / "catalog-staging/coverage.json")["characters"][0]
    row.update(mapping_status=catalog["mapping_status"], source_entries=len(catalog["entries"]),
               source_textures=len(catalog["textures"]), identity_issues=catalog["identity_issues"])
    coverage["characters"] = [r for r in coverage["characters"] if r["character_id"] != catalog["character_id"]] + [row]
    coverage["characters"].sort(key=lambda r: r["character_id"])
    coverage["runs"] = [r for r in coverage["runs"] if r["run"] != RUN] + provenance
    write(TOOL / "catalog/coverage.json", coverage)
    identities = load(TOOL / "catalog/source-identities.json")
    identity_row = dict(character_id=catalog["character_id"], name=catalog["name"], entries=len(catalog["entries"]),
                        textures=len(catalog["textures"]), mapping_status=catalog["mapping_status"], issues=catalog["identity_issues"])
    identities["characters"] = [r for r in identities["characters"] if r["character_id"] != catalog["character_id"]] + [identity_row]
    identities["characters"].sort(key=lambda r: r["character_id"])
    write(TOOL / "catalog/source-identities.json", identities)

    evidence_dir = DEST / "evidence"
    evidence_dir.mkdir(parents=True, exist_ok=True)
    for capture in (HERE / "captures").glob(RUN + "*.jsonl"):
        shutil.copy2(capture, evidence_dir / capture.name)
    summary = load(HERE / "runs" / RUN / "summary.json")
    write(evidence_dir / "capture-summary.json", summary)
    full_spec = dict(schema=1, kind="bem-resource-target-spec", id="chr_0036_jsspsi", target_kind="character",
        profile_id="chr_0036_jsspsi-full-pc", revision="1.6.3-20261015", snapshot=version,
        resources=[dict(id="world", asset_path="assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0036_jsspsi_postmodel.prefab", platforms=["windows-x64"], lod=0),
                   dict(id="ui", asset_path="assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0036_jsspsi_uimodel.prefab", platforms=["windows-x64"], lod=0)])
    specs = [("chr_0036_jsspsi-full", full_spec, database, raw_jsspsi, "祀 · 完整世界与详情资源")]
    for prefix, role, title in [
        ("chr_0036_jsspsi-cloth04-world", "chr_0036_jsspsi", "祀 · 世界 cloth_04"),
        ("chr_0037_minghe-world", "chr_0037_minghe", "明河 · 世界模型"),
        ("chr_0037_minghe02-world", "chr_0037_minghe02", "明河 · 世界模型 02"),
        ("chr_0037_minghe03-world", "chr_0037_minghe03", "明河 · 世界模型 03"),
    ]:
        specs.append((prefix, load(HERE / "target-profiles" / (prefix + ".spec.json")),
                      load(HERE / role / "database.observed.json"), raw_jsspsi if role.startswith("chr_0036") else raw_minghe, title))
    rows = []
    for prefix, spec, graph, raw, title in specs:
        profile, manifest = build_profile(graph, spec)
        renderers = {r["id"]: r for r in graph["renderers"]}
        # Resource-local source identities are references for explicit authors,
        # never fed to the legacy source converter as an invented world/UI pair.
        local_catalog = dict(source_snapshot={"manifest_version": version}, components={}, entries={}, textures={})
        layouts = []
        for item in profile["evidence"]:
            native = renderers[item["renderer_id"]]
            observed = observations[(native["resource_root"], native["path"])]["row"]
            if not item["runtime_layout_observed"]:
                raise ValueError("target lacks an actual runtime declaration")
            local_catalog["components"][str(item["component"])] = dict(mesh_name=native["mesh_name"],
                original_index_count=native["original_index_count"], evidence=dict(mesh_id=native["mesh_id"], observations={
                    native["resource_root"]: dict(renderer_id=native["id"], path=native["path"])}))
            layouts.append(dict(component=item["component"], resource=item["resource"], renderer_path=item["renderer_path"],
                mesh_id=native["mesh_id"], mesh_name=native["mesh_name"], attributes=observed["attributes"], strides=observed["strides"],
                vertex_count=observed["vertex_count"], original_index_count=observed["original_index_count"]))
        local = build_identities(local_catalog, graph, raw, observations)
        resource_entries = {}
        resource_issues = []
        for resource in profile["target"]["resources"]:
            ids = {str(e["component"]) for e in profile["evidence"] if e["resource"] == resource["id"]}
            scoped_catalog = {**local_catalog, "components": {k: v for k, v in local_catalog["components"].items() if k in ids}}
            scoped = build_identities(scoped_catalog, graph, raw, observations)
            resource_entries[resource["id"]] = scoped["entries"]
            resource_issues.extend({"resource": resource["id"], **issue} for issue in scoped["identity_issues"])
        local_evidence = dict(schema=1, kind="bem-explicit-resource-evidence", profile_id=profile["target"]["profile_id"],
            platform="windows-x64", source_snapshot={"manifest_version": version}, run=RUN, layouts=layouts,
            identity_policy=local["identity_policy"], resource_entries=resource_entries, entry_identity_evidence=local["entry_identity_evidence"],
            textures=local["textures"], identity_issues=resource_issues, legacy_conversion_ready=False)
        evidence_name = "evidence/" + prefix + ".native.json"
        write(DEST / evidence_name, local_evidence)
        profile["name"] = title
        profile["evidence_file"] = evidence_name
        profile["limitations"] = [
            "Original Windows 1.6.3 resource identities and runtime layouts are observed; replacement rendering and lifecycle are not verified.",
            "The included project is keep-only. Add reviewed replacement payloads and resource-local source mappings before creation.",
            "This explicit target is not a legacy world/UI conversion profile; no Android donor evidence is included."]
        manifest["name"] = title + "（创作起点，无替换）"
        write(DEST / (prefix + ".spec.json"), spec)
        write(DEST / (prefix + ".profile.json"), profile)
        write(DEST / (prefix + ".project.json"), dict(manifest=manifest, payload_files=[]))
        rows.append(dict(name=title, owner_id=profile["target"]["id"], profile=prefix + ".profile.json",
            project=prefix + ".project.json", components=len(profile["target"]["components"]), platforms=["windows-x64"],
            preparation="keep-only", runtime_verified=False, conversion_ready=False, render_verified=False))
    index = dict(schema=1, kind="bem-bundled-resource-index", game_version="1.6.3", platforms=["windows-x64"],
        legacy_catalog=dict(path="../../catalog/chr_0036_jsspsi.json", character_id=catalog["character_id"], name=catalog["name"],
            mapping_status=catalog["mapping_status"], components=len(catalog["components"]), source_entries=len(catalog["entries"]),
            source_textures=len(catalog["textures"]), supported_source_layouts=sum(c.get("source_layout_supported") is True for c in catalog["components"].values())),
        targets=rows)
    write(DEST / "index.json", index)
    print(json.dumps(dict(catalog=identity_row, profiles=[dict(name=r["name"], components=r["components"]) for r in rows]), ensure_ascii=False))


if __name__ == "__main__":
    main()
