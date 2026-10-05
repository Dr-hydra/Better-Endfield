"""Import useful research states; never depend on legacy as a live input."""
from __future__ import annotations
import argparse
import json
import re
from pathlib import Path
from workspace_config import load_workspace, write_if_changed
from migrate_workspace import copy_entry


def topic(name):
    return re.sub(r"[-_]20\d{6}(?:[-_]\d{6})?", "", name).replace("_", "-").lower()


def classify(name):
    value=name.lower()
    if value.startswith(("actions", "liino", "acl", "actioninitests")):
        return "actions"
    if value.startswith(("aglina", "aglina", "bundle", "charfbx", "cloth", "hair", "intro", "login", "logo")):
        return "custom_model" if not any(x in value for x in ("pose", "loop", "return", "effect")) else "actions"
    if any(x in value for x in ("camera", "eiem", "poser", "renodx", "firstperson", "enhancer")):
        return "camera"
    if value.startswith("android"):
        return "android"
    if value.startswith(("animator", "lua")):
        return "host"
    return "workspace"


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument("command",choices=("plan","apply"));parser.add_argument("--workspace-config",type=Path)
    args=parser.parse_args();ws=load_workspace(args.workspace_config)
    legacy=ws.path("legacy.root");version=ws.get("game.version")
    rows=[]
    def add(identifier,source,target,module,exclusions=()):
        rows.append({"id":identifier,"source":str(source),"destination":str(target),"kind":"research","mode":"copy","exclude":list(exclusions),"game_version":version,"module":module})
    for source in sorted((legacy/"tmp_analysis").iterdir()):
        if not source.is_dir() or source.name in {"ida","plot-libs","__pycache__"}:
            continue
        module=classify(source.name)
        destination=ws.path("paths.research",module,version,"Windows",topic(source.name))
        add("analysis-"+source.name,source,destination,module,(".git","__pycache__","obj","CMakeFiles",".venv","node_modules"))
    for source in sorted((legacy/"artifacts").iterdir()):
        if not source.is_dir() or source.name in {"android-transfer","betterendfield-native-build","bem-tools","bem-archive-backend","BetterEndfield-win-x64","installer","generic-model-test-build"}:
            continue
        value=source.name.lower()
        if value.startswith("release-"):
            destination=ws.path("paths.releases","history",source.name.removeprefix("release-"),"Windows")
            add("release-"+source.name,source,destination,"workspace")
        else:
            module="camera" if value.startswith("first-person") else "custom_model" if any(x in value for x in ("model","world","loading")) else "android" if value.startswith("android") else "workspace"
            destination=ws.path("paths.research",module,version,"Windows",topic(source.name))
            add("evidence-"+source.name,source,destination,module,("CMakeFiles","build",".cxx","__pycache__","obj","node_modules"))
    add("postmodel-analysis",legacy/"research/postmodel-focused",ws.path("paths.research","custom_model",version,"Windows","postmodel-analysis"),"custom_model",("__pycache__",))
    add("legacy-analysis-state",legacy/"legacy/better-endfield-2.3.1/local-only/state",ws.path("paths.research","host","pre-1.5.3","Windows","legacy-analysis-state"),"host")
    add("legacy-source-research",legacy/"legacy/better-endfield-2.3.1/references/source-research",ws.path("paths.research","host","pre-1.5.3","Windows","legacy-source-research"),"host",("__pycache__",))
    if args.command=="plan":print(json.dumps(rows,ensure_ascii=False,indent=2));return
    registry=ws.path("paths.research","workspace",version,"host","migration","research-imports.json")
    results={x["id"]:x for x in json.loads(registry.read_text(encoding="utf-8")).get("entries",[])} if registry.exists()else {}
    for row in rows:
        source=Path(row["source"]).resolve();target=Path(row["destination"]).resolve()
        if legacy not in source.parents or ws.root not in target.parents:
            raise ValueError("Research import boundary violation")
        print(json.dumps({"importing_research":row["id"],"module":row["module"]},ensure_ascii=False),flush=True)
        results[row["id"]]=copy_entry(row)
        write_if_changed(registry,json.dumps({"schema_version":1,"entries":list(results.values()),"legacy_backup_only":True},ensure_ascii=False,indent=2)+"\n")
    print(json.dumps({"research_imports":len(rows),"registry":str(registry)},ensure_ascii=False),flush=True)


if __name__=="__main__":main()
