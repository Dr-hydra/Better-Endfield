#!/usr/bin/env python3
"""Convert the validated Endmin Casualwear EFMI sample into BEM PoC-3 raw-stream data.

PoC-3 deliberately preserves the original EFMI GPU vertex streams instead of
round-tripping them through Unity's high-level Mesh channel setters. The runtime
clones the game's original C9 Mesh (preserving its Unity vertex declaration) and
overwrites only the first custom vertex range in each stream.

Payload order after the fixed header:
  Component9_VB0
  Component9_VB1          (LOD0)
  Component9_VB2          (LOD0)
  Component9_VB1_LOD1
  Component9_VB2_LOD1
  Component9_IB
"""

from __future__ import annotations

import argparse
import json
from pathlib import Path
import re
import struct
import sys
import zipfile

MAGIC = b"BEMPOC3\0"
VERSION = 3
COMPONENT_ID = 9
VERTEX_COUNT = 12_873
INDEX_COUNT = 59_073
LOD0_ORIGINAL_INDEX_COUNT = 101_994
LOD1_ORIGINAL_INDEX_COUNT = 59_301
VB0_STRIDE = 16
VB1_LOD0_STRIDE = 12
VB2_LOD0_STRIDE = 12
VB1_LOD1_STRIDE = 8
VB2_LOD1_STRIDE = 12
EXPECTED_MAX_BONE_LOD0 = 155
EXPECTED_MAX_BONE_LOD1 = 148

# magic + 13 uint32 values
HEADER = struct.Struct("<8s13I")


class Source:
    def __init__(self, path: Path):
        self.path = path
        self._zip = zipfile.ZipFile(path) if path.is_file() and path.suffix.lower() == ".zip" else None
        if self._zip:
            self.names = self._zip.namelist()
        elif path.is_dir():
            self.names = [str(p.relative_to(path)).replace("\\", "/") for p in path.rglob("*") if p.is_file()]
        else:
            raise ValueError(f"source must be an EFMI mod folder or .zip: {path}")

    def close(self) -> None:
        if self._zip:
            self._zip.close()

    def _find(self, suffix: str) -> str:
        matches = [n for n in self.names if n.replace("\\", "/").endswith(suffix)]
        if len(matches) != 1:
            raise ValueError(f"expected exactly one '*{suffix}', found {len(matches)}")
        return matches[0]

    def read(self, suffix: str) -> bytes:
        name = self._find(suffix)
        if self._zip:
            return self._zip.read(name)
        return (self.path / Path(name)).read_bytes()


def parse_ini_int(text: str, key: str) -> int | None:
    match = re.search(rf"(?mi)^\s*\$?{re.escape(key)}\s*=\s*([^\r\n;]+)", text)
    if not match:
        return None
    return int(match.group(1).strip(), 0)


def validate_components_json(raw: bytes) -> None:
    meta = json.loads(raw.decode("utf-8-sig"))
    required = {
        "Component9_VB0": ("POSITION", "ENCODEDDATA"),
        "Component9_VB1": ("TEXCOORD", "COLOR"),
        "Component9_VB2": ("BLENDWEIGHTS", "BLENDINDICES"),
        "Component9_VB1_LOD1": ("TEXCOORD",),
        "Component9_VB2_LOD1": ("BLENDWEIGHTS", "BLENDINDICES"),
        "Component9_IB": ("INDEX",),
    }
    for name, semantics in required.items():
        if name not in meta:
            raise ValueError(f"Components.buf missing {name}")
        blob = json.dumps(meta[name], ensure_ascii=False)
        for semantic in semantics:
            if semantic not in blob:
                raise ValueError(f"{name} missing semantic {semantic}")


def validate_vertex_stream(blob: bytes, stride: int, label: str) -> None:
    expected = VERTEX_COUNT * stride
    if len(blob) != expected:
        raise ValueError(f"{label} size={len(blob)} expected={expected} ({VERTEX_COUNT}*{stride})")


def validate_skin(blob: bytes, label: str, expected_max: int) -> tuple[int, int]:
    max_bone = 0
    weighted_slots: set[int] = set()
    for off in range(0, len(blob), 12):
        weights = struct.unpack_from("<4H", blob, off)
        bones = struct.unpack_from("<4B", blob, off + 8)
        if sum(weights) != 65535:
            raise ValueError(f"{label} weights do not sum to 65535 at vertex {off // 12}")
        max_bone = max(max_bone, *bones)
        for bone, weight in zip(bones, weights):
            if weight:
                weighted_slots.add(bone)
    if max_bone != expected_max:
        raise ValueError(f"{label} max bone={max_bone} expected={expected_max}")
    return max_bone, len(weighted_slots)


def validate_indices(blob: bytes) -> None:
    if len(blob) != INDEX_COUNT * 2:
        raise ValueError(f"Component9_IB size={len(blob)} expected={INDEX_COUNT * 2}")
    values = struct.unpack("<" + "H" * INDEX_COUNT, blob)
    if max(values) >= VERTEX_COUNT:
        raise ValueError(f"Component9_IB references vertex {max(values)} >= {VERTEX_COUNT}")


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("source", type=Path, help="EFMI mod folder or zip")
    parser.add_argument("-o", "--output", type=Path, required=True,
                        help="output .bempoc path under BetterEndfield catalog/custom-model")
    args = parser.parse_args()

    src = Source(args.source)
    try:
        ini = src.read("mod.ini").decode("utf-8-sig", errors="strict")
        if "EFMI ALPHA-12 INI" not in ini:
            raise ValueError("sample is not EFMI ALPHA-12")
        component_count = parse_ini_int(ini, "component_count")
        if component_count not in (None, 11):
            raise ValueError(f"unexpected component_count={component_count}")
        validate_components_json(src.read("Meshes/Components.buf"))

        vb0 = src.read("Meshes/Component9_VB0.buf")
        vb1_l0 = src.read("Meshes/Component9_VB1.buf")
        vb2_l0 = src.read("Meshes/Component9_VB2.buf")
        vb1_l1 = src.read("Meshes/Component9_VB1_LOD1.buf")
        vb2_l1 = src.read("Meshes/Component9_VB2_LOD1.buf")
        ib = src.read("Meshes/Component9_IB.buf")
    finally:
        src.close()

    validate_vertex_stream(vb0, VB0_STRIDE, "VB0")
    validate_vertex_stream(vb1_l0, VB1_LOD0_STRIDE, "VB1 LOD0")
    validate_vertex_stream(vb2_l0, VB2_LOD0_STRIDE, "VB2 LOD0")
    validate_vertex_stream(vb1_l1, VB1_LOD1_STRIDE, "VB1 LOD1")
    validate_vertex_stream(vb2_l1, VB2_LOD1_STRIDE, "VB2 LOD1")
    validate_indices(ib)
    max_l0, used_l0 = validate_skin(vb2_l0, "VB2 LOD0", EXPECTED_MAX_BONE_LOD0)
    max_l1, used_l1 = validate_skin(vb2_l1, "VB2 LOD1", EXPECTED_MAX_BONE_LOD1)

    header = HEADER.pack(
        MAGIC,
        VERSION,
        COMPONENT_ID,
        VERTEX_COUNT,
        INDEX_COUNT,
        max_l0,
        max_l1,
        LOD0_ORIGINAL_INDEX_COUNT,
        LOD1_ORIGINAL_INDEX_COUNT,
        VB0_STRIDE,
        VB1_LOD0_STRIDE,
        VB2_LOD0_STRIDE,
        VB1_LOD1_STRIDE,
        VB2_LOD1_STRIDE,
    )

    args.output.parent.mkdir(parents=True, exist_ok=True)
    with args.output.open("wb") as out:
        out.write(header)
        out.write(vb0)
        out.write(vb1_l0)
        out.write(vb2_l0)
        out.write(vb1_l1)
        out.write(vb2_l1)
        out.write(ib)

    print(f"wrote {args.output}")
    print(f"component=9 vertices={VERTEX_COUNT} indices={INDEX_COUNT}")
    print(f"lod0 originalIndices={LOD0_ORIGINAL_INDEX_COUNT} maxBone={max_l0} weightedBoneSlots={used_l0}")
    print(f"lod1 originalIndices={LOD1_ORIGINAL_INDEX_COUNT} maxBone={max_l1} weightedBoneSlots={used_l1}")
    print("streams=RAW(VB0 + VB1_LOD0 + VB2_LOD0 + VB1_LOD1 + VB2_LOD1)")
    print(f"size={args.output.stat().st_size} bytes")
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Exception as exc:
        print(f"error: {exc}", file=sys.stderr)
        raise SystemExit(2)
