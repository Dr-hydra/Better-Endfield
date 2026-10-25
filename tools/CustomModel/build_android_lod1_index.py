"""Build or refresh the Android LOD1 renderer index from real prefab data.

The relation table used to derive LOD1 mesh names from a pre-1.5.3 Windows
catalog that only lists 32 characters.  Characters outside that catalog fell
through a fallback that simply appended ``_lod1`` to the UI mesh stem, and the
resulting rows were still stamped ``reference_confirmed,mesh_names_confirmed``.
That is how ``chr_0038_purrche`` got three wrong fur rows: its Android meshes
are ``S_actor_purrchena_fur_0{1,2,3}_lod1_11``.

This script replaces the guess with evidence for a *named* character.  It
extracts that character's Android prefab closure from the VFS, parses it with
NativeAssetReader, and records every renderer under ``Mesh_all/lod1/`` together
with its resolved mesh name and array sizes.

The index is maintained incrementally on purpose.  Pass only the characters
that need rebuilding; entries for every other character are carried over
verbatim, so the generated table only changes where evidence changed.
"""
from __future__ import annotations

import argparse
import importlib.util
import json
import re
from pathlib import Path, PurePosixPath
import subprocess
import sys
import types
import zlib
from typing import Any

LOD1_PREFIX = "mesh_all/lod1/"


def load(name: str, path: Path) -> Any:
    spec = importlib.util.spec_from_file_location(name, path)
    if spec is None or spec.loader is None:
        raise RuntimeError(f"cannot load Python module {path}")
    module = importlib.util.module_from_spec(spec)
    sys.modules[name] = module
    spec.loader.exec_module(module)
    return module


def select_bundles(manifest: dict[str, Any], character: str) -> tuple[list[dict], list[str]]:
    """Resolve the world prefab closure.  LOD1 renderers live only there, so the
    UI prefab closure — which pulls update-layer bundles that a base VFS dump does
    not carry — is deliberately left out."""
    paths = [
        f"assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/{character}_postmodel.prefab",
    ]
    assets: dict[str, dict] = {}
    for asset in manifest["Assets"]:
        if asset["path"].lower() in paths:
            assets[asset["path"].lower()] = asset
    missing = set(paths) - set(assets)
    if missing:
        raise ValueError(f"required assets not found: {sorted(missing)}")
    bundles = {bundle["bundleIndex"]: bundle for bundle in manifest["Bundles"]}
    pending = [asset["bundleIndex"] for asset in assets.values()]
    selected: set[int] = set()
    while pending:
        index = pending.pop()
        if index in selected:
            continue
        if index not in bundles:
            raise ValueError(f"missing dependency bundle {index}")
        selected.add(index)
        if len(selected) > 512:
            raise ValueError("asset dependency closure exceeds 512 bundles")
        bundle = bundles[index]
        pending.extend(bundle.get("dependencies", []))
        pending.extend(bundle.get("directDependencies", []))
    names = []
    for index in sorted(selected):
        name = bundles[index]["name"]
        pure = PurePosixPath(name)
        if pure.is_absolute() or ".." in pure.parts or "\\" in name or ":" in name:
            raise ValueError("invalid manifest bundle path")
        names.append("Bundles/Android/" + name)
    return list(assets.values()), names


def read_renderers(by_id: dict[str, dict], obj: dict) -> str | None:
    """Return the full GameObject path of a renderer, or None if the chain is broken."""
    reference = obj.get("game_object") or {}
    if not reference.get("resolved"):
        return None
    game_object = by_id.get(reference["id"])
    if game_object is None:
        return None
    transform = None
    for component in game_object.get("components", []):
        candidate = by_id.get(component["id"])
        if candidate is not None and candidate.get("type") == "Transform":
            transform = candidate
            break
    parts: list[str] = []
    seen: set[str] = set()
    while transform is not None:
        if transform["id"] in seen:
            break
        seen.add(transform["id"])
        owner = by_id.get((transform.get("game_object") or {}).get("id"))
        parts.append(owner.get("name", "?") if owner else "?")
        parent = transform.get("parent") or {}
        transform = by_id.get(parent["id"]) if parent.get("resolved") else None
    if not parts:
        return None
    return "/".join(reversed(parts))


def collect_lod1_renderers(graph_path: Path) -> dict[str, dict[str, Any]]:
    graph = json.loads(graph_path.read_text(encoding="utf-8-sig"))
    objects = graph["objects"]
    by_id = {o["id"]: o for o in objects}
    renderers: dict[str, dict[str, Any]] = {}
    unresolved = 0
    for obj in objects:
        if obj.get("type") not in ("SkinnedMeshRenderer", "MeshRenderer"):
            continue
        path = read_renderers(by_id, obj)
        if path is None:
            unresolved += 1
            continue
        # Renderer paths are absolute within the prefab and start at the prefab
        # root (``chr_xxxx_postmodel/Mesh_all/lod1/...``).  The relation table
        # addresses them from ``Mesh_all/`` down, so drop the root segment.
        marker = path.lower().find(LOD1_PREFIX)
        if marker < 0:
            continue
        path = path[marker:]
        mesh_reference = obj.get("mesh") or {}
        mesh = by_id.get(mesh_reference["id"]) if mesh_reference.get("resolved") else None
        if mesh is None:
            unresolved += 1
            continue
        submeshes = mesh.get("submeshes") or []
        indices = sum(int(s.get("index_count") or 0) for s in submeshes)
        entry = {
            "mesh": mesh.get("name", ""),
            "vertices": int(mesh.get("vertex_count") or 0),
            "indices": indices,
        }
        previous = renderers.get(path)
        if previous is not None and previous != entry:
            raise ValueError(f"conflicting LOD1 identity for {path}: {previous} vs {entry}")
        renderers[path] = entry
    if unresolved:
        print(f"  warning: {unresolved} renderers without a resolved path or mesh")
    return dict(sorted(renderers.items()))


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repo", type=Path, default=Path(__file__).resolve().parents[2])
    parser.add_argument("--character", action="append", required=True,
                        help="Character ID to (re)build, e.g. chr_0038_purrche; repeatable")
    parser.add_argument("--workspace-config", type=Path)
    parser.add_argument("--relation-config", type=Path)
    parser.add_argument("--index", type=Path, help="Index file to update in place")
    parser.add_argument("--work-dir", type=Path, help="Scratch directory for bundles and graphs")
    parser.add_argument("--manifest", type=Path, help="Override the Android manifest.json")
    parser.add_argument("--vfs-root", type=Path, help="Override the Android VFS files root")
    parser.add_argument("--force", action="store_true", help="Re-extract even when a parsed graph exists")
    args = parser.parse_args()
    repo = args.repo.resolve()
    for character in args.character:
        if not re.fullmatch(r"chr_\d{4}_[a-z0-9]+", character):
            parser.error(f"invalid character ID: {character}")

    sys.path.insert(0, str(repo / "scripts"))
    from workspace_config import load_workspace, write_if_changed  # noqa: PLC0415 - repo helper

    workspace = load_workspace(args.workspace_config, repo)
    version = workspace.get("game.version")
    policy_candidates = [
        repo / "config/compatibility/custom_model" / str(version) / "android-lod-relations.json",
        repo / "config/compatibility/custom_model/1.6.3/android-lod-relations.json",
        repo / "config/compatibility/custom_model/1.5.3/android-lod-relations.json",
    ]
    policy_path = args.relation_config or next((p for p in policy_candidates if p.exists()), policy_candidates[0])
    policy = json.loads(policy_path.read_text(encoding="utf-8-sig"))
    source = policy.get("android_source") or {}
    manifest_path = args.manifest or repo / source["manifest"]
    vfs_root = args.vfs_root or repo / source["vfs_root"]
    index_path = args.index or repo / policy["lod1_index"]
    work_root = args.work_dir or repo / "temp/android-lod1-index"

    manifest = json.loads(manifest_path.read_text(encoding="utf-8-sig"))
    unpacker_path = workspace.path("tools.endfield_unpacker_root") / "decrypt_vfs.py"
    reader_path = workspace.path("tools.native_asset_reader")
    helpers = load("native_vfs_helpers", repo / "scripts/RefreshEndfieldResourceInputs.py")
    stub = types.ModuleType("config")
    stub.get_game_dir = lambda: str(repo)
    sys.modules["config"] = stub
    unpacker = load("native_vfs_reader", unpacker_path)
    unpacker.crc32 = lambda data: (zlib.crc32(data) + 2**31) % 2**32 - 2**31

    index: dict[str, Any] = {"schema_version": 1, "characters": {}}
    if index_path.exists():
        index = json.loads(index_path.read_text(encoding="utf-8-sig"))
        if index.get("schema_version") != 1:
            raise ValueError("Unsupported LOD1 index schema")
    index.setdefault("characters", {})
    index["platform"] = policy["platform"]
    index["snapshot"] = policy["snapshot"]
    index["manifest"] = str(manifest_path.relative_to(repo)).replace("\\", "/") if manifest_path.is_relative_to(repo) else str(manifest_path)
    index["manifest_version"] = manifest.get("Version")
    index["note"] = ("Per-character renderer evidence extracted from the Android prefab. "
                     "Rebuild only the characters that are reported broken; others stay as-is.")

    for character in args.character:
        work = work_root / character
        graph_path = work / "graph/raw-graph.json"
        if not graph_path.exists() or args.force:
            print(f"[{character}] resolving bundle closure")
            assets, names = select_bundles(manifest, character)
            bundle_dir = work / "out"
            target = re.compile("(?:" + "|".join(map(re.escape, names)) + ")", re.I)
            records = helpers.extract_overlay(unpacker, [("Android", vfs_root)], bundle_dir, target)
            missing = sorted({n.lower() for n in names} - {r.relative_path.lower() for r in records})
            print(f"[{character}] bundles={len(records)}/{len(names)} missing={len(missing)}")
            if missing:
                raise ValueError(f"{character}: bundles missing from the VFS: {missing[:5]}")
            graph_path.parent.mkdir(parents=True, exist_ok=True)
            result = subprocess.run([str(reader_path), str(bundle_dir / "Bundles/Android"), str(graph_path)],
                                    capture_output=True, text=True)
            if result.returncode != 0:
                raise RuntimeError(f"NativeAssetReader failed for {character}: {result.stderr.strip()}")
            print(f"[{character}] {result.stdout.strip()}")
            (work / "extraction.json").write_text(json.dumps({
                "schema": 1, "character": character, "platform": "Android",
                "manifest_version": manifest.get("Version"), "assets": assets,
                "bundles": [{"path": r.relative_path, "size": r.size} for r in records],
                "missing": missing}, ensure_ascii=False, indent=2), encoding="utf-8")

        renderers = collect_lod1_renderers(graph_path)
        if not renderers:
            raise ValueError(f"{character}: prefab exposes no renderers under {LOD1_PREFIX}")
        index["characters"][character] = {
            "source": "android-prefab",
            "manifest_version": manifest.get("Version"),
            "renderers": renderers,
        }
        print(f"[{character}] LOD1 renderers={len(renderers)}")

    index["characters"] = dict(sorted(index["characters"].items()))
    index_path.parent.mkdir(parents=True, exist_ok=True)
    write_if_changed(index_path, json.dumps(index, ensure_ascii=False, indent=2) + "\n")
    print(f"updated {index_path} with {len(args.character)} character(s); total indexed={len(index['characters'])}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
