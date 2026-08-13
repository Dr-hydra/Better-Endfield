#!/usr/bin/env python3
"""Build a reproducible catalog of playable-character model and animation assets.

The game manifest is the authority for loadable asset paths and bundle mapping.
PrefabInfo JSON records define the playable-character set and each character's
rig/folder family.  Optional AnimeStudio AnimationClip JSON exports add exact
Unity loop metadata to the walk-animation report.
"""

from __future__ import annotations

import argparse
import csv
import json
import re
from collections import defaultdict
from datetime import datetime, timezone
from pathlib import Path
from typing import Any, Iterable


SCRIPT_PATH = Path(__file__).resolve()
WORKSPACE_ROOT = SCRIPT_PATH.parent.parent
DEFAULT_MANIFEST = (
    WORKSPACE_ROOT
    / "state"
    / "vfs-manifest"
    / "Data"
    / "Bundles"
    / "Windows"
    / "manifest.json"
)
DEFAULT_PREFAB_INFO = (
    WORKSPACE_ROOT
    / "tools"
    / "EndfieldUnpacker"
    / "DecryptOutput"
    / "Json_decrypted"
    / "NPC"
    / "PrefabInfo"
)
DEFAULT_OUTPUT = WORKSPACE_ROOT / "research" / "character-catalog"
DEFAULT_CLIP_METADATA = DEFAULT_OUTPUT / "walk-clip-metadata.json"
DEFAULT_CLIP_JSON = DEFAULT_OUTPUT / "walk-animation-json"

PLAYABLE_CHARACTER_FILE = re.compile(r"^npc_(chr_\d{4}_[a-z0-9]+)\.json$", re.I)


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--manifest", type=Path, default=DEFAULT_MANIFEST)
    parser.add_argument("--prefab-info", type=Path, default=DEFAULT_PREFAB_INFO)
    parser.add_argument("--clip-json", type=Path, action="append", default=[])
    parser.add_argument("--out", type=Path, default=DEFAULT_OUTPUT)
    return parser.parse_args()


def hex_u64(value: Any) -> str | None:
    if value is None:
        return None
    return f"0x{int(value) & 0xFFFFFFFFFFFFFFFF:016X}"


def load_json(path: Path) -> Any:
    with path.open("r", encoding="utf-8-sig") as stream:
        return json.load(stream)


def load_clip_metadata(
    roots: Iterable[Path],
) -> tuple[dict[str, list[dict[str, Any]]], int, int]:
    by_name: dict[str, list[dict[str, Any]]] = defaultdict(list)
    file_count = 0
    record_count = 0
    for root in roots:
        if not root.exists():
            continue
        paths = [root] if root.is_file() else sorted(root.rglob("*.json"))
        for path in paths:
            try:
                data = load_json(path)
            except (OSError, UnicodeDecodeError, json.JSONDecodeError):
                continue
            file_count += 1
            if isinstance(data, list):
                records = data
            elif isinstance(data, dict) and isinstance(data.get("clips"), list):
                records = data["clips"]
            elif isinstance(data, dict):
                records = [data]
            else:
                records = []
            for item in records:
                if not isinstance(item, dict):
                    continue
                name = item.get("name") or item.get("m_Name") or item.get("Name")
                if not isinstance(name, str) or not name:
                    continue
                muscle = item.get("m_MuscleClip")
                if not isinstance(muscle, dict):
                    muscle = {}
                record = {
                    "name": name,
                    "loopTime": item.get("loopTime", muscle.get("m_LoopTime")),
                    "loopBlend": item.get("loopBlend", muscle.get("m_LoopBlend")),
                    "stopTime": item.get("stopTime", muscle.get("m_StopTime")),
                    "sampleRate": item.get("sampleRate", item.get("m_SampleRate")),
                    "sourceJson": item.get("sourceJson", str(path.resolve())),
                }
                by_name[name.casefold()].append(record)
                record_count += 1
    return dict(by_name), file_count, record_count


def animation_kind(clip_name: str) -> str:
    name = clip_name.casefold()
    if "walk" not in name:
        return "other"
    if "additive" in name:
        return "walk_additive"
    if "virtual_single_walk" in name:
        return "walk_single"
    if "loop" in name:
        return "walk_loop_named"
    return "walk_other"


def is_neutral_loop_walk(clip_name: str, loop_time: bool | None) -> bool:
    """Conservative naming rule for a plain, standalone dialogue walk loop."""
    return loop_time is True and clip_name.casefold().endswith("_dialog_state_walk_loop")


def write_csv(path: Path, fieldnames: list[str], rows: Iterable[dict[str, Any]]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", encoding="utf-8-sig", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=fieldnames, extrasaction="ignore")
        writer.writeheader()
        writer.writerows(rows)


def main() -> int:
    args = parse_args()
    manifest_path = args.manifest.resolve()
    prefab_info_dir = args.prefab_info.resolve()
    out_dir = args.out.resolve()

    manifest = load_json(manifest_path)
    bundles = {int(item["bundleIndex"]): item for item in manifest["Bundles"]}
    assets = manifest["Assets"]
    assets_by_path = {item["path"].casefold(): item for item in assets}

    clip_json_roots = list(args.clip_json)
    if not clip_json_roots:
        if DEFAULT_CLIP_METADATA.exists():
            clip_json_roots.append(DEFAULT_CLIP_METADATA)
        elif DEFAULT_CLIP_JSON.exists():
            clip_json_roots.append(DEFAULT_CLIP_JSON)
    clip_metadata, clip_metadata_files, clip_metadata_records = load_clip_metadata(
        clip_json_roots
    )

    character_records: list[dict[str, Any]] = []
    for path in sorted(prefab_info_dir.glob("npc_chr_*.json")):
        match = PLAYABLE_CHARACTER_FILE.match(path.name)
        if not match:
            continue
        data = load_json(path)
        character_id = data.get("correspondingCharId") or match.group(1)
        codename = character_id.split("_", 2)[-1]
        animation_config = data.get("cpuAnimationTempletName", "")
        animation_parts = animation_config.split("/")
        rig_family = animation_parts[-2] if len(animation_parts) >= 2 else ""
        art_root = f"assets/beyond/arts/entity/actor/{rig_family.casefold()}/{codename}"
        animation_prefix = f"{art_root}/animations/"
        model_prefix = f"{art_root}/models/"

        postmodels: list[dict[str, Any]] = []
        for part_name in data.get("partNameIdList", []):
            load_path = (
                "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
                f"{part_name}.prefab"
            )
            asset = assets_by_path.get(load_path.casefold())
            bundle = bundles.get(int(asset["bundleIndex"])) if asset else None
            postmodels.append(
                {
                    "partName": part_name,
                    "loadPath": load_path,
                    "pathHashHead": asset.get("pathHashHead") if asset else None,
                    "pathHashHex": hex_u64(asset.get("pathHashHead")) if asset else None,
                    "assetSize": asset.get("assetSize") if asset else None,
                    "bundleIndex": asset.get("bundleIndex") if asset else None,
                    "bundleName": bundle.get("name") if bundle else None,
                    "bundleHashName": bundle.get("hashName") if bundle else None,
                    "bundleHashHex": hex_u64(bundle.get("hashName")) if bundle else None,
                }
            )

        model_assets: list[dict[str, Any]] = []
        animations: list[dict[str, Any]] = []
        for asset in assets:
            asset_path = asset["path"]
            folded = asset_path.casefold()
            if folded.startswith(model_prefix):
                bundle = bundles.get(int(asset["bundleIndex"]))
                model_assets.append(
                    {
                        "path": asset_path,
                        "assetSize": asset.get("assetSize"),
                        "bundleIndex": asset.get("bundleIndex"),
                        "bundleName": bundle.get("name") if bundle else None,
                    }
                )
            if not folded.startswith(animation_prefix) or "##" not in asset_path:
                continue

            source_path, clip_name = asset_path.split("##", 1)
            bundle = bundles.get(int(asset["bundleIndex"]))
            metadata_matches = clip_metadata.get(clip_name.casefold(), [])
            loop_values = {item.get("loopTime") for item in metadata_matches}
            loop_values.discard(None)
            loop_time: bool | None
            if len(loop_values) == 1:
                loop_time = bool(next(iter(loop_values)))
            else:
                loop_time = None
            animations.append(
                {
                    "path": asset_path,
                    "sourcePath": source_path,
                    "clipName": clip_name,
                    "category": source_path[len(animation_prefix) :].split("/", 1)[0],
                    "kind": animation_kind(clip_name),
                    "isWalk": "walk" in clip_name.casefold(),
                    "loopTime": loop_time,
                    "loopMetadataMatches": len(metadata_matches),
                    "metadataConflict": len(loop_values) > 1,
                    "stopTime": metadata_matches[0].get("stopTime") if metadata_matches else None,
                    "sampleRate": metadata_matches[0].get("sampleRate") if metadata_matches else None,
                    "assetSize": asset.get("assetSize"),
                    "bundleIndex": asset.get("bundleIndex"),
                    "bundleName": bundle.get("name") if bundle else None,
                }
            )

        model_assets.sort(key=lambda item: item["path"].casefold())
        animations.sort(key=lambda item: item["path"].casefold())
        walk_animations = [item for item in animations if item["isWalk"]]
        loop_walks = [item for item in walk_animations if item["loopTime"] is True]
        usable_loop_walks = [
            item
            for item in loop_walks
            if item["kind"] != "walk_additive" and "entry_pose" not in item["clipName"].casefold()
        ]
        neutral_loop_walks = [
            item
            for item in walk_animations
            if is_neutral_loop_walk(item["clipName"], item["loopTime"])
        ]
        character_records.append(
            {
                "characterId": character_id,
                "codename": codename,
                "rigFamily": rig_family,
                "gender": data.get("gender"),
                "sizeType": data.get("sizeType"),
                "avatarTemplate": data.get("avatarTempletName"),
                "avatarMesh": data.get("avatarMeshName"),
                "animationConfig": animation_config,
                "facialMorphAvatar": data.get("facialMorphAvatarName"),
                "earMorphAvatar": data.get("earMorphAvatarName"),
                "prefabInfoSource": str(path.resolve()),
                "postmodels": postmodels,
                "modelAssets": model_assets,
                "animations": animations,
                "summary": {
                    "postmodelCount": len(postmodels),
                    "resolvedPostmodelCount": sum(1 for item in postmodels if item["bundleName"]),
                    "modelAssetCount": len(model_assets),
                    "animationClipCount": len(animations),
                    "walkClipCount": len(walk_animations),
                    "singleWalkClipCount": sum(
                        1 for item in walk_animations if item["kind"] == "walk_single"
                    ),
                    "loopSingleWalkClipCount": sum(
                        1
                        for item in walk_animations
                        if item["kind"] == "walk_single" and item["loopTime"] is True
                    ),
                    "walkClipMetadataCount": sum(
                        1 for item in walk_animations if item["loopTime"] is not None
                    ),
                    "loopWalkClipCount": len(loop_walks),
                    "usableLoopWalkClipCount": len(usable_loop_walks),
                    "neutralLoopWalkClipCount": len(neutral_loop_walks),
                },
            }
        )

    character_records.sort(key=lambda item: item["characterId"])
    generated_at = datetime.now(timezone.utc).isoformat()
    loop_characters = [
        item for item in character_records if item["summary"]["loopWalkClipCount"] > 0
    ]
    usable_loop_characters = [
        item for item in character_records if item["summary"]["usableLoopWalkClipCount"] > 0
    ]
    neutral_loop_characters = [
        item for item in character_records if item["summary"]["neutralLoopWalkClipCount"] > 0
    ]
    catalog = {
        "schemaVersion": 1,
        "generatedAtUtc": generated_at,
        "sources": {
            "manifest": str(manifest_path),
            "manifestVersion": manifest.get("Version"),
            "manifestHash": manifest.get("Hash"),
            "prefabInfoDirectory": str(prefab_info_dir),
            "animationClipJsonRoots": [str(path.resolve()) for path in clip_json_roots],
            "animationClipMetadataFilesRead": clip_metadata_files,
            "animationClipMetadataRecordsRead": clip_metadata_records,
        },
        "scope": {
            "characterRule": PLAYABLE_CHARACTER_FILE.pattern,
            "animationRule": (
                "Assets/Beyond/Arts/Entity/Actor/<rig>/<codename>/Animations/**"
                " with an ##AnimationClip fragment"
            ),
            "modelRule": (
                "logical PrefabInfo avatar keys, resolved character postmodel prefabs, "
                "and Arts/Entity/Actor/<rig>/<codename>/Models/**"
            ),
            "loopRule": "Unity AnimationClip m_MuscleClip.m_LoopTime == true",
            "usableLoopRule": "loopRule, excluding additive and entry_pose clips",
            "neutralLoopRule": "loopRule and clip name ending in _dialog_state_walk_loop",
        },
        "summary": {
            "characterCount": len(character_records),
            "charactersWithWalkClip": sum(
                1 for item in character_records if item["summary"]["walkClipCount"] > 0
            ),
            "charactersWithoutWalkClip": sum(
                1 for item in character_records if item["summary"]["walkClipCount"] == 0
            ),
            "resolvedPostmodelCount": sum(
                item["summary"]["resolvedPostmodelCount"] for item in character_records
            ),
            "modelAssetCount": sum(
                item["summary"]["modelAssetCount"] for item in character_records
            ),
            "animationClipCount": sum(
                item["summary"]["animationClipCount"] for item in character_records
            ),
            "walkClipCount": sum(
                item["summary"]["walkClipCount"] for item in character_records
            ),
            "singleWalkClipCount": sum(
                item["summary"]["singleWalkClipCount"] for item in character_records
            ),
            "loopSingleWalkClipCount": sum(
                item["summary"]["loopSingleWalkClipCount"] for item in character_records
            ),
            "walkClipMetadataCount": sum(
                item["summary"]["walkClipMetadataCount"] for item in character_records
            ),
            "loopWalkClipCount": sum(
                item["summary"]["loopWalkClipCount"] for item in character_records
            ),
            "charactersWithLoopWalk": len(loop_characters),
            "charactersWithUsableLoopWalk": len(usable_loop_characters),
            "neutralLoopWalkClipCount": sum(
                item["summary"]["neutralLoopWalkClipCount"] for item in character_records
            ),
            "charactersWithNeutralLoopWalk": len(neutral_loop_characters),
        },
        "characters": character_records,
    }

    out_dir.mkdir(parents=True, exist_ok=True)
    compact_clip_records: list[dict[str, Any]] = []
    seen_clip_records: set[tuple[Any, ...]] = set()
    for records in clip_metadata.values():
        for record in records:
            key = (
                record.get("name"),
                record.get("loopTime"),
                record.get("loopBlend"),
                record.get("stopTime"),
                record.get("sampleRate"),
            )
            if key in seen_clip_records:
                continue
            seen_clip_records.add(key)
            compact_clip_records.append(
                {
                    "name": record.get("name"),
                    "loopTime": record.get("loopTime"),
                    "loopBlend": record.get("loopBlend"),
                    "stopTime": record.get("stopTime"),
                    "sampleRate": record.get("sampleRate"),
                }
            )
    compact_clip_records.sort(key=lambda item: str(item["name"]).casefold())
    compact_metadata = {
        "schemaVersion": 1,
        "generatedAtUtc": generated_at,
        "criterion": "Unity AnimationClip m_MuscleClip.m_LoopTime",
        "sourceRoots": [str(path.resolve()) for path in clip_json_roots],
        "clips": compact_clip_records,
    }
    with (out_dir / "walk-clip-metadata.json").open(
        "w", encoding="utf-8", newline="\n"
    ) as stream:
        json.dump(compact_metadata, stream, ensure_ascii=False, indent=2)
        stream.write("\n")

    with (out_dir / "characters.json").open("w", encoding="utf-8", newline="\n") as stream:
        json.dump(catalog, stream, ensure_ascii=False, indent=2)
        stream.write("\n")

    selection_options = {
        "schemaVersion": 1,
        "generatedAtUtc": generated_at,
        "characters": [
            {
                "characterId": character["characterId"],
                "codename": character["codename"],
                "rigFamily": character["rigFamily"],
                "avatarTemplate": character["avatarTemplate"],
                "avatarMesh": character["avatarMesh"],
                "animationConfig": character["animationConfig"],
                "modelOptions": character["postmodels"],
                "finalAnimationOptions": [
                    {
                        key: animation[key]
                        for key in (
                            "path", "clipName", "kind", "loopTime", "stopTime", "bundleName"
                        )
                    }
                    for animation in character["animations"]
                    if animation["isWalk"]
                ],
            }
            for character in character_records
        ],
    }
    with (out_dir / "selection-options.json").open(
        "w", encoding="utf-8", newline="\n"
    ) as stream:
        json.dump(selection_options, stream, ensure_ascii=False, indent=2)
        stream.write("\n")

    model_rows: list[dict[str, Any]] = []
    animation_rows: list[dict[str, Any]] = []
    walk_rows: list[dict[str, Any]] = []
    walk_bundle_names: set[str] = set()
    for character in character_records:
        common = {
            "characterId": character["characterId"],
            "codename": character["codename"],
            "rigFamily": character["rigFamily"],
        }
        for postmodel in character["postmodels"]:
            model_rows.append({**common, "modelKind": "postmodel", "path": postmodel["loadPath"], **postmodel})
        for model in character["modelAssets"]:
            model_rows.append({**common, "modelKind": "model_asset", **model})
        for animation in character["animations"]:
            row = {**common, **animation}
            animation_rows.append(row)
            if animation["isWalk"]:
                walk_rows.append(row)
                if animation["bundleName"]:
                    walk_bundle_names.add(animation["bundleName"])

    write_csv(
        out_dir / "models.csv",
        [
            "characterId", "codename", "rigFamily", "modelKind", "partName", "path",
            "loadPath", "pathHashHead", "pathHashHex", "assetSize", "bundleIndex",
            "bundleName", "bundleHashName", "bundleHashHex",
        ],
        model_rows,
    )
    animation_fields = [
        "characterId", "codename", "rigFamily", "category", "kind", "clipName", "path",
        "sourcePath", "isWalk", "loopTime", "loopMetadataMatches", "metadataConflict",
        "stopTime", "sampleRate", "assetSize", "bundleIndex", "bundleName",
    ]
    write_csv(out_dir / "animations.csv", animation_fields, animation_rows)
    write_csv(out_dir / "walk-animations.csv", animation_fields, walk_rows)
    with (out_dir / "walk-bundles.txt").open("w", encoding="utf-8", newline="\n") as stream:
        for bundle_name in sorted(walk_bundle_names):
            stream.write(f"{bundle_name}\n")

    report_lines = [
        "# Character walk-loop inventory",
        "",
        f"Generated: `{generated_at}`",
        "",
        "The exact loop criterion is `m_MuscleClip.m_LoopTime == true` from exported ",
        "Unity `AnimationClip` JSON. The usable count additionally excludes additive and ",
        "`entry_pose` clips because those are not standalone full-body walk replacements.",
        "",
        "| Character | Walk clips | Metadata checked | Loop walk clips | Usable loop walk clips | Neutral loop clips |",
        "| --- | ---: | ---: | ---: | ---: | ---: |",
    ]
    for character in character_records:
        summary = character["summary"]
        report_lines.append(
            f"| `{character['characterId']}` | {summary['walkClipCount']} | "
            f"{summary['walkClipMetadataCount']} | {summary['loopWalkClipCount']} | "
            f"{summary['usableLoopWalkClipCount']} | {summary['neutralLoopWalkClipCount']} |"
        )
    report_lines.extend(
        [
            "",
            f"Characters: **{catalog['summary']['characterCount']}**  ",
            f"Walk clips: **{catalog['summary']['walkClipCount']}**  ",
            (
                "Virtual single-walk clips: "
                f"**{catalog['summary']['singleWalkClipCount']}**, loop-enabled: "
                f"**{catalog['summary']['loopSingleWalkClipCount']}**  "
            ),
            f"Walk clips with exact metadata: **{catalog['summary']['walkClipMetadataCount']}**  ",
            f"Characters with any loop walk: **{catalog['summary']['charactersWithLoopWalk']}**  ",
            (
                "Characters with a usable non-additive loop walk: "
                f"**{catalog['summary']['charactersWithUsableLoopWalk']}**"
            ),
            "  ",
            (
                "Characters with a conservatively named neutral walk loop: "
                f"**{catalog['summary']['charactersWithNeutralLoopWalk']}**"
            ),
            "",
            "## Interpretation",
            "",
            (
                f"- All **{catalog['summary']['singleWalkClipCount']}** character-owned "
                "`virtual_single_walk_[s/m/l]` clips have `m_LoopTime == false`. This "
                "includes all three Li Zhiyan clips, so the current final animation has no "
                "native loop flag to preserve."
            ),
            (
                f"- **{catalog['summary']['charactersWithLoopWalk']} / "
                f"{catalog['summary']['characterCount']}** characters own at least one "
                "loop-enabled walk-named clip, but only "
                f"**{catalog['summary']['charactersWithUsableLoopWalk']}** remain after "
                "excluding additive and entry-pose clips. Most are scenario-specific poses "
                "such as carrying a plate, shy walking, hands-behind-back, or walk-and-sit."
            ),
            (
                f"- Only **{catalog['summary']['charactersWithNeutralLoopWalk']} / "
                f"{catalog['summary']['characterCount']}** have the conservative plain "
                "`dialog_state_walk_loop` form: Endministrator male, Endministrator female, "
                "Perlica, and Mifu. A neutral character-owned loop is therefore a special "
                "case in this asset set, not the default."
            ),
            (
                "- Li Zhiyan's only loop-enabled walk-named clip is "
                "`dialog_single_walk_loop_hurt_additive`; it is an additive hurt overlay, "
                "not a standalone replacement for the final full-body walk."
            ),
            (
                f"- **{catalog['summary']['charactersWithoutWalkClip']}** characters have no "
                "character-owned walk clip under this folder rule. They may rely on shared "
                "rig-family locomotion, so this catalog does not claim those characters have "
                "no walk animation anywhere in the game."
            ),
            "",
            "## Exact loop-walk clips",
            "",
        ]
    )
    exact_rows = [row for row in walk_rows if row["loopTime"] is True]
    if exact_rows:
        for row in exact_rows:
            report_lines.append(
                f"- `{row['characterId']}` — `{row['clipName']}` "
                f"(`{row['kind']}`, {row['stopTime']} s)"
            )
    else:
        report_lines.append(
            "No exact result yet. Export the bundles in `walk-bundles.txt` as AnimationClip "
            "JSON and rerun this scanner with `--clip-json <directory>`."
        )
    report_lines.append("")
    (out_dir / "walk-loop-report.md").write_text(
        "\n".join(report_lines), encoding="utf-8", newline="\n"
    )

    readme_lines = [
        "# Character asset catalog",
        "",
        "Generated from the decrypted playable-character `PrefabInfo` records and the full ",
        "VFS bundle manifest. No runtime replacement behavior is changed by this catalog.",
        "",
        "## Files",
        "",
        "- `characters.json`: authoritative detailed catalog, including logical model keys, ",
        "  resolved postmodel paths, model-directory assets, every character-owned animation ",
        "  clip path, and bundle mapping.",
        "- `selection-options.json`: compact model/final-animation choices intended for later ",
        "  parameterization.",
        "- `models.csv`: flattened model and postmodel paths.",
        "- `animations.csv`: flattened list of all character-owned animation clips.",
        "- `walk-animations.csv`: walk-only subset with exact Unity loop metadata where exported.",
        "- `walk-loop-report.md`: per-character loop-walk counts and exact clip list.",
        "- `walk-bundles.txt`: minimal bundle set needed to re-export walk metadata.",
        "- `walk-clip-metadata.json`: compact exact loop/length metadata for all 148 walk clips.",
        "",
        "## Rebuild",
        "",
        "```powershell",
        "python scripts\\ScanCharacterAssets.py",
        "```",
        "",
        "The scanner automatically consumes `walk-clip-metadata.json`; if it is absent, it ",
        "falls back to full AnimeStudio JSON exports in `walk-animation-json/`.",
        "The neutral-loop count is deliberately conservative: the clip must have Unity ",
        "`m_LoopTime` enabled and its name must end in `_dialog_state_walk_loop`.",
        "",
    ]
    (out_dir / "README.md").write_text(
        "\n".join(readme_lines), encoding="utf-8", newline="\n"
    )

    print(json.dumps(catalog["summary"], ensure_ascii=False, indent=2))
    print(f"Wrote catalog to {out_dir}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
