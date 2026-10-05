#!/usr/bin/env python3
"""Build a current-version resource snapshot from Endfield's VFS overlay."""

from __future__ import annotations

import argparse
import importlib.util
import json
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

from workspace_config import load_workspace, producer_key, write_if_changed

def configured_path(ws: Any, key: str, fallback: Path) -> Path:
    return ws.path(key, required=False) or fallback


def read_metadata(path: Path) -> dict[str, Any]:
    try:
        value = json.loads(path.read_text(encoding="utf-8-sig"))
        return value if isinstance(value, dict) else {}
    except (OSError, ValueError):
        return {}


def tool_revision(paths: list[Path]) -> str:
    """Hash small source files; use version/stat identity for compiled tools."""
    revisions = []
    for path in paths:
        if not path.is_file():
            raise FileNotFoundError(f"Resource tool not found: {path}")
        stat = path.stat()
        revisions.append({"path": str(path.resolve()), "size": stat.st_size,
                          "revision": sha256(path) if path.suffix == ".py"
                          else stat.st_mtime_ns})
    return producer_key(revisions, "resource-tools-v1", {})


def snapshot_complete(output: Path, metadata: dict[str, Any], key: str) -> bool:
    if metadata.get("producerKey") != key or not metadata.get("files"):
        return False
    for item in metadata["files"]:
        try:
            path = output / item["path"]
            if output.resolve() not in path.resolve().parents or not path.is_file():
                return False
            if path.stat().st_size != item["size"]:
                return False
        except (KeyError, TypeError, OSError):
            return False
    return True


def publish_snapshot(staging: Path, output: Path, temp: Path,
                     obsolete: list[str] | None = None) -> None:
    """Publish changed files only, restoring them if any publish operation fails.

    Back up only files that actually change, in disposable staging. Untracked
    files (including manual overrides) are retained. Metadata is published last.
    """
    output = output.resolve()
    staged = sorted((path for path in staging.rglob("*") if path.is_file()),
                    key=lambda p: (p.name in {"input-snapshot.json", "source-metadata.json"}
                                   or p.parent.name == ".producers", str(p)))
    changes: list[tuple[Path, bytes | None]] = []
    for path in staged:
        destination = output / path.relative_to(staging)
        if output not in destination.resolve().parents:
            raise ValueError(f"Snapshot path escapes output: {destination}")
        changes.append((destination, path.read_bytes()))
    for relative in obsolete or []:
        destination = output / relative
        if output not in destination.resolve().parents:
            raise ValueError(f"Snapshot path escapes output: {destination}")
        if destination.is_file() and not (staging / relative).exists():
            changes.insert(0, (destination, None))
    publish_changes(changes, temp)


def publish_changes(changes: list[tuple[Path, bytes | str | None]], temp: Path) -> None:
    """Apply a prepared batch, backing up only changed existing files."""
    pending: list[tuple[Path, bytes | None]] = []
    for destination, content in changes:
        data = content.encode("utf-8") if isinstance(content, str) else content
        if data is None:
            if destination.is_file():
                pending.append((destination, None))
        elif not destination.is_file() or destination.read_bytes() != data:
            pending.append((destination, data))
    with tempfile.TemporaryDirectory(prefix="resource-rollback-", dir=temp) as saved:
        undo: list[tuple[Path, Path | None]] = []
        try:
            for index, (destination, data) in enumerate(pending):
                backup = Path(saved) / str(index) if destination.is_file() else None
                if backup is not None:
                    shutil.copyfile(destination, backup)
                undo.append((destination, backup))
                if data is None:
                    destination.unlink()
                else:
                    write_if_changed(destination, data)
        except Exception:
            for destination, backup in reversed(undo):
                if backup is None:
                    destination.unlink(missing_ok=True)
                else:
                    write_if_changed(destination, backup.read_bytes())
            raise


def vfs_block_snapshot(game_path: Path, block: str) -> list[dict[str, Any]]:
    """Read only relevant BLC indexes, never hash CHK/game payloads."""
    result = []
    for layer in ("StreamingAssets", "Persistent"):
        root = game_path / "Endfield_Data" / layer / "VFS"
        if not root.is_dir():
            continue
        indexes = [directory / f"{directory.name}.blc" for directory in sorted(root.iterdir())
                   if directory.is_dir()]
        matching = [index for index in indexes if index.stem.casefold().startswith(block.casefold())]
        # Some clients use opaque block directory names. Conservatively bind
        # to their small indexes when a named block cannot be selected.
        for index in matching or indexes:
            if index.is_file():
                result.append({"layer": layer, "block": index.stem,
                               "sha256": sha256(index)})
    if not result:
        raise FileNotFoundError(f"No {block} VFS block indexes found under {game_path}")
    return result


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
    content_md5: str = ""
    file_md5: str = ""


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace-config", type=Path)
    parser.add_argument("--check", action="store_true", help="Report configuration and prerequisite status without reading game resources")
    parser.add_argument("--game-path", type=Path)
    parser.add_argument("--output", type=Path)
    parser.add_argument(
        "--platform", choices=("Windows", "Android")
    )
    parser.add_argument("--streaming-vfs", type=Path)
    parser.add_argument("--persistent-vfs", type=Path)
    parser.add_argument(
        "--resconv",
        type=Path,
    )
    args = parser.parse_args()
    args.workspace = ws = load_workspace(args.workspace_config)
    args.game_path = args.game_path or ws.path("game.install_dir", required=False)
    args.output = args.output or ws.path("resource_update.input_root")
    args.resconv = args.resconv or ws.path("tools.resconv")
    args.platform = args.platform or ws.get("game.platform", "Windows")
    return args


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
                        content_md5=_content_md5.hex().upper(),
                        file_md5=_file_data_md5.hex().upper(),
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


def select_overlay(
    unpacker: Any,
    roots: list[tuple[str, Path]],
    target: re.Pattern[str],
) -> list[VfsRecord]:
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

    return sorted(selected.values(), key=lambda item: item.relative_path.casefold())


def extract_overlay(unpacker: Any, roots: list[tuple[str, Path]], output: Path,
                    target: re.Pattern[str], records: list[VfsRecord] | None = None
                    ) -> list[VfsRecord]:
    records = records if records is not None else select_overlay(unpacker, roots, target)
    chunks = build_chunk_index(roots)
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
        write_if_changed(destination, payload)
    return records


def sha256(path: Path) -> str:
    import hashlib

    digest = hashlib.sha256()
    with path.open("rb") as stream:
        while chunk := stream.read(1024 * 1024):
            digest.update(chunk)
    return digest.hexdigest().upper()


def is_character_prefab(data: dict[str, Any], filename: str) -> bool:
    if data.get("correspondingCharId"):
        return True
    # Some playable prefabs omit correspondingCharId. Require their own
    # character postmodel and an explicit humanoid animation configuration.
    match = re.fullmatch(r"npc_(chr_[0-9]{4}_[a-z0-9]+)\.json", filename, re.I)
    if not match:
        return False
    parts = data.get("partNameIdList", [])
    animation = data.get("cpuAnimationTempletName", "")
    return (
        isinstance(parts, list)
        and f"{match.group(1)}_postmodel" in parts
        and isinstance(animation, str)
        and re.fullmatch(r"NPC/AnimationConfig/Humanoid/[^/]+/[^/]+", animation, re.I)
        is not None
    )


def convert_inputs(
    unpacker_root: Path, resconv: Path, staging: Path, platform: str,
    env: dict[str, str] | None = None,
) -> tuple[dict[str, int], set[str]]:
    manifest = staging / f"Bundles/{platform}/manifest.hgmmap"
    audio_dialog = staging / "TableCfg/AudioDialog.bytes"
    prefab_root = staging / "Json/NPC/PrefabInfo"
    if not manifest.exists() or not audio_dialog.exists():
        raise FileNotFoundError("current manifest or AudioDialog was not extracted")
    if not resconv.exists():
        raise FileNotFoundError(f"ResConv was not found: {resconv}")
    subprocess.run([str(resconv), str(manifest)], check=True, env=env)
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
    write_if_changed(table_output, json.dumps(table, ensure_ascii=False, indent=2) + "\n")

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
        if not is_character_prefab(parsed, source.name):
            excluded_prefabs.add(
                source.relative_to(staging).as_posix().casefold()
            )
            source.unlink()
            continue
        write_if_changed(decoded_root / source.name, result + "\n")
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


def main() -> int:
    args = parse_args()
    ws = args.workspace
    output = args.output.resolve()
    resconv = args.resconv.resolve()
    unpacker_root = configured_path(ws, "tools.endfield_unpacker_root",
                                    ws.path("paths.toolchains", "EndfieldUnpacker"))
    prerequisites = [resconv, *(unpacker_root / name for name in
                               ("decrypt_vfs.py", "decode_sparkbuffer.py", "decode_json_other.py"))]
    if args.check:
        print(json.dumps({"configurationValid": True, "gameVersion": ws.get("game.version"),
                          "resourceSnapshot": ws.get("game.snapshot"), "inputRoot": str(output),
                          "gamePath": str(args.game_path) if args.game_path else None,
                          "gamePathStatus": "available" if args.game_path and args.game_path.is_dir() else "missing",
                          "prerequisites": [{"path": str(path), "status": "available" if path.is_file() else "missing"}
                                            for path in prerequisites], "refreshPerformed": False}, indent=2))
        return 0
    if not args.game_path:
        raise ValueError("Configure game.install_dir or pass --game-path to refresh inputs")
    game_path = args.game_path.resolve()
    missing = [str(path) for path in prerequisites if not path.is_file()]
    if missing:
        raise FileNotFoundError("Resource refresh prerequisites missing: " + ", ".join(missing)
                                + ". Configure tools.resconv/tools.endfield_unpacker_root or pass --resconv; prebuilt tools are required.")
    env = ws.env()
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
            check=False, env=env,
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

    records = select_overlay(unpacker, roots, target_pattern(args.platform))
    revision = tool_revision([Path(__file__), unpacker_root / "decrypt_vfs.py",
                              unpacker_root / "decode_sparkbuffer.py",
                              unpacker_root / "decode_json_other.py", resconv])
    key = producer_key([asdict(item) for item in records], revision,
                       {"platform": args.platform, "roots": [str(root) for _, root in roots],
                        "gameVersion": ws.get("game.version"), "resourceSnapshot": ws.get("game.snapshot"),
                        "toolVersion": ws.get("resource_update.tool_revision", "unknown")})
    previous = read_metadata(output / "input-snapshot.json")
    if snapshot_complete(output, previous, key):
        print(f"Reusing complete input snapshot: {output}")
        return 0
    with tempfile.TemporaryDirectory(prefix="resource-inputs-", dir=ws.path("paths.temp")) as temporary:
        staging = Path(temporary)
        records = extract_overlay(
            unpacker, roots, staging, target_pattern(args.platform), records
        )
        counts, excluded_prefabs = convert_inputs(
            unpacker_root, resconv, staging, args.platform, env
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
            **previous,
            "schemaVersion": 1,
            "producerKey": key,
            "toolRevision": revision,
            "gameVersion": ws.get("game.version"),
            "resourceSnapshot": ws.get("game.snapshot"),
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
        write_if_changed(staging / "input-snapshot.json",
                         json.dumps(snapshot, ensure_ascii=True, indent=2) + "\n")
        publish_snapshot(staging, output, ws.path("paths.temp"),
                         [item["path"] for item in previous.get("files", [])])

    print(
        f"Refreshed {counts['prefabInfoCount']} PrefabInfo records, "
        f"{counts['audioDialogRows']} AudioDialog rows, and "
        f"{counts['manifestAssets']} manifest assets in {output}"
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
