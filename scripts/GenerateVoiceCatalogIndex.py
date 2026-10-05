#!/usr/bin/env python3
"""Generate the compact routing index embedded by the Better Endfield UI."""

from __future__ import annotations

import argparse
import hashlib
import json
from pathlib import Path
from typing import Any

from workspace_config import load_workspace, write_if_changed
from RefreshEndfieldResourceInputs import read_metadata

from BuildVoiceCatalog import (
    LANGUAGES,
    collect_routes,
    load_json,
    target_packages_for,
)


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace-config", type=Path)
    parser.add_argument(
        "--manifest",
        type=Path,
    )
    parser.add_argument(
        "--schema-version",
        type=int,
        choices=(1, 2),
        help="Use 1 for the current Android reader's Media ID pairs; default 2 uses language-aware triples.",
    )
    parser.add_argument(
        "--language", choices=LANGUAGES, action="append",
        help="Restrict output to validated languages; repeat for each downloaded Android language.",
    )
    parser.add_argument("--legacy-index", type=Path,
                        help="Retain the shipped Android schema-1 catalogs and routes incrementally.")
    parser.add_argument("--merge-evidence", type=Path,
                        help="Verified Android table/resource evidence for retaining the legacy index.")
    parser.add_argument(
        "--output",
        type=Path,
    )
    args = parser.parse_args()
    ws = load_workspace(args.workspace_config)
    args.manifest = args.manifest or ws.path("resource_update.outputs.manifests", "voice", "voice-event-media-manifest.json")
    args.output = args.output or ws.path("resource_update.outputs.voice_index")
    args.schema_version = args.schema_version or ws.get("resource_update.voice_schema_version", 2)
    args.language = args.language or ws.get("resource_update.voice_languages")
    args.legacy_index = args.legacy_index or ws.path("resource_update.voice_legacy_index", required=False)
    args.merge_evidence = args.merge_evidence or ws.path("resource_update.voice_merge_evidence", required=False)
    return args


def collect_voice_sources(
    manifest: dict[str, Any], language: str, character_id: str
) -> dict[str, list[int]]:
    sources: dict[str, list[int]] = {}
    for voice in manifest.get("voices", []):
        if character_id and voice.get("characterId") != character_id:
            continue
        identities = [
            str(value)
            for value in (voice.get("voiceId"), voice.get("audioDialogKey"))
            if value is not None and str(value)
        ]
        if not identities:
            continue
        media: set[int] = set()
        for mapping in voice.get("languageMappings", []):
            if mapping.get("language") != language:
                continue
            media.update(
                int(slot["mediaId"]) & 0xFFFFFFFF
                for slot in mapping.get("soundSlots", [])
                if slot.get("mediaId")
            )
            media.update(
                int(media_id) & 0xFFFFFFFF
                for media_id in mapping.get("mediaIds", [])
                if media_id
            )
        if media:
            ordered = sorted(media)
            for identity in identities:
                sources[identity] = ordered
    return sources


def build_index(
    manifest_path: Path, schema_version: int = 2,
    languages: list[str] | None = None,
) -> dict[str, Any]:
    if schema_version not in (1, 2):
        raise ValueError("voice catalog schema version must be 1 or 2")
    selected_languages = set(LANGUAGES if languages is None else languages)
    if not selected_languages or not selected_languages.issubset(LANGUAGES):
        raise ValueError("voice catalog languages are empty or unsupported")
    manifest_bytes = manifest_path.read_bytes()
    manifest = load_json(manifest_path)
    if manifest.get("kind") != "endfield-voice-event-media-manifest":
        raise ValueError("voice manifest kind is not recognized")

    packages: list[dict[str, Any]] = []
    for language_index, language in enumerate(LANGUAGES):
        if language not in selected_languages:
            continue
        for package in target_packages_for(manifest, language):
            packages.append(
                {
                    "language": language_index,
                    "source": package["source"],
                    "size": int(package["size"]),
                    "headerSize": int(package["headerSize"]),
                    "headerSha256": package["headerSha256"],
                }
            )

    characters = sorted(
        {
            str(voice.get("characterId"))
            for voice in manifest.get("voices", [])
            if voice.get("characterId")
        }
    )
    catalogs: list[dict[str, Any]] = []
    for character_id in ["*", *characters]:
        filter_id = "" if character_id == "*" else character_id
        for language_index, language in enumerate(LANGUAGES):
            if language not in selected_languages:
                continue
            routes, voice_count = collect_routes(manifest, language, filter_id)
            if not routes:
                continue
            flattened: list[int] = []
            if schema_version == 1:
                pairs: dict[int, int] = {}
                for (_source_language, source_id), target_id in sorted(routes.items()):
                    previous = pairs.setdefault(source_id, target_id)
                    if previous != target_id:
                        raise ValueError(
                            f"Android schema 1 cannot represent conflicting Media ID routes: "
                            f"{character_id}/{language}/{source_id}"
                        )
                for source_id, target_id in sorted(pairs.items()):
                    flattened.extend((source_id, target_id))
            else:
                for (source_language, source_id), target_id in sorted(routes.items()):
                    flattened.extend((source_language, source_id, target_id))
            catalog = {
                "characterId": character_id,
                "language": language_index,
                "voiceCount": voice_count,
                "routes": flattened,
            }
            if schema_version == 2:
                catalog["voiceSources"] = collect_voice_sources(
                    manifest, language, filter_id
                )
            catalogs.append(catalog)

    result = {
        "schemaVersion": schema_version,
        "kind": "betterendfield-voice-catalog-index",
        "sourceManifestSha256": hashlib.sha256(manifest_bytes).hexdigest().upper(),
        "packages": packages,
        "catalogs": catalogs,
    }
    if schema_version == 1 and manifest.get("versionInputs", {}).get("platform") == "Android" and \
            manifest.get("pckSnapshot", {}).get("platform") == "Android":
        result["provenance"] = {
            "mode": "validated-Android-device-manifest",
            "platform": "Android",
            "sourceManifestSha256": result["sourceManifestSha256"],
            "pckSnapshot": manifest["pckSnapshot"],
            "validatedLanguages": [language for language in LANGUAGES if language in selected_languages],
            "staticRouteVerification": "Android BNK HIRC sound slots and device PCK target Media IDs",
            "playbackVerified": False,
        }
        for catalog in catalogs:
            catalog["provenance"] = {
                "mode": "current-Android-BNK-and-media-index",
                "sourceManifestSha256": result["sourceManifestSha256"],
            }
    return result


def schema1_routes(catalog: dict[str, Any]) -> dict[int, int]:
    encoded = catalog["routes"]
    if not encoded or len(encoded) % 2:
        raise ValueError("invalid Android schema-1 route pairs")
    result: dict[int, int] = {}
    for source_id, target_id in zip(encoded[::2], encoded[1::2]):
        if not all(isinstance(value, int) and 0 < value <= 0xFFFFFFFF
                   for value in (source_id, target_id)):
            raise ValueError("Android schema-1 Media ID is outside uint32 range")
        previous = result.setdefault(source_id, target_id)
        if previous != target_id:
            raise ValueError(f"conflicting legacy schema-1 route for source Media ID {source_id}")
    return result


def merge_legacy_index(
    current: dict[str, Any], legacy_path: Path, evidence_path: Path,
) -> dict[str, Any]:
    legacy_bytes = legacy_path.read_bytes()
    legacy = json.loads(legacy_bytes.decode("utf-8-sig"))
    evidence_bytes = evidence_path.read_bytes()
    evidence = json.loads(evidence_bytes.decode("utf-8-sig"))
    if any(index.get("schemaVersion") != 1 or
           index.get("kind") != "betterendfield-voice-catalog-index"
           for index in (legacy, current)):
        raise ValueError("incremental Android preservation requires two schema-1 indexes")
    if evidence.get("schemaVersion") != 1 or evidence.get("platform") != "Android" or \
            evidence.get("kind") != "betterendfield-android-voice-index-merge-evidence":
        raise ValueError("invalid Android legacy merge evidence")
    legacy_sha = hashlib.sha256(legacy_bytes).hexdigest().upper()
    if evidence["legacyIndex"]["sha256"] != legacy_sha or \
            evidence["currentManifestSha256"] != current["sourceManifestSha256"]:
        raise ValueError("legacy merge evidence does not match its input indexes")
    for record in evidence["inputs"]:
        path = (ROOT / record["path"]).resolve()
        if not path.is_relative_to(ROOT) or \
                hashlib.sha256(path.read_bytes()).hexdigest().upper() != record["sha256"]:
            raise ValueError("legacy merge source file SHA-256 mismatch")
    comparison = evidence["audioDialogComparison"]
    if comparison["changedOldRows"] != 0 or comparison["removedOldRows"] != 0 or \
            not evidence["unchangedKoreanResources"] or any(
                record["old"] != record["current"]
                for record in evidence["unchangedKoreanResources"]):
        raise ValueError("legacy Korean resource/table evidence is not unchanged")

    def catalog_map(index: dict[str, Any]) -> dict[tuple[str, int], dict[str, Any]]:
        result: dict[tuple[str, int], dict[str, Any]] = {}
        for catalog in index["catalogs"]:
            key = (catalog["characterId"], catalog["language"])
            if key in result or key[1] not in range(len(LANGUAGES)):
                raise ValueError("duplicate or unsupported Android catalog")
            result[key] = catalog
        return result

    old_catalogs = catalog_map(legacy)
    new_catalogs = catalog_map(current)
    catalogs: list[dict[str, Any]] = []
    for key in sorted(old_catalogs.keys() | new_catalogs.keys(),
                      key=lambda item: (item[0] != "*", item[0], item[1])):
        old = old_catalogs.get(key)
        new = new_catalogs.get(key)
        old_routes = schema1_routes(old) if old else {}
        new_routes = schema1_routes(new) if new else {}
        for source_id in old_routes.keys() & new_routes.keys():
            if old_routes[source_id] != new_routes[source_id]:
                raise ValueError(
                    f"legacy/current schema-1 source ID conflict: {key}/{source_id}"
                )
        routes = {**old_routes, **new_routes}
        catalog = dict(new if new is not None else old)
        catalog["voiceCount"] = max(old.get("voiceCount", 0) if old else 0,
                                    new.get("voiceCount", 0) if new else 0)
        catalog["routes"] = [value for pair in sorted(routes.items()) for value in pair]
        catalog["provenance"] = {
            "currentRouteCount": len(new_routes),
            "legacyOnlyRouteCount": len(old_routes.keys() - new_routes.keys()),
            "legacyOnlyCatalog": new is None,
        }
        if old:
            catalog["provenance"]["legacyIndexSha256"] = legacy_sha
        if new:
            catalog["provenance"]["currentManifestSha256"] = current["sourceManifestSha256"]
        catalogs.append(catalog)

    current_package_languages = {package["language"] for package in current["packages"]}
    retained_packages = [dict(package) for package in legacy["packages"]
                         if package["language"] not in current_package_languages]
    result = {**current, "catalogs": catalogs,
              "packages": [*current["packages"], *retained_packages]}
    result["provenance"] = {
        "mode": "incremental-schema1-preserving-shipped-baseline",
        "currentManifestSha256": current["sourceManifestSha256"],
        "legacyIndex": evidence["legacyIndex"],
        "mergeEvidence": {
            "path": evidence_path.resolve().relative_to(ROOT).as_posix(),
            "sha256": hashlib.sha256(evidence_bytes).hexdigest().upper(),
        },
        "retainedCatalogCount": len(old_catalogs),
        "addedCatalogCount": len(new_catalogs.keys() - old_catalogs.keys()),
        "removedCatalogCount": 0,
        "retainedPackageLanguages": sorted({p["language"] for p in retained_packages}),
        "legacyPackageDescriptorRole": evidence["legacyDescriptorRole"],
        "koreanPayloadRead": evidence["koreanPayloadRead"],
        "newCharacterKoreanStatus": evidence["newCharacterKoreanStatus"],
    }
    return result


def main() -> int:
    args = parse_args()
    output = build_index(args.manifest.resolve(), args.schema_version, args.language)
    if bool(args.legacy_index) != bool(args.merge_evidence):
        raise ValueError("--legacy-index and --merge-evidence must be supplied together")
    if args.legacy_index:
        output = merge_legacy_index(output, args.legacy_index.resolve(), args.merge_evidence.resolve())
    output = {**read_metadata(args.output), **output}
    args.output.parent.mkdir(parents=True, exist_ok=True)
    write_if_changed(args.output,
        json.dumps(output, ensure_ascii=True, separators=(",", ":")) + "\n",
    )
    print(
        json.dumps(
            {
                "output": str(args.output),
                "packageCount": len(output["packages"]),
                "catalogCount": len(output["catalogs"]),
                "sourceManifestSha256": output["sourceManifestSha256"],
            },
            indent=2,
        )
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
