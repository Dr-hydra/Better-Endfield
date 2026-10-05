#!/usr/bin/env python3
"""Refresh compact combat-overlay portraits from the CEP character index."""

from __future__ import annotations

import argparse
import hashlib
import html as html_module
import io
import json
import os
import re
import urllib.request
from pathlib import Path

from workspace_config import load_workspace
from RefreshEndfieldResourceInputs import configured_path, publish_changes


INDEX_URL = "https://end.canmoe.com/zh-CN/wiki/characters"

CARD_PATTERN = re.compile(
    r'href="/zh-CN/wiki/characters/(?P<id>chr_[^"]+)"[\s\S]*?'
    r'data-testid="rarity-frame-image"[\s\S]*?src="(?P<src>/images/characters/[^"]+)"'
    r'[\s\S]*?<h3[^>]*>(?P<name>[^<]+)</h3>'
)

# The website index only contains the current display entries.  Keep the
# English labels used by the overlay and the legacy aliases used by older
# combat records in the generated native table.
ENGLISH_NAMES = {
    "chr_0025_ardelia": "Ardelia",
    "chr_0026_lastrite": "Lastrite",
    "chr_9000_endmin": "Endministrator",
    "chr_0013_aglina": "Gilberta",
    "chr_0032_lizhiyan": "Li Zhiyan",
    "chr_0029_pograni": "Pograni",
    "chr_0033_camille": "Camille",
    "chr_0016_laevat": "Laevatain",
    "chr_0035_liino": "Reno",
    "chr_0015_lifeng": "Lifeng",
    "chr_0028_wulfa": "Wulfa",
    "chr_0031_mifu": "Mifu",
    "chr_0027_tangtang": "Tangtang",
    "chr_0034_typhoea": "Typhoea",
    "chr_0017_yvonne": "Yvonne",
    "chr_0009_azrila": "Ember",
    "chr_0030_zhuangfy": "Zhuang Fangyi",
    "chr_0024_deepfin": "Alesh",
    "chr_0012_avywenna": "Avywenna",
    "chr_0005_chen": "Chen Qianyu",
    "chr_0018_dapan": "Da Pan",
    "chr_0007_ikut": "Ikut",
    "chr_0006_wolfgd": "Wulfgard",
    "chr_0004_pelica": "Perlica",
    "chr_0038_purrche": "Purrche",
    "chr_0011_seraph": "Xaihi",
    "chr_0014_aurora": "Snowshine",
    "chr_0021_whiten": "Aethel",
    "chr_0023_antal": "Antal",
    "chr_0020_meurs": "Catcher",
    "chr_0019_karin": "Karin",
    "chr_0022_bounda": "Fluorite",
}

LEGACY_ALIASES = (
    ("chr_0002_endminm", "管理员（男）", "Endministrator (Male)", "chr_9000_endmin"),
    ("chr_0003_endminf", "管理员（女）", "Endministrator (Female)", "chr_9000_endmin"),
    ("chr_0036_jsspsi", "祀", "Si", "chr_0025_ardelia"),
)


def download(url: str) -> bytes:
    request = urllib.request.Request(url, headers={"User-Agent": "BetterEndfield/2.1"})
    with urllib.request.urlopen(request, timeout=30) as response:
        return response.read()


def cpp_wstring(value: str) -> str:
    return value.replace("\\", "\\\\").replace('"', '\\"')


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--workspace-config", type=Path)
    parser.add_argument("--output", type=Path)
    parser.add_argument("--overlay-output", type=Path)
    parser.add_argument("--index-url")
    args = parser.parse_args()
    args.workspace = ws = load_workspace(args.workspace_config)
    args.output = args.output or ws.path("resource_update.outputs.avatars")
    args.overlay_output = args.overlay_output or configured_path(ws, "resource_update.outputs.avatar_overlay", ws.root / "native/modules/combat_stats/overlay")
    args.index_url = args.index_url or ws.get("resource_update.avatar_index_url", INDEX_URL)
    return args


def main() -> None:
    args = parse_args()
    from PIL import Image, ImageOps, UnidentifiedImageError
    from urllib.parse import urljoin

    asset_dir, overlay_dir = args.output, args.overlay_output
    page = download(args.index_url).decode("utf-8")
    pending: dict[Path, bytes | str] = {}
    records: list[dict[str, object]] = []
    seen: set[str] = set()
    for match in CARD_PATTERN.finditer(page):
        character_id = match.group("id")
        if character_id in seen:
            continue
        seen.add(character_id)
        source = html_module.unescape(match.group("src"))
        name = html_module.unescape(match.group("name"))
        records.append({
            "id": character_id,
            "name": name,
            "nameEn": ENGLISH_NAMES.get(character_id, name),
            "source": source,
        })

    if len(records) < 25:
        raise RuntimeError(f"Character index yielded only {len(records)} records")

    for index, record in enumerate(records):
        source_url = urljoin(args.index_url, str(record["source"]))
        source_bytes = download(source_url)
        output_path = asset_dir / f"{record['id']}.png"
        try:
            with Image.open(io.BytesIO(source_bytes)) as source_image:
                portrait = ImageOps.fit(
                    source_image.convert("RGBA"),
                    (112, 112),
                    method=Image.Resampling.LANCZOS,
                    centering=(0.5, 0.35),
                )
                encoded = io.BytesIO()
                portrait.save(encoded, format="PNG", optimize=True)
                pending[output_path] = encoded.getvalue()
        except UnidentifiedImageError:
            # Some Python installations do not include an AVIF decoder.  A
            # previously generated PNG is still a valid local asset; retain it
            # and continue refreshing the manifest and native resource table.
            if not output_path.is_file():
                raise RuntimeError(
                    f"Cannot decode {source_url} and no existing PNG is available"
                )
        record["resourceId"] = 1000 + index
        record["sourceUrl"] = source_url
        record["sourceSha256"] = hashlib.sha256(source_bytes).hexdigest()

    resources = {str(record["id"]): int(record["resourceId"]) for record in records}
    aliases = [
        {"id": alias_id, "name": name, "nameEn": name_en,
         "resourceId": resources.get(target, 1000)}
        for alias_id, name, name_en, target in LEGACY_ALIASES
    ]

    header_lines = [
        "// Generated by scripts/UpdateCombatAvatars.py. Do not edit by hand.",
        "#pragma once",
        "",
        "struct CharacterAsset {",
        "    const char* id;",
        "    const wchar_t* name;",
        "    const wchar_t* name_en;",
        "    int resource_id;",
        "};",
        "",
        "inline constexpr CharacterAsset kCharacterAssets[] = {",
    ]
    rc_lines = ["// Generated by scripts/UpdateCombatAvatars.py."]
    for record in records + aliases:
        header_lines.append(
            f'    {{"{record["id"]}", L"{cpp_wstring(str(record["name"]))}", '
            f'L"{cpp_wstring(str(record["nameEn"]))}", '
            f'{record["resourceId"]}}},'
        )
    for record in records:
        rc_lines.append(
            f'{record["resourceId"]} PNG "{Path(os.path.relpath(asset_dir / (str(record["id"]) + ".png"), overlay_dir)).as_posix()}"'
        )
    header_lines.extend(["};", ""])

    pending[overlay_dir / "character_assets.generated.h"] = "\n".join(header_lines)
    pending[overlay_dir / "avatars.generated.rcinc"] = "\n".join(rc_lines) + "\n"
    manifest = {
        "schemaVersion": 1,
        "source": args.index_url,
        "characters": records,
    }
    pending[asset_dir.parent / "characters.json"] = json.dumps(manifest, ensure_ascii=False, indent=2) + "\n"
    # Finish all downloads/decoding before replacing valid local assets.
    args.workspace.env()
    publish_changes(list(pending.items()), args.workspace.path("paths.temp"))
    print(f"Updated {len(records)} combat portraits in {asset_dir}")


if __name__ == "__main__":
    main()
