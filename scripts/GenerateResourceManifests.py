#!/usr/bin/env python3
"""Generate deterministic Endfield action and voice mapping manifests.

The action manifest is normalized from the catalog embedded by the UI.  The
voice manifest hashes AudioDialog ``*_sv`` paths with the Wwise FNV-1 algorithm,
matches the resulting event IDs to per-event BNK files, and extracts media IDs
from validated HIRC Sound objects.  PCK files are indexed only; WEM payloads are
never extracted or written.
"""

from __future__ import annotations

import argparse
import hashlib
import json
import re
import struct
from collections import Counter
from dataclasses import dataclass
from pathlib import Path
from typing import Any, Iterable


GENERATOR_VERSION = 2
MUL_CONST = 81861667
XOR_CONST = 0x9C5A0B29
VOICE_ID = re.compile(
    r"^(chr_\d{4}_[a-z0-9]+)_.+_sv(?:_[a-z0-9]+)*$", re.IGNORECASE
)
PCK_MAGICS = (b"AKPK", b":)xD")
LANGUAGE_NAMES = {
    "c": "Chinese",
    "e": "English",
    "j": "Japanese",
    "k": "Korean",
}

REPO_ROOT = Path(__file__).resolve().parents[1]
DEFAULT_ACTIONS = REPO_ROOT / "research/current-inputs/character-presets.json"
DEFAULT_AUDIO_DIALOG = REPO_ROOT / "research/current-inputs/Table/AudioDialog.json"
DEFAULT_BNK_DIR = REPO_ROOT / "research/bank-pck/bnk"
DEFAULT_VOICE_ALIASES = REPO_ROOT / "resources/voice/voice-event-aliases.json"
DEFAULT_INPUT_SNAPSHOT = REPO_ROOT / "research/current-inputs/input-snapshot.json"
DEFAULT_OUTPUT = REPO_ROOT / "manifests"


@dataclass(frozen=True)
class PckEntry:
    file_id: int
    offset: int
    size: int
    language: str | None
    kind: str


@dataclass
class PckIndex:
    path: Path
    source: str
    size: int
    header_size: int
    header_sha256: str
    languages: dict[int, str]
    banks: list[PckEntry]
    media: list[PckEntry]


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--actions", type=Path, default=DEFAULT_ACTIONS)
    parser.add_argument("--audio-dialog", type=Path, default=DEFAULT_AUDIO_DIALOG)
    parser.add_argument("--voice-aliases", type=Path, default=DEFAULT_VOICE_ALIASES)
    parser.add_argument("--input-snapshot", type=Path, default=DEFAULT_INPUT_SNAPSHOT)
    parser.add_argument("--bnk-dir", type=Path, default=DEFAULT_BNK_DIR)
    parser.add_argument("--game-path", type=Path)
    parser.add_argument(
        "--pck", type=Path, action="append", default=[],
        help="Explicit raw CHK or extracted PCK to index; may be repeated.",
    )
    parser.add_argument(
        "--no-pck-discovery", action="store_true",
        help="Do not scan the game VFS for PCK headers.",
    )
    parser.add_argument("--output-dir", type=Path, default=DEFAULT_OUTPUT)
    return parser.parse_args()


def load_json(path: Path) -> Any:
    with path.open("r", encoding="utf-8-sig") as stream:
        return json.load(stream)


def write_json(path: Path, value: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", encoding="utf-8", newline="\n") as stream:
        json.dump(value, stream, ensure_ascii=True, indent=2)
        stream.write("\n")


def sha256_file(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as stream:
        while chunk := stream.read(1024 * 1024):
            digest.update(chunk)
    return digest.hexdigest().upper()


def sha256_bytes(value: bytes | bytearray) -> str:
    return hashlib.sha256(value).hexdigest().upper()


def wwise_id(value: str) -> int:
    """Wwise GetIDFromString: lowercase UTF-8 FNV-1, not FNV-1a."""
    result = 2166136261
    for byte in value.casefold().encode("utf-8"):
        result = ((result * 16777619) & 0xFFFFFFFF) ^ byte
    return result


def derive_key(seed: int) -> int:
    key = ((seed & 0xFF) ^ XOR_CONST) * MUL_CONST & 0xFFFFFFFF
    key = (key ^ ((seed >> 8) & 0xFF)) * MUL_CONST & 0xFFFFFFFF
    key = (key ^ ((seed >> 16) & 0xFF)) * MUL_CONST & 0xFFFFFFFF
    return (key ^ ((seed >> 24) & 0xFF)) * MUL_CONST & 0xFFFFFFFF


def decrypt_vfs(data: bytearray, start: int, length: int, seed: int) -> None:
    position = start
    remaining = length
    key_index = seed & 0xFFFFFFFF
    while remaining >= 4:
        value = struct.unpack_from("<I", data, position)[0] ^ derive_key(key_index)
        struct.pack_into("<I", data, position, value)
        position += 4
        remaining -= 4
        key_index = (key_index + 1) & 0xFFFFFFFF
    if remaining:
        key = derive_key(key_index)
        for index in range(remaining):
            data[position + index] ^= (key >> (index * 8)) & 0xFF


def read_u32(data: bytes | bytearray, offset: int) -> int:
    return struct.unpack_from("<I", data, offset)[0]


def read_u64(data: bytes | bytearray, offset: int) -> int:
    return struct.unpack_from("<Q", data, offset)[0]


def source_ref(path: Path, game_path: Path | None) -> str:
    resolved = path.resolve()
    if game_path:
        try:
            return resolved.relative_to(game_path.resolve()).as_posix()
        except ValueError:
            pass
    try:
        return resolved.relative_to(REPO_ROOT).as_posix()
    except ValueError:
        return resolved.name


def parse_languages(
    data: bytes | bytearray, start: int, size: int,
) -> dict[int, str]:
    if size < 4 or start + size > len(data):
        raise ValueError("invalid PCK language sector")
    count = read_u32(data, start)
    position = start + 4
    result: dict[int, str] = {}
    for _ in range(count):
        if position + 8 > start + size:
            raise ValueError("truncated PCK language record")
        name_offset = read_u32(data, position)
        language_id = read_u32(data, position + 4)
        position += 8
        string_start = start + name_offset
        string_end = data.find(b"\0", string_start, start + size)
        if string_end < 0:
            raise ValueError("unterminated PCK language name")
        result[language_id] = bytes(data[string_start:string_end]).decode(
            "utf-8", errors="replace"
        )
    return result


def parse_pck_sector(
    data: bytes | bytearray,
    start: int,
    size: int,
    kind: str,
    languages: dict[int, str],
    external: bool = False,
) -> list[PckEntry]:
    if not size:
        return []
    if size < 4 or start + size > len(data):
        raise ValueError(f"invalid PCK {kind} sector")
    count = read_u32(data, start)
    if not count:
        return []
    payload_size = size - 4
    if payload_size % count:
        raise ValueError(f"unaligned PCK {kind} sector")
    entry_size = payload_size // count
    if entry_size not in (20, 24):
        raise ValueError(f"unsupported PCK {kind} entry size {entry_size}")
    result: list[PckEntry] = []
    position = start + 4
    for _ in range(count):
        file_id = read_u32(data, position)
        cursor = position + 4
        if entry_size == 24 and external:
            file_id |= read_u32(data, cursor) << 32
            cursor += 4
        block_size = read_u32(data, cursor)
        cursor += 4
        if entry_size == 24 and not external:
            file_size = read_u64(data, cursor)
            cursor += 8
        else:
            file_size = read_u32(data, cursor)
            cursor += 4
        file_offset = read_u32(data, cursor)
        language_id = read_u32(data, cursor + 4)
        if block_size:
            file_offset *= block_size
        result.append(
            PckEntry(
                file_id=file_id,
                offset=file_offset,
                size=file_size,
                language=languages.get(language_id),
                kind=kind,
            )
        )
        position += entry_size
    return result


def parse_pck(path: Path, game_path: Path | None) -> PckIndex:
    with path.open("rb") as stream:
        prefix = stream.read(12)
        if len(prefix) != 12 or prefix[:4] not in PCK_MAGICS:
            raise ValueError("not an AKPK package")
        header_size = read_u32(prefix, 4)
        if header_size < 16 or header_size > 64 * 1024 * 1024:
            raise ValueError(f"unsupported PCK header size {header_size}")
        stream.seek(0)
        header = bytearray(stream.read(header_size + 8))
    if len(header) != header_size + 8:
        raise ValueError("truncated PCK header")
    if header[:4] == b":)xD":
        decrypt_vfs(header, 12, header_size - 4, header_size)
        header[:4] = b"AKPK"
        struct.pack_into("<I", header, 8, 1)
    if header[:4] != b"AKPK":
        raise ValueError("PCK header decryption failed")

    position = 4
    parsed_header_size = read_u32(header, position)
    position += 4
    _flags = read_u32(header, position)
    position += 4
    languages_size = read_u32(header, position)
    position += 4
    banks_size = read_u32(header, position)
    position += 4
    sounds_size = read_u32(header, position)
    position += 4
    externals_size = 0
    if languages_size + banks_size + sounds_size + 0x10 < parsed_header_size:
        externals_size = read_u32(header, position)
        position += 4

    languages = parse_languages(header, position, languages_size)
    position += languages_size
    banks = parse_pck_sector(header, position, banks_size, "bank", languages)
    position += banks_size
    media = parse_pck_sector(header, position, sounds_size, "sound", languages)
    position += sounds_size
    media.extend(
        parse_pck_sector(
            header, position, externals_size, "external", languages, external=True
        )
    )
    return PckIndex(
        path=path.resolve(),
        source=source_ref(path, game_path),
        size=path.stat().st_size,
        header_size=len(header),
        header_sha256=sha256_bytes(header),
        languages=languages,
        banks=banks,
        media=media,
    )


def discover_pck_paths(game_path: Path) -> list[Path]:
    roots = [
        game_path / "Endfield_Data/Persistent/VFS",
        game_path / "Endfield_Data/StreamingAssets/VFS",
    ]
    result: list[Path] = []
    for root in roots:
        if not root.exists():
            continue
        for path in root.rglob("*.chk"):
            try:
                with path.open("rb") as stream:
                    prefix = stream.read(8)
                if len(prefix) == 8 and prefix[:4] in PCK_MAGICS:
                    header_size = read_u32(prefix, 4)
                    if 16 <= header_size <= 64 * 1024 * 1024:
                        result.append(path.resolve())
            except OSError:
                continue
    return sorted(set(result), key=lambda item: str(item).casefold())


def read_pck_payload(index: PckIndex, entry: PckEntry) -> bytes:
    with index.path.open("rb") as stream:
        stream.seek(entry.offset)
        payload = bytearray(stream.read(entry.size))
    if len(payload) != entry.size:
        raise ValueError(f"truncated PCK entry {entry.file_id}")
    decrypt_vfs(payload, 0, len(payload), entry.file_id & 0xFFFFFFFF)
    return bytes(payload)


def iter_chunks(data: bytes) -> Iterable[tuple[bytes, bytes]]:
    position = 0
    while position + 8 <= len(data):
        tag = data[position : position + 4]
        size = read_u32(data, position + 4)
        start = position + 8
        end = start + size
        if end > len(data):
            raise ValueError(f"truncated {tag!r} chunk")
        yield tag, data[start:end]
        position = end
    if position != len(data):
        raise ValueError("trailing BNK bytes")


def parse_hirc_bank(data: bytes, expected_event_id: int) -> dict[str, Any]:
    chunks = {tag: body for tag, body in iter_chunks(data)}
    hirc = chunks.get(b"HIRC")
    if hirc is None or len(hirc) < 4:
        raise ValueError("BNK has no HIRC chunk")
    count = read_u32(hirc, 0)
    position = 4
    object_counts: Counter[int] = Counter()
    event_ids: list[int] = []
    objects: dict[int, tuple[int, bytes]] = {}
    for _ in range(count):
        if position + 5 > len(hirc):
            raise ValueError("truncated HIRC object header")
        object_type = hirc[position]
        object_size = read_u32(hirc, position + 1)
        body_start = position + 5
        body_end = body_start + object_size
        body = hirc[body_start:body_end]
        if len(body) != object_size or object_size < 4:
            raise ValueError("truncated HIRC object")
        object_id = read_u32(body, 0)
        if object_id in objects:
            raise ValueError(f"duplicate HIRC object ID {object_id}")
        objects[object_id] = (object_type, body)
        object_counts[object_type] += 1
        if object_type == 4:
            event_ids.append(object_id)
        position = body_end
    if position != len(hirc):
        raise ValueError("HIRC object table size mismatch")
    if event_ids != [expected_event_id]:
        raise ValueError(
            f"expected one Event {expected_event_id}, found {event_ids}"
        )
    object_ids = set(objects)
    references: dict[int, set[int]] = {}
    for object_id, (_object_type, body) in objects.items():
        references[object_id] = {
            read_u32(body, offset)
            for offset in range(4, len(body) - 3)
            if read_u32(body, offset) in object_ids
        }
    reachable: set[int] = set()
    pending = [expected_event_id]
    while pending:
        object_id = pending.pop()
        if object_id in reachable:
            continue
        reachable.add(object_id)
        pending.extend(references[object_id] - reachable)

    media_ids: list[int] = []
    sound_slots: list[dict[str, int]] = []
    reachable_sound_count = 0
    for object_id in reachable:
        object_type, body = objects[object_id]
        if object_type != 2:
            continue
        reachable_sound_count += 1
        # Wwise v150 Sound: sourceID begins at byte 9 of the HIRC body.
        if len(body) < 13:
            raise ValueError("short HIRC Sound object")
        media_id = read_u32(body, 9)
        media_ids.append(media_id)
        sound_slots.append({"soundObjectId": object_id, "mediaId": media_id})
    if not media_ids or any(not item for item in media_ids):
        raise ValueError("HIRC Event graph has no valid Sound media IDs")
    normalized_graph = bytearray()
    for object_id in sorted(reachable):
        object_type, body = objects[object_id]
        # Sound payload metadata (Media ID, byte size, and language-specific
        # make-up gain) legitimately differs between localized banks.  The
        # object ID is the stable leaf key; all routing objects retain their
        # complete bytes so playlist order, weights, modes, and flags are part
        # of the fingerprint.
        normalized_body = b"" if object_type == 2 else body
        normalized_graph.extend(
            struct.pack("<BII", object_type, object_id, len(normalized_body))
        )
        normalized_graph.extend(normalized_body)
    return {
        "mediaIds": sorted(set(media_ids)),
        "soundSlots": sorted(sound_slots, key=lambda item: item["soundObjectId"]),
        "routingStructureSha256": sha256_bytes(normalized_graph),
        "objectGraph": [
            {
                "objectId": object_id,
                "objectType": objects[object_id][0],
                "references": sorted(references[object_id]),
            }
            for object_id in sorted(reachable)
        ],
        "soundObjectCount": reachable_sound_count,
        "reachableObjectCount": len(reachable),
        "hircObjectCount": count,
        "hircObjectTypes": {
            str(key): object_counts[key] for key in sorted(object_counts)
        },
    }


def build_native_media_routes(
    language_mappings: list[dict[str, Any]],
) -> dict[str, Any]:
    mapped = {
        item["language"]: item
        for item in language_mappings
        if item.get("bankStatus") == "mapped"
    }
    required = tuple(LANGUAGE_NAMES.values())
    if any(language not in mapped for language in required):
        return {"status": "fallback", "reason": "not-mapped-in-all-languages"}
    fingerprints = {
        mapped[language].get("routingStructureSha256") for language in required
    }
    if len(fingerprints) != 1 or None in fingerprints:
        return {"status": "fallback", "reason": "routing-structure-mismatch"}
    slots_by_language = {
        language: {
            int(slot["soundObjectId"]): int(slot["mediaId"])
            for slot in mapped[language].get("soundSlots", [])
        }
        for language in required
    }
    object_ids = set(slots_by_language[required[0]])
    if not object_ids or any(
        set(slots_by_language[language]) != object_ids for language in required[1:]
    ):
        return {"status": "fallback", "reason": "sound-object-set-mismatch"}
    slots = [
        {
            "soundObjectId": object_id,
            "mediaIds": {
                language: slots_by_language[language][object_id]
                for language in required
            },
        }
        for object_id in sorted(object_ids)
    ]
    return {
        "status": "mapped",
        "routingStructureSha256": next(iter(fingerprints)),
        "slots": slots,
    }


def localized_language(index: PckIndex, entries: list[PckEntry]) -> str | None:
    codes = {entry.language for entry in entries if entry.language in LANGUAGE_NAMES}
    if len(codes) != 1:
        return None
    return LANGUAGE_NAMES[next(iter(codes))]


def pck_partition(index: PckIndex) -> str:
    return index.source.replace("\\", "/").rsplit("/", 1)[0].casefold()


def select_language_bank_packages(
    pcks: list[PckIndex],
) -> dict[str, PckIndex]:
    candidates: dict[str, list[PckIndex]] = {}
    for index in pcks:
        language = localized_language(index, index.banks)
        if language and index.banks:
            candidates.setdefault(language, []).append(index)
    selected: dict[str, PckIndex] = {}
    for language, packages in candidates.items():
        selected[language] = max(
            packages,
            key=lambda item: (
                len(item.banks),
                "/persistent/" in item.source.casefold(),
                item.size,
                item.source.casefold(),
            ),
        )
    return selected


def load_banks(
    bnk_dir: Path | None,
    pcks: list[PckIndex],
) -> tuple[
    dict[str, dict[int, bytes]],
    dict[str, dict[int, str]],
    dict[str, PckIndex],
]:
    selected = select_language_bank_packages(pcks)
    banks_by_language: dict[str, dict[int, bytes]] = {}
    origins_by_language: dict[str, dict[int, str]] = {}
    for language, pck in sorted(selected.items()):
        banks: dict[int, bytes] = {}
        origins: dict[int, str] = {}
        for entry in pck.banks:
            event_id = entry.file_id & 0xFFFFFFFF
            banks[event_id] = read_pck_payload(pck, entry)
            origins[event_id] = pck.source
        banks_by_language[language] = banks
        origins_by_language[language] = origins

    # The research cache contains the current Japanese event banks and enables
    # an offline run when the game PCKs are not supplied.
    if bnk_dir and bnk_dir.exists():
        language = "Japanese"
        banks = banks_by_language.setdefault(language, {})
        origins = origins_by_language.setdefault(language, {})
        for path in sorted(bnk_dir.glob("*.bnk"), key=lambda item: item.name):
            if not path.stem.isdigit():
                continue
            event_id = int(path.stem)
            payload = path.read_bytes()
            previous = banks.get(event_id)
            if previous is not None and previous != payload:
                raise ValueError(
                    f"conflicting Japanese BNK payload for Event {event_id}"
                )
            if previous is None:
                banks[event_id] = payload
                origins[event_id] = source_ref(path, None)
    return banks_by_language, origins_by_language, selected


def build_action_manifest(source_path: Path) -> dict[str, Any]:
    source = load_json(source_path)
    characters: list[dict[str, Any]] = []
    action_count = 0
    for item in source.get("characters", []):
        actions = item.get("actions", [])
        ids = [action.get("id") for action in actions]
        if len(ids) != len(set(ids)):
            raise ValueError(f"duplicate action IDs for {item.get('id')}")
        action_count += len(actions)
        characters.append(
            {
                "characterId": item["id"],
                "displayName": item.get("displayName"),
                "model": item.get("model"),
                "introSequence": {
                    "sitLoop": item.get("sitLoop"),
                    "sitSpecial": item.get("sitSpecial"),
                    "sitToWalk": item.get("sitToWalk"),
                    "finalActionId": item.get("defaultActionId"),
                },
                "actions": actions,
            }
        )
    if not characters or not action_count:
        raise ValueError("action source contains no usable records")
    return {
        "schemaVersion": 1,
        "kind": "endfield-action-manifest",
        "generatorVersion": GENERATOR_VERSION,
        "manifestVersion": source.get("manifestVersion"),
        "manifestHash": source.get("manifestHash"),
        "source": {
            "path": source_ref(source_path, None),
            "sha256": sha256_file(source_path),
        },
        "stats": {
            "characterCount": len(characters),
            "actionCount": action_count,
            "skippedCharacterCount": len(source.get("skippedCharacters", [])),
        },
        "characters": characters,
        "skippedCharacters": source.get("skippedCharacters", []),
    }


def load_version_inputs(snapshot_path: Path | None) -> dict[str, Any] | None:
    if snapshot_path is None or not snapshot_path.exists():
        return None
    snapshot = load_json(snapshot_path)
    wanted = {
        "Bundles/Windows/manifest.hgmmap",
        "TableCfg/AudioDialog.bytes",
    }
    files = [
        item for item in snapshot.get("files", []) if item.get("path") in wanted
    ]
    found = {item["path"] for item in files}
    if found != wanted:
        raise ValueError(
            "input snapshot does not contain the current manifest and AudioDialog"
        )
    files.sort(key=lambda item: item["path"].casefold())
    return {
        "snapshot": {
            "path": source_ref(snapshot_path, None),
            "sha256": sha256_file(snapshot_path),
        },
        "overlayOrder": snapshot.get("overlayOrder"),
        "files": files,
    }


def pck_descriptor(index: PckIndex, package_id: str) -> dict[str, Any]:
    language_names = sorted(set(index.languages.values()), key=str.casefold)
    media_languages = sorted(
        {entry.language for entry in index.media if entry.language}, key=str.casefold
    )
    return {
        "packageId": package_id,
        "source": index.source,
        "size": index.size,
        "headerSize": index.header_size,
        "headerSha256": index.header_sha256,
        "languages": media_languages or language_names,
        "bankCount": len(index.banks),
        "mediaCount": len(index.media),
    }


def build_voice_manifest(
    audio_dialog_path: Path,
    voice_aliases_path: Path | None,
    banks_by_language: dict[str, dict[int, bytes]],
    bank_origins_by_language: dict[str, dict[int, str]],
    selected_bank_packages: dict[str, PckIndex],
    pcks: list[PckIndex],
) -> dict[str, Any]:
    table = load_json(audio_dialog_path)
    voice_rows: dict[str, tuple[str | None, dict[str, Any], dict[str, Any]]] = {}
    for row_key, item in table.items():
        voice_id = item.get("path") if isinstance(item, dict) else None
        match = VOICE_ID.fullmatch(voice_id or "")
        if not match:
            continue
        folded = voice_id.casefold()
        if folded in voice_rows:
            raise ValueError(f"duplicate AudioDialog voice ID {voice_id}")
        voice_rows[folded] = (
            str(row_key),
            item,
            {"kind": "AudioDialog", "runtimeVerified": False},
        )

    alias_source: dict[str, Any] | None = None
    if voice_aliases_path and voice_aliases_path.exists():
        aliases = load_json(voice_aliases_path)
        if aliases.get("schemaVersion") != 1:
            raise ValueError("unsupported voice alias schema")
        for alias in aliases.get("aliases", []):
            voice_id = alias.get("voiceId")
            match = VOICE_ID.fullmatch(voice_id or "")
            if not match:
                raise ValueError(f"invalid voice alias ID {voice_id!r}")
            event_id = wwise_id(voice_id)
            expected = alias.get("expectedEventId")
            if expected is not None and event_id != int(expected):
                raise ValueError(
                    f"voice alias hash mismatch for {voice_id}: "
                    f"computed {event_id}, expected {expected}"
                )
            folded = voice_id.casefold()
            character_id = match.group(1).casefold()
            declared_character = alias.get("characterId", character_id).casefold()
            if declared_character != character_id:
                raise ValueError(f"voice alias character mismatch for {voice_id}")
            if folded in voice_rows:
                row_key, item, name_source = voice_rows[folded]
                voice_rows[folded] = (
                    row_key,
                    item,
                    {
                        **name_source,
                        "runtimeVerified": bool(alias.get("runtimeVerified")),
                        "evidence": alias.get("evidence"),
                    },
                )
            else:
                voice_rows[folded] = (
                    None,
                    {
                        "path": voice_id,
                        "speakerChannel": alias.get(
                            "speakerChannel", character_id
                        ),
                        "codec": alias.get("codec"),
                        "voType": alias.get("voType"),
                        "wavDuration": None,
                        "wavDurationEN": None,
                        "wavDurationJP": None,
                        "wavDurationKR": None,
                    },
                    {
                        "kind": "runtime-alias",
                        "runtimeVerified": bool(alias.get("runtimeVerified")),
                        "evidence": alias.get("evidence"),
                    },
                )
        alias_source = {
            "path": source_ref(voice_aliases_path, None),
            "sha256": sha256_file(voice_aliases_path),
        }

    event_to_name: dict[int, str] = {}
    entries: list[dict[str, Any]] = []
    all_referenced_media: set[int] = set()
    referenced_media_by_language: dict[str, set[int]] = {
        language: set() for language in banks_by_language
    }
    mapped_events_by_language: dict[str, set[int]] = {
        language: set() for language in banks_by_language
    }
    invalid_counts: Counter[str] = Counter()
    for folded in sorted(voice_rows):
        row_key, item, name_source = voice_rows[folded]
        voice_id = item["path"]
        character_id = VOICE_ID.fullmatch(voice_id).group(1).casefold()  # type: ignore[union-attr]
        event_id = wwise_id(voice_id)
        if row_key is not None:
            try:
                row_event_id = int(row_key) & 0xFFFFFFFF
            except ValueError as exception:
                raise ValueError(
                    f"invalid AudioDialog key {row_key!r} for {voice_id}"
                ) from exception
            if row_event_id != event_id:
                raise ValueError(
                    f"AudioDialog/Event hash mismatch for {voice_id}: "
                    f"row {row_event_id}, computed {event_id}"
                )
        collision = event_to_name.get(event_id)
        if collision and collision != folded:
            raise ValueError(
                f"Wwise Event hash collision: {collision} and {voice_id}"
            )
        event_to_name[event_id] = folded
        language_mappings: list[dict[str, Any]] = []
        for language in sorted(banks_by_language):
            banks = banks_by_language[language]
            origins = bank_origins_by_language[language]
            bank = banks.get(event_id)
            mapping: dict[str, Any] | None = None
            error: str | None = None
            if bank is not None:
                try:
                    mapping = parse_hirc_bank(bank, event_id)
                    mapped_events_by_language[language].add(event_id)
                    all_referenced_media.update(mapping["mediaIds"])
                    referenced_media_by_language[language].update(
                        mapping["mediaIds"]
                    )
                except ValueError as exception:
                    error = str(exception)
                    invalid_counts[language] += 1
            language_mapping: dict[str, Any] = {
                "language": language,
                "bankStatus": (
                    "mapped" if mapping else "invalid" if bank is not None else "missing"
                ),
            }
            if mapping:
                language_mapping.update(mapping)
                language_mapping["bankSource"] = origins.get(event_id)
            elif error:
                language_mapping["bankError"] = error
            language_mappings.append(language_mapping)
        entry: dict[str, Any] = {
            "characterId": character_id,
            "voiceId": voice_id,
            "audioDialogKey": row_key,
            "nameSource": name_source,
            "eventId": event_id,
            "speakerChannel": item.get("speakerChannel"),
            "codec": item.get("codec"),
            "voType": item.get("voType"),
            "durations": {
                "Chinese": item.get("wavDuration"),
                "English": item.get("wavDurationEN"),
                "Japanese": item.get("wavDurationJP"),
                "Korean": item.get("wavDurationKR"),
            },
            "languageMappings": language_mappings,
            "nativeMediaRoute": build_native_media_routes(language_mappings),
        }
        entries.append(entry)

    referenced_packages: list[
        tuple[str, str, PckIndex, dict[int, PckEntry]]
    ] = []
    for language in sorted(selected_bank_packages):
        partition = pck_partition(selected_bank_packages[language])
        targets = referenced_media_by_language[language]
        candidates = [
            index
            for index in pcks
            if pck_partition(index) == partition and index.media
        ]
        package_number = 0
        for index in sorted(candidates, key=lambda item: item.source.casefold()):
            media_by_id = {
                entry.file_id & 0xFFFFFFFF: entry
                for entry in index.media
                if entry.file_id <= 0xFFFFFFFF
            }
            if targets.intersection(media_by_id):
                package_number += 1
                package_id = f"{language.casefold()}-pck-{package_number}"
                referenced_packages.append(
                    (package_id, language, index, media_by_id)
                )

    for entry in entries:
        for mapping in entry["languageMappings"]:
            media_ids = mapping.get("mediaIds")
            if not media_ids:
                continue
            media_records = []
            for media_id in media_ids:
                available = []
                for package_id, language, _index, media_by_id in referenced_packages:
                    if language != mapping["language"]:
                        continue
                    pck_entry = media_by_id.get(media_id)
                    if pck_entry:
                        available.append(
                            {"packageId": package_id, "size": pck_entry.size}
                        )
                media_records.append(
                    {"mediaId": media_id, "availableIn": available}
                )
            mapping["media"] = media_records

    known_ids = set(event_to_name)
    package_descriptors = [
        {
            **pck_descriptor(index, package_id),
            "mappingLanguage": language,
        }
        for package_id, language, index, _media in referenced_packages
    ]
    bank_package_descriptors = [
        {
            **pck_descriptor(index, f"{language.casefold()}-banks"),
            "mappingLanguage": language,
        }
        for language, index in sorted(selected_bank_packages.items())
    ]
    package_coverage = []
    for package_id, language, _index, media_by_id in referenced_packages:
        targets = referenced_media_by_language[language]
        present = targets.intersection(media_by_id)
        package_coverage.append(
            {
                "packageId": package_id,
                "language": language,
                "referencedMediaPresent": len(present),
                "referencedMediaMissing": len(targets - present),
            }
        )

    language_stats: dict[str, Any] = {}
    for language in sorted(banks_by_language):
        banks = banks_by_language[language]
        mapped = mapped_events_by_language[language]
        media = referenced_media_by_language[language]
        available_media = {
            media_id
            for _package_id, package_language, _index, media_by_id in referenced_packages
            if package_language == language
            for media_id in media_by_id
        }
        language_stats[language] = {
            "bankCount": len(banks),
            "mappedVoiceCount": len(mapped),
            "missingBankCount": len(known_ids - set(banks)),
            "invalidBankCount": invalid_counts[language],
            "unresolvedBankCount": len(set(banks) - known_ids),
            "mediaReferenceCount": sum(
                len(mapping.get("mediaIds", []))
                for item in entries
                for mapping in item["languageMappings"]
                if mapping["language"] == language
            ),
            "uniqueMediaCount": len(media),
            "mediaPresentCount": len(media & available_media),
            "mediaMissingCount": len(media - available_media),
            "bankPackage": (
                selected_bank_packages[language].source
                if language in selected_bank_packages
                else None
            ),
        }

    mapped_any = {
        event_id
        for mapped in mapped_events_by_language.values()
        for event_id in mapped
    }
    fully_mapped = set(known_ids)
    for mapped in mapped_events_by_language.values():
        fully_mapped.intersection_update(mapped)
    all_banks = {
        event_id
        for banks in banks_by_language.values()
        for event_id in banks
    }
    return {
        "schemaVersion": 1,
        "kind": "endfield-voice-event-media-manifest",
        "generatorVersion": GENERATOR_VERSION,
        "wwiseHash": "lowercase UTF-8 FNV-1 32-bit",
        "hircSoundMediaIdOffset": 9,
        "source": {
            "path": source_ref(audio_dialog_path, None),
            "sha256": sha256_file(audio_dialog_path),
            "aliases": alias_source,
        },
        "stats": {
            "namedVoiceCount": len(entries),
            "audioDialogVoiceCount": sum(
                item["nameSource"]["kind"] == "AudioDialog" for item in entries
            ),
            "aliasVoiceCount": sum(
                item["nameSource"]["kind"] == "runtime-alias" for item in entries
            ),
            "runtimeVerifiedVoiceCount": sum(
                bool(item["nameSource"].get("runtimeVerified")) for item in entries
            ),
            "characterCount": len({item["characterId"] for item in entries}),
            "mappedVoiceCount": len(mapped_any),
            "fullyMappedVoiceCount": len(fully_mapped),
            "languageCount": len(banks_by_language),
            "bankCount": sum(len(banks) for banks in banks_by_language.values()),
            "unresolvedBankCount": len(all_banks - known_ids),
            "mediaReferenceCount": sum(
                len(mapping.get("mediaIds", []))
                for item in entries
                for mapping in item["languageMappings"]
            ),
            "uniqueMediaCount": len(all_referenced_media),
        },
        "languageStats": language_stats,
        "bankPackages": bank_package_descriptors,
        "pckPackages": package_descriptors,
        "pckCoverage": package_coverage,
        "voices": entries,
        "unresolvedEventIds": sorted(all_banks - known_ids),
    }


def build_summary(actions: dict[str, Any], voices: dict[str, Any]) -> str:
    action_stats = actions["stats"]
    voice_stats = voices["stats"]
    native_event_count = sum(
        1
        for voice in voices["voices"]
        if voice.get("nativeMediaRoute", {}).get("status") == "mapped"
    )
    native_fallback_count = len(voices["voices"]) - native_event_count
    lines = [
        "# 终末地资源映射清单",
        "",
        "## 动作",
        "",
        f"- 游戏 VFS manifest 版本：`{actions.get('manifestVersion')}`",
        f"- 角色：{action_stats['characterCount']}",
        f"- 动作：{action_stats['actionCount']}",
        f"- 未纳入角色：{action_stats['skippedCharacterCount']}",
        "",
        "## 版本输入",
        "",
    ]
    version_inputs = actions.get("versionInputs")
    if version_inputs:
        for item in version_inputs["files"]:
            lines.append(
                f"- `{item['path']}`：`{item['sha256']}`（{item['size']} bytes）"
            )
    else:
        lines.append("- 未提供 VFS Overlay 输入快照。")
    lines.extend([
        "",
        "## 角色短语音",
        "",
        f"- `AudioDialog` 具名角色短语音 Event：{voice_stats['namedVoiceCount']}",
        f"- 覆盖角色：{voice_stats['characterCount']}",
        f"- 至少一种语言通过 BNK/HIRC 映射：{voice_stats['mappedVoiceCount']}",
        f"- 四语均通过映射：{voice_stats['fullyMappedVoiceCount']}",
        f"- 原生 Wwise 容器事件：{native_event_count}",
        f"- v9 显式降级事件：{native_fallback_count}",
        f"- 无可读名称的 Bank：{voice_stats['unresolvedBankCount']}",
        f"- Media 引用：{voice_stats['mediaReferenceCount']}（唯一 {voice_stats['uniqueMediaCount']}）",
        "",
        "### 各语言",
        "",
    ])
    for language, stats in voices["languageStats"].items():
        lines.append(
            f"- {language}：映射 {stats['mappedVoiceCount']}，缺 Bank "
            f"{stats['missingBankCount']}，无效 {stats['invalidBankCount']}，"
            f"Media 存在 {stats['mediaPresentCount']} / {stats['uniqueMediaCount']}"
        )
    lines.extend([
        "",
        "## PCK 覆盖",
        "",
    ])
    if voices["pckCoverage"]:
        by_id = {item["packageId"]: item for item in voices["pckPackages"]}
        for coverage in voices["pckCoverage"]:
            package = by_id[coverage["packageId"]]
            languages = ", ".join(package["languages"]) or "未知语言"
            lines.append(
                f"- `{coverage['packageId']}` {languages}：存在 "
                f"{coverage['referencedMediaPresent']}，缺少 "
                f"{coverage['referencedMediaMissing']}（`{package['source']}`）"
            )
    else:
        lines.append("- 未提供或未发现包含目标 Media ID 的 PCK。")
    lines.extend(
        [
            "",
            "## 解释",
            "",
            "- `mapped` 表示 AudioDialog Event ID、BNK/HIRC Event 对象和可达 Sound Media ID 均通过结构校验。",
            "- `missing` 表示表中有可读语音 ID，但当前 Bank 输入不包含同 ID BNK。",
            "- `unresolvedEventIds` 只保存无可读名称的 Event ID，不猜测角色归属。",
            "- 清单不包含 WEM/PCK 音频内容；PCK 只读取索引与目标 Media 大小。",
            "- 生成过程不读取 `GameAssembly.dll`，官服/B 服只要表、BNK 与 PCK 内容一致即可共用清单。",
            "",
        ]
    )
    return "\n".join(lines)


def main() -> int:
    args = parse_args()
    actions_path = args.actions.resolve()
    audio_dialog_path = args.audio_dialog.resolve()
    voice_aliases_path = (
        args.voice_aliases.resolve() if args.voice_aliases else None
    )
    input_snapshot_path = (
        args.input_snapshot.resolve() if args.input_snapshot else None
    )
    bnk_dir = args.bnk_dir.resolve() if args.bnk_dir else None
    game_path = args.game_path.resolve() if args.game_path else None
    output_dir = args.output_dir.resolve()

    explicit_pcks = [path.resolve() for path in args.pck]
    discovered_pcks: list[Path] = []
    if game_path and not args.no_pck_discovery:
        discovered_pcks = discover_pck_paths(game_path)
    pck_paths = sorted(
        set(explicit_pcks + discovered_pcks), key=lambda item: str(item).casefold()
    )
    pcks: list[PckIndex] = []
    pck_errors: list[str] = []
    for path in pck_paths:
        try:
            pcks.append(parse_pck(path, game_path))
        except (OSError, ValueError) as exception:
            pck_errors.append(f"{source_ref(path, game_path)}: {exception}")
    if pck_errors:
        raise ValueError("PCK indexing failed:\n" + "\n".join(pck_errors))

    banks_by_language, origins_by_language, selected_banks = load_banks(
        bnk_dir, pcks
    )
    action_manifest = build_action_manifest(actions_path)
    voice_manifest = build_voice_manifest(
        audio_dialog_path,
        voice_aliases_path,
        banks_by_language,
        origins_by_language,
        selected_banks,
        pcks,
    )
    version_inputs = load_version_inputs(input_snapshot_path)
    if version_inputs:
        action_manifest["versionInputs"] = version_inputs
        voice_manifest["versionInputs"] = version_inputs
    write_json(output_dir / "model" / "action-manifest.json", action_manifest)
    write_json(
        output_dir / "voice" / "voice-event-media-manifest.json",
        voice_manifest,
    )
    summary = build_summary(action_manifest, voice_manifest)
    report_path = output_dir / "shared" / "resource-manifest-report.md"
    report_path.parent.mkdir(parents=True, exist_ok=True)
    report_path.write_text(
        summary, encoding="utf-8", newline="\n"
    )
    print(
        "Generated "
        f"{action_manifest['stats']['characterCount']} characters / "
        f"{action_manifest['stats']['actionCount']} actions and "
        f"{voice_manifest['stats']['mappedVoiceCount']} / "
        f"{voice_manifest['stats']['namedVoiceCount']} mapped voices in "
        f"{output_dir}"
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
