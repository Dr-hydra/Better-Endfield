#!/usr/bin/env python3
"""Generate the compact routing index embedded by the Better Endfield UI."""

from __future__ import annotations

import argparse
import hashlib
import json
from pathlib import Path
from typing import Any

from BuildVoiceCatalog import (
    LANGUAGES,
    collect_routes,
    load_json,
    target_package_for,
)


ROOT = Path(__file__).resolve().parents[1]


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument(
        "--manifest",
        type=Path,
        default=ROOT / "manifests" / "voice" / "voice-event-media-manifest.json",
    )
    parser.add_argument(
        "--output",
        type=Path,
        default=ROOT
        / "ui"
        / "BetterEndfield.UI"
        / "Assets"
        / "voice"
        / "voice-catalog-index.json",
    )
    return parser.parse_args()


def build_index(manifest_path: Path) -> dict[str, Any]:
    manifest_bytes = manifest_path.read_bytes()
    manifest = load_json(manifest_path)
    if manifest.get("kind") != "endfield-voice-event-media-manifest":
        raise ValueError("voice manifest kind is not recognized")

    packages: list[dict[str, Any]] = []
    for language_index, language in enumerate(LANGUAGES):
        package = target_package_for(manifest, language)
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
            routes, voice_count = collect_routes(manifest, language, filter_id)
            if not routes:
                continue
            flattened: list[int] = []
            for source_id, target_id in sorted(routes.items()):
                flattened.extend((source_id, target_id))
            catalogs.append(
                {
                    "characterId": character_id,
                    "language": language_index,
                    "voiceCount": voice_count,
                    "routes": flattened,
                }
            )

    return {
        "schemaVersion": 1,
        "kind": "betterendfield-voice-catalog-index",
        "sourceManifestSha256": hashlib.sha256(manifest_bytes).hexdigest().upper(),
        "packages": packages,
        "catalogs": catalogs,
    }


def main() -> int:
    args = parse_args()
    output = build_index(args.manifest.resolve())
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(
        json.dumps(output, ensure_ascii=True, separators=(",", ":")) + "\n",
        encoding="utf-8",
        newline="\n",
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
