"""Stage an isolated login-model capture for Aurora; no user or game writes."""
from pathlib import Path
import argparse
import json
import shutil
import sys

ROOT = Path(__file__).resolve().parents[5]
LIVE = ROOT / "build/aurora-compat-1.6.3/live"
INSTALL = Path("E:/Better Endfield/Better Endfield Next")
MANIFEST = ROOT / "research/custom_model/1.6.3/Windows/runtime-catalog-update/minghe-shared-inputs/Bundles/Windows/manifest.json"
RUN = "aurora-1_6_3-login-20261015"
sys.path.insert(0, str(ROOT / "tools/CustomModel"))
from runtime_sweep import make_request


def write_settings(target):
    targets = json.loads((LIVE / "targets.json").read_text(encoding="utf-8"))
    character = targets["character"]
    action = next(a for a in character["actions"] if a["id"] == character["defaultActionId"])
    resource = targets[target]
    model = {"enabled": "true", "model_replacement_enabled": "true", "character": character["id"],
             "model_path": resource["path"], "model_path_hash": resource["pathHash"],
             "final_action": action["id"], "final_native_loop": str(action.get("nativeLoop", False)).lower(),
             "logo_theme_enabled": "false"}
    for prefix, clip in (("sit_loop", character["sitLoop"]), ("sit_special", character["sitSpecial"]),
                         ("sit_to_walk", character["sitToWalk"]), ("final", action)):
        model[prefix + "_path"] = clip["path"]
        model[prefix + "_path_hash"] = clip["pathHash"]
        model[prefix + "_label"] = clip.get("displayName", clip.get("id", ""))
    runtime = LIVE / "runtime"
    sections = {"Loader": {"install_root": str(runtime), "load_host": "true"},
                "Host": {"modules_root": str(runtime / "modules")},
                "betterendfieldnext.model": model,
                "betterendfieldnext.custom_model": {"enabled": "true"}}
    settings = LIVE / "local/BetterEndfieldNext/BetterEndfieldNext.ini"
    settings.parent.mkdir(parents=True, exist_ok=True)
    text = "".join(f"[{section}]\r\n" + "".join(f"{k}={v}\r\n" for k, v in values.items()) + "\r\n"
                   for section, values in sections.items())
    temporary = settings.with_suffix(".tmp")
    temporary.write_bytes(text.encode("utf-16"))
    temporary.replace(settings)
    print(json.dumps({"selected": target, "resource": resource["path"], "settings": str(settings)}))


def prepare():
    if LIVE.exists():
        raise RuntimeError("Capture directory already exists; select a resource or inspect it, do not reset it.")
    files = ["loaders/BetterEndfieldNext.Injector.exe", "loaders/BetterEndfieldNext.Bootstrap.dll",
             "runtime/BetterEndfieldNext.Host.dll", "modules/BetterEndfieldNext.Model.dll",
             "modules/betterendfieldnext.model.module.ini", "modules/betterendfieldnext.custom_model.module.ini"]
    for relative in files:
        destination = LIVE / "runtime" / relative
        destination.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(INSTALL / relative, destination)
    shutil.copy2(ROOT / "build/bem-capture-1.6.3/modules/BetterEndfieldNext.CustomModel.dll",
                 LIVE / "runtime/modules/BetterEndfieldNext.CustomModel.dll")
    presets = json.loads((ROOT / "resources/generated/model/character-presets.json").read_text(encoding="utf-8-sig"))
    character = next(c for c in presets["characters"] if c["id"] == "chr_0014_aurora")
    manifest = json.loads(MANIFEST.read_text(encoding="utf-8-sig"))
    text, metadata = make_request(manifest, RUN, [character["id"]], persistent=False)
    targets = {"character": character}
    for resource in metadata["resources"].values():
        asset = next(a for a in manifest["Assets"] if a["path"] == resource["asset_path"])
        targets[resource["variant"]] = {"path": asset["path"], "pathHash": f"0x{asset['pathHashHead']:016X}"}
    (LIVE / "targets.json").write_text(json.dumps(targets, ensure_ascii=False, indent=2), encoding="utf-8")
    catalog = LIVE / "local/BetterEndfieldNext/catalog/custom-model"
    capture = catalog / "native-probe"
    capture.mkdir(parents=True)
    metadata["manifest_file"] = str(MANIFEST)
    (capture / (RUN + ".request.json")).write_text(json.dumps(metadata, indent=2), encoding="utf-8")
    (catalog / "native-probe.request").write_text(text, encoding="utf-8")
    (catalog / "runtime.ini").write_text("[Options]\nhot_switch=false\n", encoding="utf-8")
    write_settings("world")
    print(json.dumps({"run": RUN, "catalog": str(catalog), "resources": len(metadata["resources"])}))


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("operation", choices=["prepare", "world", "ui"])
    args = parser.parse_args()
    prepare() if args.operation == "prepare" else write_settings(args.operation)
