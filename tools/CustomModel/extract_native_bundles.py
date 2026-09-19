"""Extract a character's world/UI bundle closure from the installed VFS overlay.

Reuses the project's VFS reader; only writes to a new output directory. No
asset hashes are computed and no installed game file is modified.
"""
import argparse
import importlib.util
import json
from pathlib import Path, PurePosixPath
import re
import subprocess
import sys
import types
import zlib


def load(name, path):
    spec = importlib.util.spec_from_file_location(name, path)
    module = importlib.util.module_from_spec(spec)
    sys.modules[name] = module
    spec.loader.exec_module(module)
    return module


def select_bundles(manifest, character, extra_assets=()):
    paths = [f"assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/{character}_postmodel.prefab",
             f"assets/beyond/dynamicassets/gameplay/prefabs/uimodels/{character}_uimodel.prefab", *extra_assets]
    assets = {}
    for asset in manifest["Assets"]:
        if asset["path"].lower() in paths:
            if asset["path"].lower() in assets: raise ValueError("duplicate asset identity")
            assets[asset["path"].lower()] = asset
    missing = set(paths) - set(assets)
    if missing: raise ValueError(f"required character assets not found: {sorted(missing)}")
    bundles = {b["bundleIndex"]: b for b in manifest["Bundles"]}
    pending = [a["bundleIndex"] for a in assets.values()]
    selected = set()
    while pending:
        index = pending.pop()
        if index in selected: continue
        if index not in bundles: raise ValueError(f"missing dependency bundle {index}")
        selected.add(index)
        if len(selected) > 512: raise ValueError("character dependency closure exceeds 512 bundles")
        bundle = bundles[index]
        pending.extend(bundle.get("dependencies", []))
        pending.extend(bundle.get("directDependencies", []))
    names = []
    for index in sorted(selected):
        name = bundles[index]["name"]
        path = PurePosixPath(name)
        if path.is_absolute() or ".." in path.parts or "\\" in name or ":" in name:
            raise ValueError("invalid manifest bundle path")
        names.append("Bundles/Windows/" + name)
    return list(assets.values()), names


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--game", required=True, type=Path)
    parser.add_argument("--character", required=True)
    parser.add_argument("--unpacker", required=True, type=Path)
    parser.add_argument("--resconv", required=True, type=Path)
    parser.add_argument("--output", required=True, type=Path)
    parser.add_argument("--extra-asset", action="append", default=[], help="Exact extra prefab asset path, e.g. a transformed state")
    args = parser.parse_args()
    if not re.fullmatch(r"chr_\d{4}_[a-z0-9]+", args.character): parser.error("invalid character ID")
    game, out = args.game.resolve(), args.output.resolve()
    if out == game or out.is_relative_to(game): parser.error("output must be outside the game directory")
    if out.exists() and any(out.iterdir()): parser.error("output directory must be empty to avoid stale bundles")
    out.mkdir(parents=True, exist_ok=True)
    helpers = load("native_vfs_helpers", Path(__file__).resolve().parents[2] / "scripts/RefreshEndfieldResourceInputs.py")
    stub = types.ModuleType("config"); stub.get_game_dir = lambda: str(game)
    sys.modules["config"] = stub
    unpacker = load("native_vfs_reader", args.unpacker / "decrypt_vfs.py")
    unpacker.crc32 = lambda data: (zlib.crc32(data) + 2**31) % 2**32 - 2**31
    roots = [("StreamingAssets", game / "Endfield_Data/StreamingAssets/VFS"),
             ("Persistent", game / "Endfield_Data/Persistent/VFS")]
    helpers.extract_overlay(unpacker, roots, out, re.compile(r"Bundles/Windows/manifest\.hgmmap", re.I))
    manifest_path = out / "Bundles/Windows/manifest.hgmmap"
    if not manifest_path.exists(): raise ValueError("installed manifest missing")
    subprocess.run([str(args.resconv), str(manifest_path)], check=True, stdout=subprocess.DEVNULL)
    manifest = json.loads(manifest_path.with_suffix(".json").read_text(encoding="utf-8-sig"))
    assets, names = select_bundles(manifest, args.character, [p.lower() for p in args.extra_asset])
    records = helpers.extract_overlay(unpacker, roots, out, re.compile("(?:" + "|".join(map(re.escape, names)) + ")", re.I))
    missing = {n.lower() for n in names} - {r.relative_path.lower() for r in records}
    snapshot = {"schema": 1, "character": args.character, "game": str(game), "assets": assets,
                "manifest_version": manifest.get("Version"), "perforce_cl": manifest.get("perforceCL"),
                "bundles": [{"path": r.relative_path, "layer": r.layer, "size": r.size} for r in records],
                "missing": sorted(missing)}
    (out / "extraction.json").write_text(json.dumps(snapshot, ensure_ascii=False, indent=2), encoding="utf-8")
    print(f"character={args.character} assets={len(assets)} bundles={len(records)} missing={len(missing)}")
    return 2 if missing else 0


if __name__ == "__main__":
    raise SystemExit(main())
