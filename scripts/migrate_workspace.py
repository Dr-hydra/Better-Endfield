"""Import reviewed local dependencies while retaining the legacy rollback tree."""
from __future__ import annotations
import argparse
import json
import os
import shutil
import subprocess
import sys
from datetime import datetime
from pathlib import Path
from workspace_config import load_workspace, write_if_changed


def entries(ws):
    legacy = ws.path("legacy.root")
    version = ws.get("game.version", "unknown")
    rows = []
    def add(key, source, destination, kind, mode="copy", exclude=()):
        data_version = None if kind == "toolchain" else ("pre-1.5.3" if key == "legacy-lod-catalog" else version)
        rows.append(dict(id=key, source=str(legacy/source), destination=str(destination), kind=kind, mode=mode, exclude=list(exclude), game_version=data_version, provenance="User identified current client as 1.5.3 since early September; original snapshot metadata retained"))
    add("resource-baseline", "research/current-inputs", ws.path("resource_update.input_root"), "input")
    add("character-catalog", "research/character-catalog-current", ws.path("resource_update.catalog_root"), "input")
    add("walk-clip-metadata", "research/character-catalog-current/walk-clip-metadata.json", ws.path("resource_update.walk_clip_metadata"), "input")
    add("tables", "research/table-dump", ws.path("resource_update.table_root").parent, "input")
    add("voice-banks", "research/bank-pck", ws.path("resource_update.bank_root").parent, "input")
    for topic in ("BuffData", "SkillData", "AnimationConfig", "CharInteractPerformCfgs"):
        add("json-"+topic.lower(), "research/combat-jsondata/Data/Json/"+topic, ws.path("resource_update.json_root", topic), "input")
    add("legacy-lod-catalog", "legacy/better-endfield-2.3.1/references/source-research/character-catalog/characters.json", ws.path("resource_update.legacy_lod_catalog"), "input")
    add("android-sdk", "tools/android-toolchain/sdk", ws.path("tools.android_sdk"), "toolchain", exclude=("__pycache__",))
    add("android-dobby", "tools/android-toolchain/dobby-1.0.5", ws.path("tools.android_dobby"), "toolchain", exclude=(".git", "build"))
    add("fkarkend", "tools/FkArkEnd", ws.path("tools.fkarkend_root"), "toolchain", exclude=(".git", "__pycache__"))
    add("unpacker", "tools/EndfieldUnpacker", ws.path("tools.endfield_unpacker_root"), "toolchain", exclude=(".git", ".venv", "__pycache__"))
    add("wwiser", "vendor/wwiser", ws.path("tools.wwiser_root"), "toolchain", exclude=(".git", "__pycache__"))
    add("native-reader", ".worktrees/dev-custom-model-design/artifacts/native-parser/reader-identities", ws.path("tools.native_asset_reader").parent, "toolchain", exclude=("*.pdb",))
    add("archive-backend", "artifacts/bem-archive-backend/7zip", ws.path("tools.archive_backend"), "toolchain")
    add("native-reader-source", ".worktrees/dev-custom-model-design/artifacts/native-parser/backend-source", ws.path("paths.research", "custom_model", version, "Windows", "native-parser", "source"), "research", exclude=("bin", "obj", ".git"))
    for key, source in [
        ("host-il2cpp", "research/il2cpp-dumps"),
        ("host-lua", "research/lua-dumps"),
        ("camera-samples", "tmp_analysis/aglina-native-return-v2"),
        ("camera-motion-inputs", "tmp_analysis/liino-animation-inputs")]:
        module = "host" if key.startswith("host") else "camera"
        add(key, source, ws.path("paths.research", module, version, "Windows", key), "research")
    # Useful historical states are imported physically. Legacy remains backup
    # only and is never used as an active input root after migration.
    for key, source, module in [
        ("android-vfs-transfer", "artifacts/android-transfer", "resources"),
        ("ida-state", "tmp_analysis/ida", "host"),
        ("combat-full-json", "research/combat-jsondata", "combat_stats"),
        ("postmodel-native-parser-history", ".worktrees/dev-custom-model-design/artifacts/native-parser", "custom_model")]:
        destination = ws.path("paths.research", module, version, "shared", key)
        exclusions = ()
        if key == "android-vfs-transfer":
            destination = ws.path("paths.inputs", version, "Android", "vfs-transfer")
        elif key == "combat-full-json":
            source = "research/combat-jsondata/Data/Json"
            destination = ws.path("resource_update.json_root")
        elif key == "postmodel-native-parser-history":
            exclusions = ("backend", "backend-source", "reader-identities", ".git", "__pycache__")
        add(key, source, destination, "archive", exclude=exclusions)
    add("combat-json-previous", "research/combat-jsondata/.Json.backup", ws.path("paths.inputs", version, "Windows", "json-previous-incomplete"), "archive")
    add("android-resource-evidence", "android/resources/.sources", ws.path("paths.inputs", version, "Android", "resource-sources"), "input")
    return rows


def copy_entry(row):
    source, target = Path(row["source"]), Path(row["destination"])
    if not source.exists():
        return {**row, "status": "source_missing", "copied": False}
    if row["mode"] == "register":
        target.mkdir(parents=True, exist_ok=True)
        write_if_changed(target/"source.json", json.dumps({"source":str(source),"read_only_archive":True,"game_version":row["game_version"],"migration": "Archive remains available; not copied automatically."},ensure_ascii=False,indent=2)+"\n")
        return {**row, "status": "archived_external", "copied": False}
    target.parent.mkdir(parents=True, exist_ok=True)
    if source.is_file():
        if target.is_file() and target.stat().st_size == source.stat().st_size and target.read_bytes() == source.read_bytes():
            return {**row, "status": "already_present", "copied": False}
        if target.exists():
            raise FileExistsError(f"Refusing to overwrite a different retained file: {target}")
        shutil.copy2(source, target)
    elif os.name == "nt":
        args = ["robocopy", str(source), str(target), "/E", "/XJ", "/COPY:DAT", "/DCOPY:DAT", "/R:0", "/W:0", "/MT:4", "/NFL", "/NDL", "/NJH", "/NJS", "/NP"]
        if row["exclude"]:
            args += ["/XD", *row["exclude"]]
        proc = subprocess.run(args, capture_output=True, text=True, errors="replace")
        if proc.returncode >= 8:
            raise RuntimeError(f"Copy failed ({proc.returncode}): {source}\n{proc.stdout[-1500:]}\n{proc.stderr[-1500:]}")
    else:
        shutil.copytree(source,target,dirs_exist_ok=True,ignore=shutil.ignore_patterns(*row["exclude"]),symlinks=True)
    return {**row,"status":"imported","copied":True}


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument("command",choices=("plan","apply"));parser.add_argument("--workspace-config",type=Path)
    parser.add_argument("--only",action="append",default=[])
    args=parser.parse_args();ws=load_workspace(args.workspace_config)
    rows=entries(ws)
    if args.only:rows=[row for row in rows if row["id"] in args.only]
    if args.command=="plan":print(json.dumps(rows,ensure_ascii=False,indent=2));return
    report=ws.ensure("paths.research")/"workspace"/ws.get("game.version", "unknown")/"host/migration/imports.json"
    report.parent.mkdir(parents=True,exist_ok=True)
    old=json.loads(report.read_text(encoding="utf-8")) if report.exists() else {"entries":[]}
    results={row["id"]:row for row in old.get("entries",[])}
    for row in rows:
        target=Path(row["destination"]).resolve();source=Path(row["source"]).resolve()
        if ws.root not in target.parents or target==ws.root:
            raise ValueError(f"Import target outside new workspace: {target}")
        legacy=ws.path("legacy.root")
        if legacy not in source.parents:
            raise ValueError(f"Import source outside legacy workspace: {source}")
        print(json.dumps({"importing":row["id"],"mode":row["mode"]},ensure_ascii=False),flush=True)
        results[row["id"]]=copy_entry(row)
        write_if_changed(report,json.dumps({"schema_version":1,"entries":list(results.values()),"updated_at":datetime.now().astimezone().isoformat(),"legacy_preserved":True},ensure_ascii=False,indent=2)+"\n")
        print(json.dumps({"completed":row["id"],"status":results[row["id"]]["status"]},ensure_ascii=False),flush=True)


if __name__=="__main__":main()
