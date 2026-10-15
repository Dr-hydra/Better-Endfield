"""Read-only, bounded Snowshine prefab closure from the current game overlay.

Writes only this investigation's current/inputs directory. Does not calculate
artifact hashes, decode texture pixels, modify the game, or start game code.
"""
import ctypes
import json
from pathlib import Path
import re
import sys
import types
import zlib

ROOT = Path(__file__).resolve().parents[5]
HERE = Path(__file__).resolve().parent
GAME = Path(r"G:\Arknights Endfield")
REFERENCE = ROOT / "research/custom_model/1.6.3/Windows/runtime-catalog-update/minghe-shared-inputs/Bundles/Windows/manifest.hgmmap"
OUT = HERE / "current/inputs"


def main():
    ctypes.windll.kernel32.SetPriorityClass(ctypes.windll.kernel32.GetCurrentProcess(), 0x4000)
    if OUT.exists() and any(OUT.iterdir()):
        raise RuntimeError("Use an empty current/inputs directory; no stale payloads are overwritten.")
    if OUT.resolve() == GAME.resolve() or OUT.resolve().is_relative_to(GAME.resolve()):
        raise RuntimeError("Output must be outside the game installation.")
    sys.path.insert(0, str(ROOT / "tools/CustomModel"))
    sys.path.insert(0, str(ROOT / "scripts"))
    import extract_native_bundles as extractor
    helper = extractor.load("snowshine_vfs_helpers", ROOT / "scripts/RefreshEndfieldResourceInputs.py")
    stub = types.ModuleType("config")
    stub.get_game_dir = lambda: str(GAME)
    sys.modules["config"] = stub
    unpacker = extractor.load("snowshine_vfs", ROOT / "toolchains/EndfieldUnpacker/decrypt_vfs.py")
    unpacker.crc32 = lambda data: (zlib.crc32(data) + 2**31) % 2**32 - 2**31
    manifest = json.loads(REFERENCE.with_suffix(".json").read_text(encoding="utf-8-sig"))
    assets, names = extractor.select_bundles(manifest, "chr_0014_aurora")
    version, perforce = manifest.get("Version"), manifest.get("perforceCL")
    del manifest
    roots = [("StreamingAssets", GAME / "Endfield_Data/StreamingAssets/VFS"),
             ("Persistent", GAME / "Endfield_Data/Persistent/VFS")]
    manifest_name = "Bundles/Windows/manifest.hgmmap"
    pattern = re.compile("^(?:" + "|".join(map(re.escape, names + [manifest_name])) + ")$", re.I)
    records = helper.select_overlay(unpacker, roots, pattern)
    manifests = [record for record in records if record.relative_path.lower() == manifest_name.lower()]
    if len(manifests) != 1:
        raise RuntimeError("Current effective Windows manifest is absent or ambiguous.")
    current = manifests[0]
    chunks = helper.build_chunk_index(roots)
    with chunks[current.chunk].open("rb") as stream:
        stream.seek(current.offset)
        payload = stream.read(current.size)
    if len(payload) != current.size:
        raise RuntimeError("Current manifest read is incomplete.")
    if current.encrypted:
        payload = unpacker.per_file_decrypt(payload, current.iv_seed)
    equal = payload == REFERENCE.read_bytes()
    del payload
    if not equal:
        raise RuntimeError("Current manifest differs from the reused JSON's source; refresh only that manifest before extracting.")
    selected = [record for record in records if record.relative_path.lower() != manifest_name.lower()]
    missing = {name.lower() for name in names} - {record.relative_path.lower() for record in selected}
    if missing:
        raise RuntimeError(f"Current overlay is missing selected bundles: {sorted(missing)}")
    total = sum(record.size for record in selected)
    print(f"current_manifest_matches_reference=True bundles={len(selected)} bytes={total} MiB={total / 1048576:.2f}", flush=True)
    OUT.mkdir(parents=True, exist_ok=True)
    helper.extract_overlay(unpacker, roots, OUT, pattern, records=selected)
    snapshot = {"schema": 1, "character": "chr_0014_aurora", "game": str(GAME), "assets": assets,
                "manifest_version": version, "perforce_cl": perforce,
                "manifest_reference": str(REFERENCE), "current_manifest_byte_equal": equal,
                "current_manifest_layer": current.layer,
                "bundles": [{"path": r.relative_path, "layer": r.layer, "size": r.size,
                             "block": r.block, "chunk": r.chunk, "offset": r.offset} for r in selected],
                "missing": [], "artifact_hashes_computed": False}
    (OUT / "extraction.json").write_text(json.dumps(snapshot, ensure_ascii=False, indent=2), encoding="utf-8")
    print(f"Extracted {len(selected)} selected bundles to {OUT}", flush=True)


if __name__ == "__main__":
    main()
