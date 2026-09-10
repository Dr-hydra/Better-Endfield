#!/usr/bin/env python3
"""Convert the validated Endmin Casualwear EFMI sample into BEM PoC-2 data.

PoC-2 preserves the vertex streams that matter when the EFMI GPU buffers are
reconstructed as a UnityEngine.Mesh:
- POSITION0 -> Vector3
- TEXCOORD0 -> Vector2
- ENCODEDDATA0 -> decoded normal
- COLOR0 (R8G8B8A8_SNORM) -> float4
- BLENDWEIGHTS0 / BLENDINDICES0 -> Unity BoneWeight
- IB -> uint16 indices

This remains intentionally narrow and targets the validated Endministrator (F)
"Endmin Casualwear" sample only.
"""

from __future__ import annotations

import argparse
import json
import math
from pathlib import Path
import re
import struct
import sys
import zipfile

MAGIC = b"BEMPOC2\0"
VERSION = 2
COMPONENT_ID = 9
ORIGINAL_INDEX_COUNT = 101_994
EXPECTED_VERTEX_COUNT = 12_873
EXPECTED_INDEX_COUNT = 59_073
EXPECTED_MAX_BONE = 155

FLAG_UV0 = 1 << 0
FLAG_SKIN4 = 1 << 1
FLAG_INDEX16 = 1 << 2
FLAG_NORMAL = 1 << 3
FLAG_COLOR0 = 1 << 4

HEADER = struct.Struct("<8s7I")


class Source:
    def __init__(self, path: Path):
        self.path = path
        self._zip = zipfile.ZipFile(path) if path.is_file() and path.suffix.lower() == ".zip" else None
        if self._zip:
            self.names = self._zip.namelist()
        elif path.is_dir():
            self.names = [
                str(p.relative_to(path)).replace("\\", "/")
                for p in path.rglob("*")
                if p.is_file()
            ]
        else:
            raise ValueError(f"source must be an EFMI mod folder or .zip: {path}")

    def close(self):
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


def parse_mod_ini(text: str) -> dict[str, int | str]:
    if "EFMI ALPHA-12 INI" not in text:
        raise ValueError("sample is not EFMI ALPHA-12")
    values: dict[str, int | str] = {}
    for key in ("required_efmi_version", "object_guid", "mesh_vertex_count", "component_count"):
        match = re.search(rf"(?mi)^\s*\$?{re.escape(key)}\s*=\s*([^\r\n;]+)", text)
        if match:
            raw = match.group(1).strip()
            try:
                values[key] = int(raw, 0)
            except ValueError:
                values[key] = raw
    return values


def validate_components_json(raw: bytes) -> None:
    meta = json.loads(raw.decode("utf-8-sig"))
    for name, expected in (
        ("Component9_VB0", ("POSITION", "ENCODEDDATA")),
        ("Component9_VB1", ("TEXCOORD", "COLOR")),
        ("Component9_VB2", ("BLENDWEIGHTS", "BLENDINDICES")),
        ("Component9_IB", ("INDEX",)),
    ):
        if name not in meta:
            raise ValueError(f"Components.buf missing {name}")
        blob = json.dumps(meta[name], ensure_ascii=False)
        for semantic in expected:
            if semantic not in blob:
                raise ValueError(f"{name} missing semantic {semantic}")


def sign_extend_10(value: int) -> int:
    value &= 0x3FF
    return value - 1024 if value >= 512 else value


def decode_packed_normal(encoded: int) -> tuple[float, float, float]:
    # Mirrors EFMI Tools DataModelEFMI.decode_tbn_data_10_10_10_2:
    # R10G10 are signed octahedral normal coordinates; bit30 marks packed data.
    if ((encoded >> 30) & 1) != 1:
        raise ValueError("ENCODEDDATA packed flag (bit30) is not set")

    x = max(-1.0, sign_extend_10(encoded) / 511.0)
    y = max(-1.0, sign_extend_10(encoded >> 10) / 511.0)
    z = 1.0 - abs(x) - abs(y)

    if z < 0.0:
        old_x = x
        sx = -1.0 if old_x < 0.0 else (1.0 if old_x > 0.0 else 0.0)
        sy = -1.0 if y < 0.0 else (1.0 if y > 0.0 else 0.0)
        x = (1.0 - abs(y)) * sx
        y = (1.0 - abs(old_x)) * sy

    length = math.sqrt(x * x + y * y + z * z)
    if length < 1e-8:
        return 0.0, 0.0, 1.0
    return x / length, y / length, z / length


def decode_snorm8(value: int) -> float:
    if value >= 128:
        value -= 256
    return max(-1.0, value / 127.0)


def decode_component9(src: Source):
    vb0 = src.read("Meshes/Component9_VB0.buf")
    vb1 = src.read("Meshes/Component9_VB1.buf")
    vb2 = src.read("Meshes/Component9_VB2.buf")
    ib = src.read("Meshes/Component9_IB.buf")

    if len(vb0) % 16:
        raise ValueError("Component9 VB0 is not 16-byte stride")
    vertex_count = len(vb0) // 16
    if vertex_count != EXPECTED_VERTEX_COUNT:
        raise ValueError(
            f"Component9 vertex count changed: {vertex_count} != {EXPECTED_VERTEX_COUNT}"
        )
    if len(vb1) != vertex_count * 12:
        raise ValueError(
            "Component9 VB1 is not expected TEXCOORD(float2)+COLOR(snorm8x4) layout"
        )
    if len(vb2) != vertex_count * 12:
        raise ValueError(
            "Component9 VB2 is not expected weights(u16x4)+indices(u8x4) layout"
        )
    if len(ib) % 2:
        raise ValueError("Component9 IB is not uint16")
    index_count = len(ib) // 2
    if index_count != EXPECTED_INDEX_COUNT:
        raise ValueError(
            f"Component9 custom index count changed: {index_count} != {EXPECTED_INDEX_COUNT}"
        )

    positions = bytearray(vertex_count * 12)
    uvs = bytearray(vertex_count * 8)
    normals = bytearray(vertex_count * 12)
    colors = bytearray(vertex_count * 16)
    weights = bytearray(vertex_count * 16)
    bone_indices = bytearray(vertex_count * 4)

    max_bone = 0
    max_weight_error = 0.0
    color_alpha_zero = 0
    color_alpha_one = 0

    for i in range(vertex_count):
        v0 = i * 16
        positions[i * 12:(i + 1) * 12] = vb0[v0:v0 + 12]

        encoded = struct.unpack_from("<I", vb0, v0 + 12)[0]
        nx, ny, nz = decode_packed_normal(encoded)
        struct.pack_into("<3f", normals, i * 12, nx, ny, nz)

        v1 = i * 12
        uvs[i * 8:(i + 1) * 8] = vb1[v1:v1 + 8]
        raw_color = struct.unpack_from("<4B", vb1, v1 + 8)
        decoded_color = tuple(decode_snorm8(x) for x in raw_color)
        struct.pack_into("<4f", colors, i * 16, *decoded_color)
        if raw_color[3] == 0:
            color_alpha_zero += 1
        elif raw_color[3] == 127:
            color_alpha_one += 1

        v2 = i * 12
        packed_weights = struct.unpack_from("<4H", vb2, v2)
        packed_bones = struct.unpack_from("<4B", vb2, v2 + 8)
        decoded_weights = [x / 65535.0 for x in packed_weights]
        total = sum(decoded_weights)
        max_weight_error = max(max_weight_error, abs(total - 1.0))
        if total > 0:
            decoded_weights = [x / total for x in decoded_weights]
        struct.pack_into("<4f", weights, i * 16, *decoded_weights)
        bone_indices[i * 4:(i + 1) * 4] = bytes(packed_bones)
        max_bone = max(max_bone, *packed_bones)

    if max_bone != EXPECTED_MAX_BONE:
        raise ValueError(
            f"Component9 max bone changed: {max_bone} != {EXPECTED_MAX_BONE}"
        )
    if max_weight_error > 0.001:
        raise ValueError(f"unexpected weight normalization error: {max_weight_error}")

    unpacked_indices = struct.unpack("<" + "H" * index_count, ib)
    max_index = max(unpacked_indices)
    if max_index >= vertex_count:
        raise ValueError(
            f"IB references vertex {max_index}, vertexCount={vertex_count}"
        )

    return (
        bytes(positions),
        bytes(uvs),
        bytes(normals),
        bytes(colors),
        bytes(weights),
        bytes(bone_indices),
        ib,
        vertex_count,
        index_count,
        max_bone,
        color_alpha_zero,
        color_alpha_one,
    )


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("source", type=Path, help="EFMI mod folder or zip")
    parser.add_argument(
        "-o",
        "--output",
        type=Path,
        required=True,
        help=(
            "output .bempoc file, usually "
            "<catalog>/custom-model/endmin-casualwear-c9.bempoc"
        ),
    )
    args = parser.parse_args()

    src = Source(args.source)
    try:
        ini = parse_mod_ini(src.read("mod.ini").decode("utf-8-sig", errors="strict"))
        validate_components_json(src.read("Meshes/Components.buf"))
        if ini.get("component_count") not in (None, 11):
            raise ValueError(f"unexpected component_count={ini.get('component_count')}")
        data = decode_component9(src)
    finally:
        src.close()

    (
        positions,
        uvs,
        normals,
        colors,
        weights,
        bones,
        indices,
        vertex_count,
        index_count,
        max_bone,
        alpha_zero,
        alpha_one,
    ) = data

    flags = FLAG_UV0 | FLAG_SKIN4 | FLAG_INDEX16 | FLAG_NORMAL | FLAG_COLOR0
    header = HEADER.pack(
        MAGIC,
        VERSION,
        COMPONENT_ID,
        ORIGINAL_INDEX_COUNT,
        vertex_count,
        index_count,
        max_bone,
        flags,
    )

    args.output.parent.mkdir(parents=True, exist_ok=True)
    with args.output.open("wb") as output:
        output.write(header)
        output.write(positions)
        output.write(uvs)
        output.write(normals)
        output.write(colors)
        output.write(weights)
        output.write(bones)
        output.write(indices)

    print(f"wrote {args.output}")
    print(
        f"component={COMPONENT_ID} vertices={vertex_count} "
        f"indices={index_count} maxBone={max_bone}"
    )
    print(
        f"streams=POSITION+UV0+NORMAL(decoded ENCODEDDATA)+COLOR0+SKIN4 "
        f"colorAlpha0={alpha_zero} colorAlpha1={alpha_one}"
    )
    print(f"size={args.output.stat().st_size} bytes")
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Exception as exc:
        print(f"error: {exc}", file=sys.stderr)
        raise SystemExit(2)
