#!/usr/bin/env python3
"""Build a per-language Better Endfield voice catalog from local PCK files.

The catalog contains only the media needed by the selected voice manifest. It
is generated on the user's machine and is never compiled into a module.
"""

from __future__ import annotations

import argparse
import hashlib
import importlib.util
import json
import struct
import sys
import tempfile
from pathlib import Path
from typing import Any


ROOT = Path(__file__).resolve().parents[1]
GENERATOR = ROOT / "scripts" / "GenerateResourceManifests.py"
LANGUAGES = ("Chinese", "English", "Japanese", "Korean")
MAGIC = b"BEVCAT01"
HEADER = struct.Struct("<8sHHIIQQ")
ENTRY = struct.Struct("<IIQII")


def load_generator() -> Any:
    spec = importlib.util.spec_from_file_location("better_endfield_manifest", GENERATOR)
    if spec is None or spec.loader is None:
        raise RuntimeError(f"unable to load {GENERATOR}")
    module = importlib.util.module_from_spec(spec)
    sys.modules[spec.name] = module
    spec.loader.exec_module(module)
    return module


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--game-path", type=Path, required=True)
    parser.add_argument(
        "--package-path",
        type=Path,
        help=(
            "explicit local PCK/CHK payload to read instead of the source path "
            "recorded in the manifest (for another validated client platform)"
        ),
    )
    parser.add_argument(
        "--manifest",
        type=Path,
        default=ROOT / "manifests" / "voice" / "voice-event-media-manifest.json",
    )
    parser.add_argument("--language", choices=LANGUAGES, required=True)
    parser.add_argument("--character-id", default="")
    parser.add_argument("--output", type=Path, required=True)
    return parser.parse_args()


def load_json(path: Path) -> Any:
    with path.open("r", encoding="utf-8-sig") as stream:
        return json.load(stream)


def source_path(game_path: Path, value: str) -> Path:
    path = Path(value)
    return path if path.is_absolute() else game_path / path


def target_package_for(manifest: dict[str, Any], language: str) -> dict[str, Any]:
    target_package = next(
        (
            item
            for item in manifest.get("pckPackages", [])
            if item.get("mappingLanguage") == language
        ),
        None,
    )
    if not target_package:
        raise ValueError(f"no PCK package is recorded for {language}")
    return target_package


def collect_routes(
    manifest: dict[str, Any], language: str, character_id: str = ""
) -> tuple[dict[int, int], int]:
    routes: dict[int, int] = {}
    voice_count = 0
    for voice in manifest.get("voices", []):
        if character_id and voice.get("characterId") != character_id:
            continue
        mappings = {
            mapping.get("language"): mapping
            for mapping in voice.get("languageMappings", [])
            if mapping.get("language") and mapping.get("soundSlots")
        }
        target_mapping = mappings.get(language)
        if not target_mapping:
            continue
        target_slots = {
            int(slot["soundObjectId"]): int(slot["mediaId"])
            for slot in target_mapping.get("soundSlots", [])
            if slot.get("soundObjectId") is not None and slot.get("mediaId")
        }
        if not target_slots:
            continue
        voice_count += 1
        for mapping in mappings.values():
            for slot in mapping.get("soundSlots", []):
                sound_id = int(slot.get("soundObjectId", 0))
                source_id = int(slot.get("mediaId", 0))
                target_id = target_slots.get(sound_id)
                if source_id and target_id and source_id not in routes:
                    routes[source_id] = target_id
    return routes, voice_count


def build_catalog(args: argparse.Namespace) -> dict[str, Any]:
    generator = load_generator()
    manifest = load_json(args.manifest)
    if manifest.get("kind") != "endfield-voice-event-media-manifest":
        raise ValueError("voice manifest kind is not recognized")
    if args.language not in LANGUAGES:
        raise ValueError(f"unsupported target language {args.language}")

    target_package = target_package_for(manifest, args.language)
    target_path = (
        args.package_path.resolve()
        if args.package_path is not None
        else source_path(args.game_path, target_package["source"])
    )
    if not target_path.is_file():
        raise FileNotFoundError(target_path)
    target_index = generator.parse_pck(target_path, args.game_path)
    target_media = {
        entry.file_id & 0xFFFFFFFF: entry
        for entry in target_index.media
        if entry.file_id <= 0xFFFFFFFF
    }

    routes, voice_count = collect_routes(
        manifest, args.language, args.character_id
    )
    missing = 0
    available_routes: dict[int, int] = {}
    for source_id, target_id in routes.items():
        if target_id not in target_media:
            missing += 1
            continue
        available_routes[source_id] = target_id
    if missing:
        raise ValueError(
            f"{missing} target Media IDs are absent from the selected PCK"
        )
    if not available_routes:
        raise ValueError("the requested speaker/language produced no media routes")

    payloads = {
        target_id: generator.read_pck_payload(target_index, target_media[target_id])
        for target_id in sorted(set(available_routes.values()))
    }

    entry_offset = HEADER.size
    data_offset = entry_offset + ENTRY.size * len(available_routes)
    data = bytearray()
    payload_offsets: dict[int, int] = {}
    for target_id, payload in payloads.items():
        payload_offsets[target_id] = data_offset + len(data)
        data.extend(payload)
    packed_entries = bytearray()
    for source_id, target_id in sorted(available_routes.items()):
        payload = payloads[target_id]
        packed_entries.extend(
            ENTRY.pack(
                source_id, target_id, payload_offsets[target_id], len(payload), 0
            )
        )

    header = HEADER.pack(
        MAGIC,
        1,
        LANGUAGES.index(args.language),
        len(available_routes),
        0,
        entry_offset,
        data_offset,
    )
    args.output.parent.mkdir(parents=True, exist_ok=True)
    with tempfile.NamedTemporaryFile(
        mode="wb", dir=args.output.parent, prefix=args.output.name + ".",
        delete=False,
    ) as stream:
        temporary = Path(stream.name)
        stream.write(header)
        stream.write(packed_entries)
        stream.write(data)
    temporary.replace(args.output)

    digest = hashlib.sha256(args.output.read_bytes()).hexdigest().upper()
    report = {
        "schemaVersion": 1,
        "kind": "betterendfield-voice-catalog",
        "targetLanguage": args.language,
        "characterId": args.character_id or "*",
        "entryCount": len(available_routes),
        "uniqueTargetMediaCount": len(payloads),
        "payloadBytes": len(data),
        "voiceCount": voice_count,
        "missingTargetMediaCount": missing,
        "catalogSha256": digest,
        "sourceManifest": str(args.manifest),
        "sourcePackage": str(target_path),
    }
    args.output.with_suffix(args.output.suffix + ".json").write_text(
        json.dumps(report, ensure_ascii=True, indent=2) + "\n",
        encoding="utf-8",
        newline="\n",
    )
    return report


def main() -> int:
    args = parse_args()
    report = build_catalog(args)
    print(json.dumps(report, ensure_ascii=True, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
