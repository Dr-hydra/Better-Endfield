#!/usr/bin/env python3
"""Build a current-version resource snapshot from Endfield's VFS overlay."""

from __future__ import annotations

import argparse
import importlib.util
import json
import os
import re
import shutil
import subprocess
import sys
import tempfile
import types
import zlib
from dataclasses import asdict, dataclass
from pathlib import Path
from typing import Any


REPO_ROOT = Path(__file__).resolve().parents[1]
DEFAULT_GAME = Path(r"E:\Endfield Game")
DEFAULT_OUTPUT = REPO_ROOT / "research/current-inputs"


def target_pattern(platform: str) -> re.Pattern[str]:
    return re.compile(
        rf"^(?:"
        rf"Bundles/{re.escape(platform)}/manifest\.hgmmap|"
        r"TableCfg/AudioDialog\.bytes|"
        r"Json/NPC/PrefabInfo/npc_chr_[0-9]{4}_[a-z0-9]+\.json"
        r")$",
        re.IGNORECASE,
    )


@dataclass
class VfsRecord:
    relative_path: str
    layer: str
    block: str
    chunk: str
    offset: int
    size: int
    encrypted: bool
    iv_seed: int


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--game-path", type=Path, default=DEFAULT_GAME)
    parser.add_argument("--output", type=Path, default=DEFAULT_OUTPUT)
    parser.add_argument(
        "--platform", choices=("Windows", "Android"), default="Windows"
    )
    parser.add_argument("--streaming-vfs", type=Path)
    parser.add_argument("--persistent-vfs", type=Path)
    parser.add_argument(
        "--resconv",
        type=Path,
        default=(
            REPO_ROOT
            / "tools/FkArkEnd/ResConv/bin/Release/net10.0/ResConv.exe"
        ),
    )
    return parser.parse_args()


def load_module(name: str, path: Path) -> Any:
    spec = importlib.util.spec_from_file_location(name, path)
    if spec is None or spec.loader is None:
        raise RuntimeError(f"cannot load Python module {path}")
    module = importlib.util.module_from_spec(spec)
    sys.modules[name] = module
    spec.loader.exec_module(module)
    return module


def normalize_vfs_path(value: str) -> str:
    normalized = re.sub(r"[^\x20-\x7e/\\]", "", value).replace("\\", "/")
    match = re.search(r"(Data/|Assets/)[A-Za-z0-9_./-]+", normalized)
    if match:
        normalized = match.group(0)
    for prefix in ("Assets/StreamingAssets/", "Assets/", "Data/"):
        if normalized.startswith(prefix):
            normalized = normalized[len(prefix) :]
            break
    return normalized


def parse_blc_records(
    unpacker: Any,
    blc_path: Path,
    layer: str,
    target: re.Pattern[str],
) -> list[VfsRecord]:
    plain = unpacker.decrypt_blc(str(blc_path))
    offset = 0
    raw_version, offset = unpacker.read_i32(plain, offset)
    if raw_version < 11:
        code_version = raw_version
        _version, offset = unpacker.read_i32(plain, offset)
    else:
        code_version = 3

    name_length, offset = unpacker.read_u16(plain, offset)
    block_name, offset = unpacker.read_string(plain, offset, name_length)
    _directory_hash, offset = unpacker.read_i64(plain, offset)
    _file_count, offset = unpacker.read_i32(plain, offset)
    _chunks_length, offset = unpacker.read_i64(plain, offset)
    _block_type, offset = unpacker.read_u8(plain, offset)
    chunk_count, offset = unpacker.read_i32(plain, offset)

    result: list[VfsRecord] = []
    for _ in range(chunk_count):
        chunk_md5, offset = unpacker.read_u128(plain, offset)
        _content_md5, offset = unpacker.read_u128(plain, offset)
        _length, offset = unpacker.read_i64(plain, offset)
        _chunk_type, offset = unpacker.read_u8(plain, offset)
        if code_version > 3:
            _main_tag, offset = unpacker.read_i32(plain, offset)
        files_in_chunk, offset = unpacker.read_i32(plain, offset)
        chunk = chunk_md5.hex().upper()
        for _ in range(files_in_chunk):
            filename_length, offset = unpacker.read_u16(plain, offset)
            filename, offset = unpacker.read_string(
                plain, offset, filename_length
            )
            _filename_hash, offset = unpacker.read_i64(plain, offset)
            _file_chunk_md5, offset = unpacker.read_u128(plain, offset)
            _file_data_md5, offset = unpacker.read_u128(plain, offset)
            file_offset, offset = unpacker.read_i64(plain, offset)
            file_size, offset = unpacker.read_i64(plain, offset)
            _file_type, offset = unpacker.read_u8(plain, offset)
            encrypted_value, offset = unpacker.read_u8(plain, offset)
            encrypted = encrypted_value != 0
            iv_seed = 0
            if encrypted:
                iv_seed, offset = unpacker.read_i64(plain, offset)
            if code_version > 3:
                _file_tag, offset = unpacker.read_i32(plain, offset)
            relative_path = normalize_vfs_path(filename)
            if target.fullmatch(relative_path):
                result.append(
                    VfsRecord(
                        relative_path=relative_path,
                        layer=layer,
                        block=block_name,
                        chunk=chunk,
                        offset=file_offset,
                        size=file_size,
                        encrypted=encrypted,
                        iv_seed=iv_seed,
                    )
                )
    return result


def build_chunk_index(roots: list[tuple[str, Path]]) -> dict[str, Path]:
    chunks: dict[str, Path] = {}
    for _layer, root in roots:
        if not root.exists():
            continue
        for path in root.glob("*/*.chk"):
            chunks[path.stem.upper()] = path.resolve()
    return chunks


def extract_overlay(
    unpacker: Any,
    roots: list[tuple[str, Path]],
    output: Path,
    target: re.Pattern[str],
) -> list[VfsRecord]:
    chunks = build_chunk_index(roots)
    selected: dict[str, VfsRecord] = {}
    for layer, root in roots:
        if not root.exists():
            continue
        for directory in sorted(root.iterdir(), key=lambda item: item.name):
            if not directory.is_dir():
                continue
            blc = directory / f"{directory.name}.blc"
            if not blc.exists():
                continue
            for record in parse_blc_records(unpacker, blc, layer, target):
                selected[record.relative_path.casefold()] = record

    records = sorted(selected.values(), key=lambda item: item.relative_path.casefold())
    for record in records:
        chunk_path = chunks.get(record.chunk)
        if chunk_path is None:
            raise FileNotFoundError(
                f"CHK {record.chunk} for {record.relative_path} is unavailable"
            )
        if record.offset < 0 or record.size <= 0:
            raise ValueError(f"invalid VFS range for {record.relative_path}")
        if record.offset + record.size > chunk_path.stat().st_size:
            raise ValueError(f"VFS range exceeds CHK for {record.relative_path}")
        with chunk_path.open("rb") as stream:
            stream.seek(record.offset)
            payload = stream.read(record.size)
        if len(payload) != record.size:
            raise ValueError(f"short read for {record.relative_path}")
        if record.encrypted:
            payload = unpacker.per_file_decrypt(payload, record.iv_seed)
        destination = output / Path(record.relative_path)
        destination.parent.mkdir(parents=True, exist_ok=True)
        destination.write_bytes(payload)
    return records


def sha256(path: Path) -> str:
    import hashlib

    digest = hashlib.sha256()
    with path.open("rb") as stream:
        while chunk := stream.read(1024 * 1024):
            digest.update(chunk)
    return digest.hexdigest().upper()


def convert_inputs(
    unpacker_root: Path, resconv: Path, staging: Path, platform: str
) -> tuple[dict[str, int], set[str]]:
    manifest = staging / f"Bundles/{platform}/manifest.hgmmap"
    audio_dialog = staging / "TableCfg/AudioDialog.bytes"
    prefab_root = staging / "Json/NPC/PrefabInfo"
    if not manifest.exists() or not audio_dialog.exists():
        raise FileNotFoundError("current manifest or AudioDialog was not extracted")
    if not resconv.exists():
        raise FileNotFoundError(f"ResConv was not found: {resconv}")
    subprocess.run([str(resconv), str(manifest)], check=True)
    manifest_json = manifest.with_suffix(".json")
    manifest_data = json.loads(manifest_json.read_text(encoding="utf-8-sig"))
    if not manifest_data.get("Assets") or not manifest_data.get("Bundles"):
        raise ValueError("converted manifest is empty")

    spark = load_module("ef_manifest_spark", unpacker_root / "decode_sparkbuffer.py")
    table_name, table = spark.parse_sparkbuffer(audio_dialog.read_bytes())
    if table_name != "AudioDialog" or not isinstance(table, dict):
        raise ValueError("AudioDialog SparkBuffer decode failed")
    table_output = staging / "Table/AudioDialog.json"
    table_output.parent.mkdir(parents=True, exist_ok=True)
    table_output.write_text(
        json.dumps(table, ensure_ascii=False, indent=2) + "\n",
        encoding="utf-8",
        newline="\n",
    )

    decoder = load_module("ef_manifest_json", unpacker_root / "decode_json_other.py")
    decoded_root = staging / "Json_decrypted/NPC/PrefabInfo"
    decoded_root.mkdir(parents=True, exist_ok=True)
    prefab_count = 0
    excluded_prefabs: set[str] = set()
    for source in sorted(prefab_root.glob("npc_chr_*.json")):
        result, _ = decoder.decode_file(source.read_bytes())
        parsed = json.loads(result)
        if not isinstance(parsed, dict):
            raise ValueError(f"PrefabInfo decode failed: {source.name}")
        if not parsed.get("correspondingCharId"):
            excluded_prefabs.add(
                source.relative_to(staging).as_posix().casefold()
            )
            source.unlink()
            continue
        (decoded_root / source.name).write_text(
            result + "\n", encoding="utf-8", newline="\n"
        )
        prefab_count += 1
    if prefab_count < 30:
        raise ValueError(f"only {prefab_count} playable PrefabInfo files decoded")
    return (
        {
            "manifestAssets": len(manifest_data["Assets"]),
            "manifestBundles": len(manifest_data["Bundles"]),
            "audioDialogRows": len(table),
            "prefabInfoCount": prefab_count,
        },
        excluded_prefabs,
    )


def replace_output(staging: Path, output: Path) -> None:
    output.parent.mkdir(parents=True, exist_ok=True)
    backup = output.with_name(f".{output.name}.previous")
    if backup.exists():
        shutil.rmtree(backup)
    if output.exists():
        os.replace(output, backup)
    try:
        os.replace(staging, output)
    except Exception:
        if backup.exists() and not output.exists():
            os.replace(backup, output)
        raise
    if backup.exists():
        shutil.rmtree(backup)


def main() -> int:
    args = parse_args()
    game_path = args.game_path.resolve()
    output = args.output.resolve()
    resconv = args.resconv.resolve()
    unpacker_root = REPO_ROOT / "tools/EndfieldUnpacker"
    try:
        import Crypto  # noqa: F401
    except ModuleNotFoundError:
        bundled_python = unpacker_root / ".venv/Scripts/python.exe"
        if not bundled_python.exists():
            raise RuntimeError(
                "pycryptodome is unavailable and EndfieldUnpacker's bundled "
                f"Python was not found: {bundled_python}"
            )
        return subprocess.run(
            [str(bundled_python), str(Path(__file__).resolve()), *sys.argv[1:]],
            check=False,
        ).returncode
    sys.path.insert(0, str(unpacker_root))
    config_stub = types.ModuleType("config")
    config_stub.get_game_dir = lambda: str(game_path)
    sys.modules["config"] = config_stub
    unpacker = load_module("ef_manifest_vfs", unpacker_root / "decrypt_vfs.py")
    # The source script reads a signed CRC but returns an unsigned CRC.  Match
    # representations so valid BLC files do not produce false warnings.
    unpacker.crc32 = lambda data: (
        (zlib.crc32(data) + 2**31) % 2**32 - 2**31
    )

    roots = [
        (
            "StreamingAssets",
            (args.streaming_vfs or game_path / "Endfield_Data/StreamingAssets/VFS").resolve(),
        ),
        (
            "Persistent",
            (args.persistent_vfs or game_path / "Endfield_Data/Persistent/VFS").resolve(),
        ),
    ]
    if not any(root.exists() for _layer, root in roots):
        raise FileNotFoundError(f"Endfield VFS was not found under {game_path}")

    output.parent.mkdir(parents=True, exist_ok=True)
    staging = Path(
        tempfile.mkdtemp(prefix=f".{output.name}.staging-", dir=output.parent)
    )
    try:
        records = extract_overlay(
            unpacker, roots, staging, target_pattern(args.platform)
        )
        counts, excluded_prefabs = convert_inputs(
            unpacker_root, resconv, staging, args.platform
        )
        records = [
            record
            for record in records
            if record.relative_path.casefold() not in excluded_prefabs
        ]
        generated_files = sorted(
            path for path in staging.rglob("*") if path.is_file()
        )
        snapshot = {
            "schemaVersion": 1,
            "platform": args.platform,
            "overlayOrder": [layer for layer, _root in roots],
            "counts": counts,
            "records": [asdict(item) for item in records],
            "files": [
                {
                    "path": path.relative_to(staging).as_posix(),
                    "size": path.stat().st_size,
                    "sha256": sha256(path),
                }
                for path in generated_files
            ],
        }
        (staging / "input-snapshot.json").write_text(
            json.dumps(snapshot, ensure_ascii=True, indent=2) + "\n",
            encoding="utf-8",
            newline="\n",
        )
        replace_output(staging, output)
    except Exception:
        if staging.exists():
            shutil.rmtree(staging)
        raise

    print(
        f"Refreshed {counts['prefabInfoCount']} PrefabInfo records, "
        f"{counts['audioDialogRows']} AudioDialog rows, and "
        f"{counts['manifestAssets']} manifest assets in {output}"
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
