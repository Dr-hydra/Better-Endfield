#!/usr/bin/env python3
"""Convert an Endfield EFMI character mod into a BEM PoC-2.4 payload.

Nothing here decodes, normalizes or repacks geometry. The Endfield character
vertex layout is reproduced at runtime through the engine's own
SetVertexBufferParams / SetVertexBufferData bindings, and the runtime takes the
declaration from the live source mesh rather than from this file, so the three
EFMI vertex streams and the index buffer are carried through byte for byte.

What this converter must get right is therefore only the framing:

  * which live renderer each component belongs to. The join key is the original
    draw's index count, recorded in the ini as
    [TextureOverride_EntryPoint_ComponentN] match_index_count. It is checked
    in-client against the renderer's summed submesh index count.
  * the per-stream strides, which the runtime requires to equal the live mesh's
    own strides before it writes anything. The sample's ten components use six
    different declarations (16/12/12, 16/12/4, 16/8/12, 16/8/4, ...), so these
    are read from the mod's Components.buf per component rather than assumed.
  * the textures, and which slot each belongs to.
  * which components the mod hides by default.

Texture slots. EFMI replaces textures by content hash at the graphics API
layer, so its ini records no material slot. Two independent facts are recovered
from the filenames EFMI Tools writes, e.g.
"Components-9-10 t=b2b16f01 BC5-Linear.dds":

  * the component list, which says which draw calls sampled the texture.
    This is the actual slot key: the runtime can compute the same set from
    the live materials, so a texture EFMI saw in exactly draws {1,4} must be
    the one texture both the face and the eyebrow material sample.
  * the format tag, which describes the ORIGINAL texture the hash identifies,
    not the replacement DDS beside it. For b2b16f01 the tag says BC5-Linear
    while the DDS header says BC7_UNORM: the game's normal-map slot is BC5 and
    the mod ships a re-encoded BC7 replacement for it. Every other BC5-tagged
    file in the sample does carry a real BC5U header, so this is not an export
    bug, it is the tag and the payload describing different objects.

So the tag says which format the slot holds today and the DDS header says how
to rebuild the replacement; both are carried. Where the component set plus the
format still leaves several candidates -- the sample's hair has five textures
EFMI saw only in draw 0 -- the runtime refuses the slot and reports the
candidates rather than guessing. A `bem-slots.json`, either inside the source
or beside it as `<source>.bem-slots.json`, pins those cases explicitly:

    {"151c9982": "T_actor_common_hairline_03_M"}

A pin value is either the original texture's object name, as above, or a
material property id. The name is the form to prefer: it says what it means,
it survives a shader revision that renumbers its properties, and the runtime
compares it against the live slot's own texture name. The converter stores it
as the CRC-32 of that name and marks the entry, so both forms travel in the
same field.

Components the mod does not replace are still carried, without geometry or
textures, because the runtime needs their original index counts to work out
which live renderer belongs to which component before it can compute those
sets.

Default visibility. EFMI expresses toggles as 3DMigoto state: a persistent
$swapvar with a declared default, and a [CommandListProcessToggles] block that
derives one $draw_component_* flag per draw object from it. This converter
evaluates that block at the declared defaults; a component whose every draw
object is off is marked hidden, which the runtime applies by disabling the
renderer. In the Endmin Casualwear sample $swapvar_mask defaults to -1 and both
mask draws test for 0, so the mask starts hidden -- the mod's "no mask" look is
its default state, not an extra feature.
"""

from __future__ import annotations

import argparse
import json
from pathlib import Path
import re
import struct
import sys
import zipfile
import zlib

MAGIC = b"BEMPC24\0"
VERSION = 24

FILE_HEADER = struct.Struct("<8s5I")
COMPONENT_HEADER = struct.Struct("<13I")
TEXTURE_ENTRY = struct.Struct("<4I4i2I2I")

COMPONENT_FLAG_HIDDEN = 1 << 0
COMPONENT_FLAG_NO_GEOMETRY = 1 << 1

MAX_STREAMS = 3

# UnityEngine.TextureFormat values. Only block formats this converter has
# actually seen in an Endfield character mod are listed; anything else is
# rejected rather than guessed at, because the runtime rebuilds the surface
# from these bytes with no reinterpretation.
UNITY_BC7 = 25
UNITY_BC4 = 26
UNITY_BC5 = 27
UNITY_DXT1 = 10
UNITY_DXT5 = 12

# (Unity TextureFormat, sRGB, bytes per 4x4 block)
DXGI_FORMATS = {
    71: (UNITY_DXT1, False, 8),   # BC1_UNORM
    72: (UNITY_DXT1, True, 8),    # BC1_UNORM_SRGB
    77: (UNITY_DXT5, False, 16),  # BC3_UNORM
    78: (UNITY_DXT5, True, 16),   # BC3_UNORM_SRGB
    80: (UNITY_BC4, False, 8),    # BC4_UNORM
    83: (UNITY_BC5, False, 16),   # BC5_UNORM
    98: (UNITY_BC7, False, 16),   # BC7_UNORM
    99: (UNITY_BC7, True, 16),    # BC7_UNORM_SRGB
}
FOURCC_FORMATS = {
    b"DXT1": (UNITY_DXT1, False, 8),
    b"DXT5": (UNITY_DXT5, False, 16),
    b"ATI1": (UNITY_BC4, False, 8),
    b"BC4U": (UNITY_BC4, False, 8),
    b"ATI2": (UNITY_BC5, False, 16),
    b"BC5U": (UNITY_BC5, False, 16),
}

# EFMI Tools' filename format tag, which describes the original texture at that
# content hash rather than the replacement DDS.
TAG_FORMATS = {
    "BC7-sRGB": (UNITY_BC7, True),
    "BC7-Linear": (UNITY_BC7, False),
    "BC5-Linear": (UNITY_BC5, False),
    "BC4-Linear": (UNITY_BC4, False),
    "BC3-sRGB": (UNITY_DXT5, True),
    "BC3-Linear": (UNITY_DXT5, False),
    "BC1-sRGB": (UNITY_DXT1, True),
    "BC1-Linear": (UNITY_DXT1, False),
}


class Source:
    def __init__(self, path: Path):
        self.path = path
        self._zip = (
            zipfile.ZipFile(path)
            if path.is_file() and path.suffix.lower() == ".zip"
            else None
        )
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

    def close(self) -> None:
        if self._zip:
            self._zip.close()

    def find(self, suffix: str) -> str | None:
        matches = [
            n for n in self.names if n.replace("\\", "/").endswith(suffix)
        ]
        if len(matches) > 1:
            raise ValueError(f"ambiguous '*{suffix}': {len(matches)} matches")
        return matches[0] if matches else None

    def read(self, suffix: str) -> bytes:
        name = self.find(suffix)
        if name is None:
            raise ValueError(f"missing '*{suffix}'")
        return self.read_exact(name)

    def read_exact(self, name: str) -> bytes:
        if self._zip:
            return self._zip.read(name)
        return (self.path / Path(name)).read_bytes()


def parse_ini_constants(text: str) -> dict[str, int]:
    values: dict[str, int] = {}
    for match in re.finditer(
        r"^\s*global\s+(?:persist\s+)?\$(\w+)\s*=\s*(-?\d+)\s*$", text, re.M
    ):
        values[match.group(1)] = int(match.group(2))
    return values


def parse_original_index_counts(text: str) -> dict[int, int]:
    """The lod0 draw's original index count per component, the runtime join key.

    The _LOD1 sections describe the reduced meshes, which this converter does
    not carry, so they are deliberately ignored.
    """
    counts: dict[int, int] = {}
    for match in re.finditer(
        r"^\[TextureOverride_EntryPoint_Component(\d+)\]\s*\n"
        r"((?:(?!\[).*\n?)*)",
        text,
        re.M,
    ):
        body = match.group(2)
        found = re.search(r"^\s*match_index_count\s*=\s*(\d+)", body, re.M)
        if found:
            counts[int(match.group(1))] = int(found.group(1))
    return counts


def parse_default_hidden(text: str, constants: dict[str, int]) -> dict[int, bool]:
    """Evaluates [CommandListProcessToggles] at the declared swapvar defaults.

    A component is hidden only when every draw object it owns is off, because
    the runtime can only hide a whole renderer. Any expression this does not
    understand leaves the draw visible and is reported.
    """
    block = re.search(
        r"^\[CommandListProcessToggles\]\s*\n((?:(?!\[).*\n?)*)", text, re.M
    )
    states: dict[str, bool] = {
        name: value != 0
        for name, value in constants.items()
        if name.startswith("draw_component_")
    }
    unparsed: list[str] = []
    if block:
        for line in block.group(1).splitlines():
            line = line.split(";", 1)[0].strip()
            if not line.startswith("$draw_component_"):
                continue
            assign = re.match(r"^\$(\w+)\s*=\s*(.+)$", line)
            if not assign:
                unparsed.append(line)
                continue
            target, expr = assign.group(1), assign.group(2).strip()
            compare = re.match(
                r"^\(?\s*\$(\w+)\s*(==|!=)\s*(-?\d+)\s*\)?$", expr
            )
            if compare:
                actual = constants.get(compare.group(1))
                if actual is None:
                    unparsed.append(line)
                    continue
                wanted = int(compare.group(3))
                states[target] = (
                    actual == wanted
                    if compare.group(2) == "=="
                    else actual != wanted
                )
                continue
            literal = re.match(r"^(-?\d+)$", expr)
            if literal:
                states[target] = int(literal.group(1)) != 0
                continue
            unparsed.append(line)

    for line in unparsed:
        print(
            f"warning: could not evaluate toggle, leaving it visible: {line}",
            file=sys.stderr,
        )

    hidden: dict[int, bool] = {}
    for name, visible in states.items():
        match = re.match(r"^draw_component_(\d+)_", name)
        if not match:
            continue
        component = int(match.group(1))
        hidden[component] = hidden.get(component, True) and not visible
    return hidden


class Texture:
    __slots__ = (
        "name", "width", "height", "mip_count", "data",
        "create_format", "create_srgb", "slot_format", "slot_srgb",
        "component_mask", "explicit_slot", "pin_kind",
    )


def mip_chain_size(width: int, height: int, mips: int, block_bytes: int) -> int:
    total = 0
    for level in range(mips):
        level_width = max(width >> level, 1)
        level_height = max(height >> level, 1)
        blocks_x = (level_width + 3) // 4
        blocks_y = (level_height + 3) // 4
        total += blocks_x * blocks_y * block_bytes
    return total


def parse_dds(name: str, raw: bytes) -> tuple[int, int, int, int, bool, bytes]:
    """Reads a block-compressed DDS and returns its surface bytes verbatim."""
    if raw[:4] != b"DDS " or len(raw) < 128:
        raise ValueError(f"{name}: not a DDS file")
    header_size, _, height, width, _, _, mips = struct.unpack_from("<7I", raw, 4)
    if header_size != 124:
        raise ValueError(f"{name}: unexpected DDS header size {header_size}")
    fourcc = raw[84:88]

    if fourcc == b"DX10":
        if len(raw) < 148:
            raise ValueError(f"{name}: truncated DX10 header")
        dxgi = struct.unpack_from("<I", raw, 128)[0]
        if dxgi not in DXGI_FORMATS:
            raise ValueError(f"{name}: unsupported DXGI format {dxgi}")
        texture_format, srgb, block_bytes = DXGI_FORMATS[dxgi]
        payload_offset = 148
    elif fourcc in FOURCC_FORMATS:
        texture_format, srgb, block_bytes = FOURCC_FORMATS[fourcc]
        payload_offset = 128
    else:
        raise ValueError(
            f"{name}: unsupported DDS pixel format {fourcc!r}; the runtime "
            f"uploads these bytes without reinterpreting them"
        )

    mips = max(mips, 1)
    if width % 4 or height % 4:
        raise ValueError(
            f"{name}: {width}x{height} is not a whole number of 4x4 blocks"
        )

    expected = mip_chain_size(width, height, mips, block_bytes)
    data = raw[payload_offset:]
    if len(data) != expected:
        raise ValueError(
            f"{name}: surface is {len(data)} bytes, expected {expected} for "
            f"{width}x{height} with {mips} mip level(s)"
        )
    return width, height, mips, texture_format, srgb, data


TEXTURE_NAME = re.compile(
    r"(?:^|/)Components-([\d-]+) t=([0-9a-fA-F]+)(?: ([\w-]+))?\.dds$"
)


PIN_KIND_ID = 1
PIN_KIND_NAME = 2


def parse_slot_pins(raw: dict) -> dict[str, tuple[int, int]]:
    """Reads bem-slots.json into texture hash -> (pin value, pin kind)."""

    def as_int(text: str) -> int | None:
        try:
            return int(text, 0)
        except ValueError:
            return None

    pins: dict[str, tuple[int, int]] = {}
    for key, value in raw.items():
        name = str(key).lower()
        if name.startswith("t="):
            name = name[2:]
        if isinstance(value, int):
            pins[name] = (value, PIN_KIND_ID)
            continue
        if not isinstance(value, str) or not value:
            raise ValueError(f"{key}: a slot pin is a property id or a name")
        parsed = as_int(value)
        if parsed is not None:
            pins[name] = (parsed, PIN_KIND_ID)
            continue
        digest = zlib.crc32(value.encode("utf-8")) & 0xFFFFFFFF
        if digest == 0:
            raise ValueError(
                f"{key}: {value!r} hashes to zero, which reads as no pin"
            )
        pins[name] = (digest, PIN_KIND_NAME)
    return pins


def collect_textures(
    src: Source, overrides: dict[str, tuple[int, int]]
) -> list[Texture]:
    """Describes every DDS the mod ships, once, with its component set.

    The component list in the filename is carried through as a bitmask because
    it, not the format, is what identifies the slot: EFMI recorded which draws
    sampled the original texture, and the runtime can compute the same set from
    the live materials. A texture bound by exactly components {1,4} has to be
    the one texture both the face and the eyebrow material sample.

    The table is global rather than per component for the same reason: a texture
    belongs to a set of draws, and the sample shares one across nine of them.
    """
    selected: list[Texture] = []
    for entry in sorted(src.names):
        match = TEXTURE_NAME.search(entry.replace("\\", "/"))
        if not match:
            continue
        components = [int(part) for part in match.group(1).split("-") if part]

        name = match.group(2)
        width, height, mips, create_format, create_srgb, data = parse_dds(
            name, src.read_exact(entry)
        )

        tag = match.group(3)
        if tag is None:
            slot_format, slot_srgb = create_format, create_srgb
        elif tag in TAG_FORMATS:
            slot_format, slot_srgb = TAG_FORMATS[tag]
        else:
            raise ValueError(f"{name}: unrecognized EFMI format tag {tag!r}")

        texture = Texture()
        texture.name = name
        texture.width = width
        texture.height = height
        texture.mip_count = mips
        texture.data = data
        texture.create_format = create_format
        texture.create_srgb = create_srgb
        texture.slot_format = slot_format
        texture.slot_srgb = slot_srgb
        texture.component_mask = sum(1 << c for c in components)
        pin = overrides.get(name)
        texture.explicit_slot, texture.pin_kind = pin if pin else (0, 0)
        selected.append(texture)

    selected.sort(key=lambda t: (bin(t.component_mask).count("1"), t.name))
    return selected


def component_list(mask: int) -> list[int]:
    return [i for i in range(32) if mask >> i & 1]


class Component:
    __slots__ = (
        "index", "original_index_count", "vertex_count", "index_count",
        "max_bone", "strides", "streams", "indices", "textures", "hidden",
        "semantics", "has_geometry",
    )

    def __init__(self):
        self.vertex_count = 0
        self.index_count = 0
        self.max_bone = 0
        self.strides = [0, 0, 0]
        self.streams = []
        self.indices = b''
        self.textures = []
        self.hidden = False
        self.semantics = []
        self.has_geometry = False


def stream_declaration(meta: dict, key: str):
    entry = meta.get(key)
    if not entry:
        return None, 0
    semantics = [
        (f"{s['name']}{s['index']}", s["format"], s["stride"])
        for s in entry["semantics"]
    ]
    return semantics, sum(s[2] for s in semantics)


def read_component(
    src: Source,
    meta: dict,
    index: int,
    original_index_count: int,
    hidden: bool,
) -> Component:
    label = f"Component{index}"
    strides: list[int] = []
    streams: list[bytes] = []
    semantics: list[str] = []
    vertex_count = None

    for stream in range(MAX_STREAMS):
        key = f"{label}_VB{stream}"
        decl, stride = stream_declaration(meta, key)
        if decl is None or stride == 0:
            raise ValueError(f"{label}: stream {stream} has no declaration")
        data = src.read(f"Meshes/{key}.buf")
        if len(data) % stride:
            raise ValueError(
                f"{key}: {len(data)} bytes is not a multiple of stride {stride}"
            )
        count = len(data) // stride
        if vertex_count is None:
            vertex_count = count
        elif count != vertex_count:
            raise ValueError(
                f"{key}: {count} vertices disagrees with {vertex_count}"
            )
        strides.append(stride)
        streams.append(data)
        semantics.append(
            " + ".join(f"{n}:{f}" for n, f, _ in decl) + f" [{stride}]"
        )

    indices = src.read(f"Meshes/{label}_IB.buf")
    if len(indices) % 2:
        raise ValueError(f"{label}: index buffer is not uint16")
    index_count = len(indices) // 2
    if index_count % 3:
        raise ValueError(f"{label}: {index_count} indices is not a triangle list")
    unpacked = struct.unpack(f"<{index_count}H", indices)
    if max(unpacked) >= vertex_count:
        raise ValueError(
            f"{label}: index {max(unpacked)} exceeds vertexCount {vertex_count}"
        )

    # Bone indices live at the end of stream 2, after the weights when present.
    decl2, _ = stream_declaration(meta, f"{label}_VB2")
    offset = 0
    found = False
    for name, _, stride in decl2:
        if name.startswith("BLENDINDICES"):
            found = True
            break
        offset += stride
    if not found:
        raise ValueError(f"{label}: stream 2 declares no BLENDINDICES")
    max_bone = 0
    for i in range(vertex_count):
        max_bone = max(
            max_bone,
            *struct.unpack_from("<4B", streams[2], i * strides[2] + offset),
        )

    component = Component()
    component.has_geometry = True
    component.index = index
    component.original_index_count = original_index_count
    component.vertex_count = vertex_count
    component.index_count = index_count
    component.max_bone = max_bone
    component.strides = strides
    component.streams = streams
    component.indices = indices
    component.hidden = hidden
    component.semantics = semantics
    return component


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
        ini = src.read("mod.ini").decode("utf-8-sig", errors="strict")
        if "EFMI ALPHA" not in ini:
            raise ValueError("sample is not an EFMI ALPHA export")
        meta = json.loads(
            src.read("Meshes/Components.buf").decode("utf-8-sig")
        )
        constants = parse_ini_constants(ini)
        original_counts = parse_original_index_counts(ini)
        hidden_by_default = parse_default_hidden(ini, constants)

        override_name = src.find("bem-slots.json")
        pins_raw: dict | None = None
        if override_name is not None:
            pins_raw = json.loads(
                src.read_exact(override_name).decode("utf-8-sig")
            )
        else:
            # The pins belong to the mod they describe, so they are also
            # looked for beside it: overwriting the download to add them is
            # not something a user should have to do.
            sidecar = src.path.with_suffix(src.path.suffix + ".bem-slots.json")
            if sidecar.is_file():
                pins_raw = json.loads(sidecar.read_text(encoding="utf-8-sig"))
        overrides = parse_slot_pins(pins_raw) if pins_raw else {}

        components: list[Component] = []
        for index in sorted(original_counts):
            if src.find(f"Meshes/Component{index}_VB0.buf") is None:
                # Carried without geometry: the game keeps its own mesh, but
                # the runtime still needs this component's original index
                # count to identify its renderer when it computes the
                # component sets the texture slots are matched on.
                placeholder = Component()
                placeholder.index = index
                placeholder.original_index_count = original_counts[index]
                components.append(placeholder)
                continue
            components.append(
                read_component(
                    src,
                    meta,
                    index,
                    original_counts[index],
                    hidden_by_default.get(index, False),
                )
            )
        textures = collect_textures(src, overrides)
    finally:
        src.close()
    if not components:
        raise ValueError("the source carries no replaceable component")

    args.output.parent.mkdir(parents=True, exist_ok=True)
    with args.output.open("wb") as output:
        output.write(
            FILE_HEADER.pack(
                MAGIC, VERSION, len(components), len(textures), 0, 0
            )
        )
        for component in components:
            output.write(
                COMPONENT_HEADER.pack(
                    component.index,
                    component.original_index_count,
                    component.vertex_count,
                    component.index_count,
                    component.max_bone,
                    MAX_STREAMS,
                    component.strides[0],
                    component.strides[1],
                    component.strides[2],
                    2,
                    0,
                    (COMPONENT_FLAG_HIDDEN if component.hidden else 0)
                    | (
                        0 if component.has_geometry
                        else COMPONENT_FLAG_NO_GEOMETRY
                    ),
                    0,
                )
            )
            for data in component.streams:
                output.write(data)
            output.write(component.indices)
        for texture in textures:
            name = texture.name.encode("ascii")
            output.write(
                TEXTURE_ENTRY.pack(
                    texture.width,
                    texture.height,
                    texture.mip_count,
                    len(texture.data),
                    texture.create_format,
                    1 if texture.create_srgb else 0,
                    texture.slot_format,
                    1 if texture.slot_srgb else 0,
                    texture.component_mask,
                    texture.explicit_slot,
                    len(name),
                    texture.pin_kind,
                )
            )
            output.write(name)
            output.write(texture.data)

    print(f"wrote {args.output}")
    print(f"components={len(components)}")
    for component in components:
        if not component.has_geometry:
            print(
                f"  C{component.index}: origIdx="
                f"{component.original_index_count} (not replaced; carried "
                f"for component-set identification only)"
            )
            continue
        flags = " HIDDEN_BY_DEFAULT" if component.hidden else ""
        print(
            f"  C{component.index}: origIdx={component.original_index_count} "
            f"vtx={component.vertex_count} idx={component.index_count} "
            f"tris={component.index_count // 3} maxBone={component.max_bone} "
            f"strides={'/'.join(str(s) for s in component.strides)} "
            f"{flags}"
        )
        for stream, text in enumerate(component.semantics):
            print(f"      VB{stream} {text}")
    print(f"textures={len(textures)}")
    for texture in textures:
        print(
            f"  t={texture.name} {texture.width}x{texture.height} "
            f"mips={texture.mip_count} "
            f"replacement=fmt{texture.create_format}/"
            f"srgb{int(texture.create_srgb)} "
            f"slot=fmt{texture.slot_format}/srgb{int(texture.slot_srgb)} "
            f"components={component_list(texture.component_mask)}"
            + (
                f" PINNED=0x{texture.explicit_slot:08X} "
                + ("(name)" if texture.pin_kind == PIN_KIND_NAME else "(id)")
                if texture.explicit_slot
                else ""
            )
        )
    print(f"size={args.output.stat().st_size} bytes")
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Exception as exc:
        print(f"error: {exc}", file=sys.stderr)
        raise SystemExit(2)
