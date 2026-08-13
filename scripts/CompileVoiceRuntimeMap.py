#!/usr/bin/env python3
"""Compile the audited voice manifest into the injector's compact runtime map."""

from __future__ import annotations

import argparse
import hashlib
import json
import struct
from pathlib import Path
from typing import Any


REPO_ROOT = Path(__file__).resolve().parents[1]
DEFAULT_INPUT = REPO_ROOT / "manifests/voice-event-media-manifest.json"
DEFAULT_BINARY = REPO_ROOT / "manifests/voice-runtime-map.bin"
DEFAULT_HEADER = (
    REPO_ROOT
    / "tools/IL2CPP-Dumper-src/Dump/include/generated/voice_runtime_map.generated.hxx"
)
DEFAULT_REPORT = REPO_ROOT / "manifests/voice-runtime-map-report.md"

MAGIC = b"EFVRMAP\0"
FORMAT_VERSION = 2
LANGUAGES = ("Chinese", "English", "Japanese", "Korean")

# Header fields: magic/version/headerSize, 16 uint32 values, AudioDialog SHA-256.
HEADER = struct.Struct("<8sHH16I32s")
PACKAGE = struct.Struct("<I H B B Q I 32s")
CHARACTER = struct.Struct("<I H H")
VOICE = struct.Struct("<I I H B B I")
ROUTE = struct.Struct("<I H H")
NATIVE_SLOT = struct.Struct("<4I B 3x")


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser()
    parser.add_argument("--input", type=Path, default=DEFAULT_INPUT)
    parser.add_argument("--binary", type=Path, default=DEFAULT_BINARY)
    parser.add_argument("--header", type=Path, default=DEFAULT_HEADER)
    parser.add_argument("--report", type=Path, default=DEFAULT_REPORT)
    return parser.parse_args()


def load_manifest(path: Path) -> dict[str, Any]:
    with path.open("r", encoding="utf-8") as stream:
        manifest = json.load(stream)
    if manifest.get("kind") != "endfield-voice-event-media-manifest":
        raise ValueError("input is not an Endfield voice Event-to-Media manifest")
    if manifest.get("wwiseHash") != "lowercase UTF-8 FNV-1 32-bit":
        raise ValueError("manifest uses an unsupported Wwise hash algorithm")
    return manifest


def hex_digest(value: str, label: str) -> bytes:
    try:
        result = bytes.fromhex(value)
    except ValueError as error:
        raise ValueError(f"invalid {label} SHA-256") from error
    if len(result) != 32:
        raise ValueError(f"invalid {label} SHA-256 length")
    return result


class StringTable:
    def __init__(self) -> None:
        self.data = bytearray()
        self.offsets: dict[str, tuple[int, int]] = {}

    def add(self, value: str) -> tuple[int, int]:
        encoded = value.encode("utf-8")
        if not encoded or len(encoded) > 0xFFFF:
            raise ValueError(f"invalid runtime string length for {value!r}")
        existing = self.offsets.get(value)
        if existing is not None:
            return existing
        result = (len(self.data), len(encoded))
        self.data.extend(encoded)
        self.offsets[value] = result
        return result


def compile_map(
    manifest: dict[str, Any], source_size: int
) -> tuple[bytes, dict[str, Any]]:
    strings = StringTable()
    mapped_voices: list[dict[str, Any]] = []
    skipped_voices = 0
    for voice in manifest.get("voices", []):
        mappings = {
            item.get("language"): item
            for item in voice.get("languageMappings", [])
        }
        routes: list[list[int]] = []
        for language in LANGUAGES:
            mapping = mappings.get(language)
            media_ids = (
                mapping.get("mediaIds", [])
                if mapping and mapping.get("bankStatus") == "mapped"
                else []
            )
            route = sorted({int(media_id) for media_id in media_ids})
            if any(media_id <= 0 or media_id > 0xFFFFFFFF for media_id in route):
                raise ValueError(f"invalid Media ID in {voice.get('voiceId')}")
            if len(route) > 0xFFFF:
                raise ValueError(f"too many Media IDs in {voice.get('voiceId')}")
            routes.append(route)
        if not any(routes):
            skipped_voices += 1
            continue
        mapped_voices.append({**voice, "runtimeRoutes": routes})

    character_ids = sorted(
        {str(voice["characterId"]).casefold() for voice in mapped_voices}
    )
    if not character_ids or len(character_ids) > 0xFF:
        raise ValueError("runtime character count must be between 1 and 255")
    character_index = {
        character_id: index for index, character_id in enumerate(character_ids)
    }

    packages_by_language: dict[str, dict[str, Any]] = {}
    for package in manifest.get("pckPackages", []):
        language = package.get("mappingLanguage")
        if language not in LANGUAGES:
            continue
        if language in packages_by_language:
            raise ValueError(f"multiple runtime PCK packages for {language}")
        packages_by_language[language] = package
    missing_packages = [item for item in LANGUAGES if item not in packages_by_language]
    if missing_packages:
        raise ValueError("missing runtime PCK packages: " + ", ".join(missing_packages))

    package_rows = bytearray()
    for language_index, language in enumerate(LANGUAGES):
        package = packages_by_language[language]
        source_offset, source_length = strings.add(str(package["source"]))
        package_rows.extend(
            PACKAGE.pack(
                source_offset,
                source_length,
                language_index,
                0,
                int(package["size"]),
                int(package["headerSize"]),
                hex_digest(package["headerSha256"], f"{language} PCK header"),
            )
        )

    character_rows = bytearray()
    for character_id in character_ids:
        offset, length = strings.add(character_id)
        character_rows.extend(CHARACTER.pack(offset, length, 0))

    mapped_voices.sort(key=lambda item: (int(item["eventId"]), item["voiceId"]))
    voice_rows = bytearray()
    route_rows = bytearray()
    media_rows = bytearray()
    route_count = 0
    media_count = 0
    native_slots: set[tuple[int, int, int, int, int]] = set()
    event_name_pairs: set[tuple[int, str]] = set()
    for voice in mapped_voices:
        voice_id = str(voice["voiceId"]).casefold()
        event_id = int(voice["eventId"])
        pair = (event_id, voice_id)
        if pair in event_name_pairs:
            raise ValueError(f"duplicate runtime voice mapping {voice_id}")
        event_name_pairs.add(pair)
        name_offset, name_length = strings.add(voice_id)
        first_route = route_count
        native = voice.get("nativeMediaRoute", {})
        native_eligible = native.get("status") == "mapped"
        for route in voice["runtimeRoutes"]:
            route_rows.extend(
                ROUTE.pack(media_count, len(route), 1 if native_eligible else 0)
            )
            for media_id in route:
                media_rows.extend(struct.pack("<I", media_id))
            media_count += len(route)
            route_count += 1
        voice_rows.extend(
            VOICE.pack(
                event_id,
                name_offset,
                name_length,
                character_index[str(voice["characterId"]).casefold()],
                int(voice.get("codec", 0)),
                first_route,
            )
        )
        if native_eligible:
            for slot in native.get("slots", []):
                media_by_language = slot.get("mediaIds", {})
                media = tuple(int(media_by_language[language]) for language in LANGUAGES)
                if any(value <= 0 or value > 0xFFFFFFFF for value in media):
                    raise ValueError(f"invalid native Media route in {voice_id}")
                character = character_index[str(voice["characterId"]).casefold()]
                native_slots.add((*media, character))

    # A source Media ID must identify one localized leaf.  The four target IDs
    # may differ by language, but another slot may not reuse the source ID with
    # a different target tuple.
    native_identity: dict[tuple[int, int], tuple[int, int, int, int]] = {}
    for *media, _character in sorted(native_slots):
        route = tuple(media)
        for source_language, source_media in enumerate(route):
            key = (source_language, source_media)
            previous = native_identity.setdefault(key, route)
            if previous != route:
                raise ValueError(
                    f"ambiguous native Media route for language {source_language} "
                    f"source {source_media}"
                )

    if route_count != len(mapped_voices) * len(LANGUAGES):
        raise AssertionError("runtime route table is not dense")

    package_offset = HEADER.size
    character_offset = package_offset + len(package_rows)
    voice_offset = character_offset + len(character_rows)
    route_offset = voice_offset + len(voice_rows)
    media_offset = route_offset + len(route_rows)
    native_slot_offset = media_offset + len(media_rows)
    native_slot_rows = bytearray()
    for chinese, english, japanese, korean, character in sorted(native_slots):
        native_slot_rows.extend(
            NATIVE_SLOT.pack(chinese, english, japanese, korean, character)
        )
    string_offset = native_slot_offset + len(native_slot_rows)
    total_size = string_offset + len(strings.data)
    source_digest = hex_digest(manifest["source"]["sha256"], "AudioDialog")
    header = HEADER.pack(
        MAGIC,
        FORMAT_VERSION,
        HEADER.size,
        total_size,
        len(LANGUAGES),
        len(LANGUAGES),
        len(character_ids),
        len(mapped_voices),
        route_count,
        media_count,
        len(native_slots),
        len(strings.data),
        package_offset,
        character_offset,
        voice_offset,
        route_offset,
        media_offset,
        native_slot_offset,
        string_offset,
        source_digest,
    )
    blob = b"".join(
        (
            header,
            package_rows,
            character_rows,
            voice_rows,
            route_rows,
            media_rows,
            native_slot_rows,
            strings.data,
        )
    )
    if len(blob) != total_size:
        raise AssertionError("runtime map size mismatch")

    stats = {
        "formatVersion": FORMAT_VERSION,
        "sourceVoiceCount": len(manifest.get("voices", [])),
        "mappedVoiceCount": len(mapped_voices),
        "nativeVoiceCount": sum(
            1
            for voice in mapped_voices
            if voice.get("nativeMediaRoute", {}).get("status") == "mapped"
        ),
        "skippedVoiceCount": skipped_voices,
        "characterCount": len(character_ids),
        "routeCount": route_count,
        "mediaReferenceCount": media_count,
        "uniqueMediaCount": len(
            {
                media_id
                for voice in mapped_voices
                for route in voice["runtimeRoutes"]
                for media_id in route
            }
        ),
        "nativeSlotCount": len(native_slots),
        "stringBytes": len(strings.data),
        "binaryBytes": len(blob),
        "sourceBytes": source_size,
        "sha256": hashlib.sha256(blob).hexdigest().upper(),
    }
    return blob, stats


def validate_blob(blob: bytes) -> None:
    if len(blob) < HEADER.size:
        raise ValueError("compiled runtime map is truncated")
    values = HEADER.unpack_from(blob)
    (
        magic,
        version,
        header_size,
        total_size,
        language_count,
        package_count,
        character_count,
        voice_count,
        route_count,
        media_count,
        native_slot_count,
        string_size,
        package_offset,
        character_offset,
        voice_offset,
        route_offset,
        media_offset,
        native_slot_offset,
        string_offset,
        _source_digest,
    ) = values
    if magic != MAGIC or version != FORMAT_VERSION or header_size != HEADER.size:
        raise ValueError("compiled runtime map header is invalid")
    if total_size != len(blob) or language_count != len(LANGUAGES):
        raise ValueError("compiled runtime map dimensions are invalid")
    expected_offsets = (
        HEADER.size,
        package_offset + package_count * PACKAGE.size,
        character_offset + character_count * CHARACTER.size,
        voice_offset + voice_count * VOICE.size,
        route_offset + route_count * ROUTE.size,
        media_offset + media_count * 4,
        native_slot_offset + native_slot_count * NATIVE_SLOT.size,
        string_offset + string_size,
    )
    actual_offsets = (
        package_offset,
        character_offset,
        voice_offset,
        route_offset,
        media_offset,
        native_slot_offset,
        string_offset,
        len(blob),
    )
    if expected_offsets != actual_offsets:
        raise ValueError("compiled runtime map section bounds are invalid")
    previous_event = -1
    for index in range(voice_count):
        row = VOICE.unpack_from(blob, voice_offset + index * VOICE.size)
        event_id, name_offset, name_length, char_index, _codec, first_route = row
        if event_id < previous_event or char_index >= character_count:
            raise ValueError("compiled runtime voice index is invalid")
        if name_offset + name_length > string_size:
            raise ValueError("compiled runtime voice string is invalid")
        if first_route + language_count > route_count:
            raise ValueError("compiled runtime route index is invalid")
        previous_event = event_id
    for index in range(route_count):
        media_index, count, _reserved = ROUTE.unpack_from(
            blob, route_offset + index * ROUTE.size
        )
        if media_index + count > media_count:
            raise ValueError("compiled runtime Media index is invalid")
    for index in range(native_slot_count):
        chinese, english, japanese, korean, character = NATIVE_SLOT.unpack_from(
            blob, native_slot_offset + index * NATIVE_SLOT.size
        )
        if not all((chinese, english, japanese, korean)) or character >= character_count:
            raise ValueError("compiled native Media slot is invalid")


def validate_against_manifest(blob: bytes, manifest: dict[str, Any]) -> None:
    values = HEADER.unpack_from(blob)
    (
        _magic,
        _version,
        _header_size,
        _total_size,
        language_count,
        package_count,
        character_count,
        voice_count,
        _route_count,
        _media_count,
        native_slot_count,
        string_size,
        package_offset,
        character_offset,
        voice_offset,
        route_offset,
        media_offset,
        native_slot_offset,
        string_offset,
        source_digest,
    ) = values
    strings = memoryview(blob)[string_offset : string_offset + string_size]

    def text_at(offset: int, length: int) -> str:
        return bytes(strings[offset : offset + length]).decode("utf-8")

    packages: dict[str, dict[str, Any]] = {}
    for index in range(package_count):
        row = PACKAGE.unpack_from(blob, package_offset + index * PACKAGE.size)
        source_at, source_length, language, _reserved, size, header_size, digest = row
        packages[LANGUAGES[language]] = {
            "source": text_at(source_at, source_length),
            "size": size,
            "headerSize": header_size,
            "headerSha256": digest.hex().upper(),
        }
    expected_packages = {
        item["mappingLanguage"]: item
        for item in manifest["pckPackages"]
        if item.get("mappingLanguage") in LANGUAGES
    }
    for language in LANGUAGES:
        actual = packages.get(language)
        expected = expected_packages.get(language)
        if actual is None or expected is None or any(
            actual[key] != expected[key]
            for key in ("source", "size", "headerSize", "headerSha256")
        ):
            raise ValueError(f"compiled {language} PCK descriptor mismatch")

    characters: list[str] = []
    for index in range(character_count):
        offset, length, _reserved = CHARACTER.unpack_from(
            blob, character_offset + index * CHARACTER.size
        )
        characters.append(text_at(offset, length))

    compiled: dict[tuple[int, str], dict[str, Any]] = {}
    for index in range(voice_count):
        event_id, name_at, name_length, character, codec, first_route = (
            VOICE.unpack_from(blob, voice_offset + index * VOICE.size)
        )
        routes: list[list[int]] = []
        for language in range(language_count):
            first_media, count, _reserved = ROUTE.unpack_from(
                blob, route_offset + (first_route + language) * ROUTE.size
            )
            routes.append(
                [
                    struct.unpack_from("<I", blob, media_offset + item * 4)[0]
                    for item in range(first_media, first_media + count)
                ]
            )
        voice_id = text_at(name_at, name_length)
        compiled[(event_id, voice_id)] = {
            "characterId": characters[character],
            "codec": codec,
            "routes": routes,
        }

    expected_count = 0
    for voice in manifest["voices"]:
        mappings = {
            item.get("language"): item for item in voice["languageMappings"]
        }
        routes = [
            sorted(
                {
                    int(media_id)
                    for media_id in mappings.get(language, {}).get("mediaIds", [])
                }
            )
            if mappings.get(language, {}).get("bankStatus") == "mapped"
            else []
            for language in LANGUAGES
        ]
        if not any(routes):
            continue
        expected_count += 1
        key = (int(voice["eventId"]), str(voice["voiceId"]).casefold())
        actual = compiled.get(key)
        if actual is None:
            raise ValueError(f"compiled voice missing: {voice['voiceId']}")
        if (
            actual["characterId"] != str(voice["characterId"]).casefold()
            or actual["codec"] != int(voice.get("codec", 0))
            or actual["routes"] != routes
        ):
            raise ValueError(f"compiled voice mismatch: {voice['voiceId']}")
    if len(compiled) != expected_count:
        raise ValueError("compiled voice table contains unexpected records")
    expected_native_slots = {
        (
            *(int(slot["mediaIds"][language]) for language in LANGUAGES),
            characters.index(str(voice["characterId"]).casefold()),
        )
        for voice in manifest["voices"]
        if voice.get("nativeMediaRoute", {}).get("status") == "mapped"
        for slot in voice["nativeMediaRoute"].get("slots", [])
    }
    compiled_native_slots = {
        NATIVE_SLOT.unpack_from(blob, native_slot_offset + index * NATIVE_SLOT.size)
        for index in range(native_slot_count)
    }
    if compiled_native_slots != expected_native_slots:
        raise ValueError("compiled native Media slots mismatch")
    if source_digest != hex_digest(manifest["source"]["sha256"], "AudioDialog"):
        raise ValueError("compiled AudioDialog identity mismatch")


def write_header(path: Path, blob: bytes, digest: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    lines = [
        "#pragma once",
        "",
        "#include <cstddef>",
        "#include <cstdint>",
        "",
        "namespace GeneratedVoiceRuntimeMap {",
        "",
        "    // Generated by scripts/CompileVoiceRuntimeMap.py. Do not edit.",
        "    alignas( 4 ) inline constexpr std::uint8_t kData [ ] = {",
    ]
    for start in range(0, len(blob), 16):
        chunk = blob[start : start + 16]
        lines.append("        " + ", ".join(f"0x{byte:02X}" for byte in chunk) + ",")
    lines.extend(
        [
            "    };",
            "    inline constexpr std::size_t kSize = sizeof( kData );",
            f'    inline constexpr char kSha256 [ ] = "{digest}";',
            "",
            "} // namespace GeneratedVoiceRuntimeMap",
            "",
        ]
    )
    path.write_text("\n".join(lines), encoding="ascii", newline="\n")


def write_report(path: Path, stats: dict[str, Any]) -> None:
    source_bytes = stats["sourceBytes"]
    reduction = 0.0 if not source_bytes else 100.0 * (
        1.0 - stats["binaryBytes"] / source_bytes
    )
    text = f"""# 注入器语音运行时映射表

- 格式版本：`{stats['formatVersion']}`
- 角色：{stats['characterCount']}
- 可路由 Event：{stats['mappedVoiceCount']} / {stats['sourceVoiceCount']}
- 原生 Wwise 容器 Event：{stats['nativeVoiceCount']}
- v9 显式降级 Event：{stats['skippedVoiceCount']}
- 语言路由：{stats['routeCount']}
- Media 引用：{stats['mediaReferenceCount']}（唯一 {stats['uniqueMediaCount']}）
- 原生 Wwise 叶子槽位：{stats['nativeSlotCount']}
- 字符串区：{stats['stringBytes']} bytes
- 二进制表：{stats['binaryBytes']} bytes
- 相对 JSON 清单缩小：{reduction:.2f}%
- SHA-256：`{stats['sha256']}`

二进制表和生成的 C++ 头文件内容完全一致。发布时映射表编译进
`il2cpp.dll`，不增加外部运行时文件；缺失映射、PCK 身份不匹配或预热失败时保留
v9 `_PlayExternal` 降级行为。原生路线由 Wwise Random/Sequence Container 选择
Sound，再由 `SetMedia` 将该 Sound 的源语言 Media 内容替换为目标语言 WEM。
"""
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text, encoding="utf-8", newline="\n")


def main() -> int:
    args = parse_args()
    manifest = load_manifest(args.input.resolve())
    blob, stats = compile_map(manifest, args.input.stat().st_size)
    validate_blob(blob)
    validate_against_manifest(blob, manifest)
    args.binary.parent.mkdir(parents=True, exist_ok=True)
    args.binary.write_bytes(blob)
    write_header(args.header, blob, stats["sha256"])
    write_report(args.report, stats)
    print(
        f"Voice runtime map: {stats['mappedVoiceCount']} voices / "
        f"{stats['mediaReferenceCount']} Media refs / {stats['binaryBytes']} bytes"
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
