#!/usr/bin/env python3
"""Generate the compact character/action catalog consumed by the WinUI app."""

from __future__ import annotations

import argparse
import json
from pathlib import Path
from typing import Any


REPO_ROOT = Path(__file__).resolve().parents[1]
DEFAULT_MANIFEST = REPO_ROOT / "state/vfs-manifest/Data/Bundles/Windows/manifest.json"
DEFAULT_CATALOG = REPO_ROOT / "research/character-catalog/characters.json"
DEFAULT_OUTPUT = (
    REPO_ROOT / "ui/BetterEndfield.UI/Assets/model/character-presets.json"
)


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser()
    parser.add_argument("--manifest", type=Path, default=DEFAULT_MANIFEST)
    parser.add_argument("--catalog", type=Path, default=DEFAULT_CATALOG)
    parser.add_argument("--output", type=Path, default=DEFAULT_OUTPUT)
    return parser.parse_args()


def load_json(path: Path) -> dict[str, Any]:
    with path.open("r", encoding="utf-8-sig") as stream:
        return json.load(stream)


def hex_u64(value: int) -> str:
    return f"0x{value:016X}"


def display_asset(path: str, clip_name: str | None = None) -> str:
    source = path.split("##", 1)[0]
    filename = source.rsplit("/", 1)[-1]
    return f"{filename}##{clip_name}" if clip_name else filename


def choose_default_action(actions: list[dict[str, Any]], character_id: str) -> str:
    preferred_names = {
        "chr_0013_aglina": "a_actor_aglina_dialog_state_shy2_walk_loop",
        "chr_0032_lizhiyan": "a_actor_lizhiyan_dialog_virtual_single_walk_l",
    }
    preferred = preferred_names.get(character_id)
    if preferred and any(action["id"] == preferred for action in actions):
        return preferred

    def score(action: dict[str, Any]) -> tuple[int, str]:
        name = action["id"].casefold()
        if name.endswith("_dialog_state_walk_loop"):
            rank = 0
        elif action["nativeLoop"] and "additive" not in name and "entry_pose" not in name:
            rank = 1
        elif name.endswith("_dialog_virtual_single_walk_l"):
            rank = 2
        elif name.endswith("_dialog_virtual_single_walk_m"):
            rank = 3
        elif name.endswith("_dialog_virtual_single_walk_s"):
            rank = 4
        else:
            rank = 5
        return rank, name

    return min(actions, key=score)["id"]


def main() -> int:
    args = parse_args()
    catalog = load_json(args.catalog.resolve())

    needed_paths: set[str] = set()
    for character in catalog["characters"]:
        needed_paths.update(
            model["loadPath"].casefold() for model in character["postmodels"]
        )
        needed_paths.update(
            animation["path"].casefold() for animation in character["animations"]
        )

    manifest = load_json(args.manifest.resolve())
    bundles = {int(item["bundleIndex"]): item for item in manifest["Bundles"]}
    assets: dict[str, dict[str, Any]] = {}
    for asset in manifest["Assets"]:
        folded = asset["path"].casefold()
        if folded in needed_paths:
            assets[folded] = asset

    output_characters: list[dict[str, Any]] = []
    skipped: list[dict[str, str]] = []
    for character in catalog["characters"]:
        character_id = character["characterId"]
        codename = character["codename"]
        model = next(
            (
                item
                for item in character["postmodels"]
                if item.get("pathHashHead") is not None
                and item.get("bundleHashName") is not None
            ),
            None,
        )
        animations_by_name = {
            animation["clipName"].casefold(): animation
            for animation in character["animations"]
        }
        required_names = {
            "sitLoop": f"a_actor_{codename}_interact_sit_loop",
            "sitSpecial": f"a_actor_{codename}_interact_sit_sp",
            "sitToWalk": f"a_actor_{codename}_interact_sit_end",
        }
        required = {
            key: animations_by_name.get(name.casefold())
            for key, name in required_names.items()
        }

        if model is None or any(value is None for value in required.values()):
            skipped.append({"characterId": character_id, "reason": "incomplete model/sit chain"})
            continue

        actions: list[dict[str, Any]] = []
        for animation in character["animations"]:
            asset = assets.get(animation["path"].casefold())
            if asset is None or asset.get("pathHashHead") is None:
                continue
            actions.append(
                {
                    "id": animation["clipName"],
                    "displayName": display_asset(
                        animation["path"], animation["clipName"]
                    ),
                    "path": animation["path"],
                    "pathHash": hex_u64(int(asset["pathHashHead"])),
                    "nativeLoop": animation["loopTime"] is True,
                    "duration": animation["stopTime"],
                }
            )

        id_counts: dict[str, int] = {}
        for action in actions:
            id_counts[action["id"]] = id_counts.get(action["id"], 0) + 1
        for action in actions:
            if id_counts[action["id"]] > 1:
                action["id"] = f'{action["id"]}@{action["pathHash"]}'

        if not actions:
            skipped.append({"characterId": character_id, "reason": "no resolved animation assets"})
            continue

        actions.sort(key=lambda item: item["displayName"].casefold())

        clip_assets: dict[str, dict[str, Any]] = {}
        missing_hash = False
        for key, animation in required.items():
            assert animation is not None
            asset = assets.get(animation["path"].casefold())
            if asset is None or asset.get("pathHashHead") is None:
                missing_hash = True
                break
            clip_assets[key] = {
                "displayName": display_asset(animation["path"], animation["clipName"]),
                "path": animation["path"],
                "pathHash": hex_u64(int(asset["pathHashHead"])),
            }
        if missing_hash:
            skipped.append({"characterId": character_id, "reason": "sit asset hash unavailable"})
            continue

        model_asset = assets[model["loadPath"].casefold()]
        model_bundle = bundles[int(model_asset["bundleIndex"])]
        output_characters.append(
            {
                "id": character_id,
                "displayName": display_asset(model["loadPath"]),
                "model": {
                    "path": model["loadPath"],
                    "pathHash": hex_u64(int(model_asset["pathHashHead"])),
                    "bundleHash": hex_u64(int(model_bundle["hashName"])),
                },
                **clip_assets,
                "defaultActionId": choose_default_action(actions, character_id),
                "actions": actions,
            }
        )

    output_characters.sort(key=lambda item: item["id"])
    result = {
        "schemaVersion": 1,
        "manifestVersion": manifest["Version"],
        "manifestHash": manifest.get("Hash"),
        "characters": output_characters,
        "skippedCharacters": skipped,
    }
    args.output.parent.mkdir(parents=True, exist_ok=True)
    with args.output.open("w", encoding="utf-8", newline="\n") as stream:
        json.dump(result, stream, ensure_ascii=True, indent=2)
        stream.write("\n")

    print(
        f"Wrote {len(output_characters)} character presets and "
        f"{sum(len(item['actions']) for item in output_characters)} actions "
        f"to {args.output.resolve()}"
    )
    if skipped:
        print("Skipped: " + ", ".join(item["characterId"] for item in skipped))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
