"""Evidence-driven repair helpers for BEM source conversion.

Contract
--------
* **Inputs are never modified.** Every command that writes requires an explicit
  ``-o``; there is no "overwrite in place" mode. Commands that only measure
  write nothing at all.
* **Every decision carries its numbers.** Output is a JSON object with the
  rule that was applied, the values before and after, and an ``evidence``
  level: ``exact`` (measured), ``computed`` (derived from measured values),
  ``inferred`` (a judgement the caller must confirm).
* **A refusal is a result.** When the data does not support a repair the
  command exits non-zero and names what is missing. It never reports success
  for work it did not do.
* **No format guessing.** Unknown DDS layouts, non-2D textures and unaligned
  index buffers are reported, not coerced.

Commands
--------
``ib-audit``       measure a draw against its index buffer, change nothing
``ib-clamp``       emit a corrected draw count (and index_count hint)
``dds-audit``      measure a DDS: format, mip chain, slice count, size limit
``dds-mipdrop``    drop the top mip of every slice into a new file
``donor-find``     list same-name donors, grouped by size, conflicts included
``donor-copy``     copy one donor only when size or sha256 is supplied
``override-set``   write BEM 1.5 material overrides with full validation
``root-offset``    write a BEM 1.5 root offset within the 0.2 m limit
``ini-audit``      classify duplicate sections by the loader's own rule
``ini-normalize``  write a duplicate-free copy with an unchanged projection
``texture-normalize`` resolve conditional texture overrides to one selection
``skin-audit``     measure palette, weight sums and out-of-palette indices
``skin-fix``       zero the out-of-palette indices that carry no weight
``vertex-compact`` drop unreferenced vertices, verified row by row
``entry-normalize`` lower old entry programs; refuse when the template is stale
``skin-convert``   rewrite a merged-skeleton skin stream to a mesh-local palette
``texture-audit``  classify texture overrides; find duplicate hashes and slots
``catalog-gap``    diff a package's entry hashes against the character catalogs
``deformation-audit`` validate body parameters and decode every delta payload
``option-audit``   enumerate option states; prove rules and the 256-draw ceiling
``ib-groups``      group meshes by index bytes; find shared topology and drift
``shape-params``   write a BEM 1.3 slider spec from a source mod's ShapeKey data

Standard library only, except ``entry-normalize``, which imports
``component_auto`` to re-check its own output against the real gate and refuses
with ``ENTRY_TEMPLATE_UNAVAILABLE`` when that module is not reachable, and
``shape-params``/``catalog-gap``, which need the CLI modules for the same
reason.
"""
from __future__ import annotations

import argparse
import hashlib
import json
import math
import os
import re
import shutil
import struct
import sys
import zipfile
from pathlib import Path
from typing import Any, Callable, Dict, Iterable, List, Optional, Tuple

MIB = 1024 * 1024
DEFAULT_TEXTURE_LIMIT = 256 * MIB


# ---------------------------------------------------------------------------
# Result plumbing
# ---------------------------------------------------------------------------

class Refused(Exception):
    """A repair the data does not support. Carries a stable code."""

    def __init__(self, code: str, message: str, **detail: Any):
        super().__init__(message)
        self.code = code
        self.message = message
        self.detail = detail

    def as_dict(self) -> Dict[str, Any]:
        return dict(ok=False, refused=self.code, message=self.message, **self.detail)


def _require(condition: bool, code: str, message: str, **detail: Any) -> None:
    if not condition:
        raise Refused(code, message, **detail)


def _sha256(path: Path) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        for block in iter(lambda: handle.read(4 * MIB), b""):
            digest.update(block)
    return digest.hexdigest()


# ---------------------------------------------------------------------------
# 1. Index buffers
# ---------------------------------------------------------------------------

def audit_ib(ib_path: Path | str, first: int, count: int, stride: int = 2) -> Dict[str, Any]:
    """Measure one draw against one index buffer. Writes nothing.

    The four fixability criteria from the playbook are *not* applied here:
    this reports capacity and alignment so the caller can apply them. What it
    does decide is whether the numbers even allow a clamp.
    """
    ib_path = Path(ib_path)
    _require(ib_path.is_file(), "IB_NOT_FOUND", f"index buffer not found: {ib_path}")
    _require(stride in (2, 4), "IB_BAD_STRIDE", f"stride must be 2 or 4, got {stride}")
    _require(first >= 0 and count > 0, "IB_BAD_RANGE", "first must be >= 0 and count > 0")

    size = ib_path.stat().st_size
    total = size // stride
    available = total - first

    issues: List[str] = []
    if size % stride:
        issues.append("TRAILING_PARTIAL_INDEX")
    if first % 3:
        issues.append("FIRST_NOT_TRIANGLE_ALIGNED")
    if count % 3:
        issues.append("COUNT_NOT_TRIANGLE_ALIGNED")
    if first >= total:
        issues.append("FIRST_OUT_OF_RANGE")
    elif count > available:
        issues.append("OVERRUN")

    overrun = count > available
    clamped = None
    fixable = False
    if overrun and available >= 3 and available % 3 == 0 and first % 3 == 0:
        clamped = available
        fixable = True

    return dict(
        ok=True,
        file=str(ib_path),
        stride=stride,
        bytes=size,
        total_indices=total,
        first=first,
        count=count,
        available_from_first=max(0, available),
        short_by=max(0, count - available),
        overrun=overrun,
        fixable=fixable,
        clamped_count=clamped,
        dropped_triangles=(count - clamped) // 3 if clamped is not None else None,
        issues=issues,
        evidence="exact",
        note=("fixable only means the arithmetic closes on complete triangles; the four "
              "playbook criteria (gap at the tail, residual complete, closed topology, "
              "explained cause) still decide whether to accept it"),
    )


def clamp_ib(
    ib_path: Path | str,
    first: int,
    count: int,
    stride: int = 2,
    accept_truncation: bool = False,
) -> Dict[str, Any]:
    """Return a corrected draw count. The buffer itself is never written.

    Truncating a draw silently drops geometry, so it requires an explicit
    ``accept_truncation`` from the caller, who owns the four-criteria decision.
    """
    report = audit_ib(ib_path, first, count, stride)
    if not report["overrun"]:
        return dict(report, changed=False, clamped_count=count)
    if not report["fixable"]:
        raise Refused(
            "IB_NOT_FIXABLE",
            "the requested range cannot be closed on complete triangles",
            report=report,
        )
    if not accept_truncation:
        raise Refused(
            "IB_TRUNCATION_NOT_ACCEPTED",
            "clamping drops geometry; pass --accept-truncation once the four criteria are met",
            report=report,
        )
    return dict(
        report,
        changed=True,
        clamped_count=report["clamped_count"],
        index_count_hint=report["clamped_count"],
        evidence="computed",
    )


# ---------------------------------------------------------------------------
# 2. DDS
# ---------------------------------------------------------------------------

DXGI_BYTES = {
    2: 16,    # R32G32B32A32_FLOAT
    10: 8,    # R16G16B16A16_FLOAT
    11: 8,    # R16G16B16A16_UNORM
    28: 4, 29: 4, 87: 4, 91: 4,   # RGBA8 / BGRA8 (+sRGB)
    61: 1,    # R8_UNORM
    71: 8, 72: 8,                 # BC1
    74: 16, 75: 16,               # BC2
    77: 16, 78: 16,               # BC3
    80: 8, 81: 8,                 # BC4
    83: 16, 84: 16,               # BC5
    95: 16, 96: 16,               # BC6H
    98: 16, 99: 16,               # BC7
}
BLOCK_FORMATS = {71, 72, 74, 75, 77, 78, 80, 81, 83, 84, 95, 96, 98, 99}
FOURCC_BYTES = {b"DXT1": 8, b"DXT3": 16, b"DXT4": 16, b"DXT5": 16,
                b"ATI1": 8, b"BC4U": 8, b"ATI2": 16, b"BC5U": 16}
FOURCC_BLOCK = set(FOURCC_BYTES)


def _mip_sizes(width: int, height: int, mips: int, unit: int, block: bool) -> List[int]:
    sizes = []
    for level in range(mips):
        w = max(width >> level, 1)
        h = max(height >> level, 1)
        if block:
            sizes.append(max(1, (w + 3) // 4) * max(1, (h + 3) // 4) * unit)
        else:
            sizes.append(w * h * unit)
    return sizes


def parse_dds(raw: bytes) -> Dict[str, Any]:
    """Parse a DDS header without guessing. Raises Refused on anything exotic."""
    _require(len(raw) >= 128 and raw[:4] == b"DDS ", "DDS_NOT_DDS", "not a DDS file")
    size, flags, height, width, pitch, depth, mips = struct.unpack_from("<7I", raw, 4)
    _require(size == 124, "DDS_BAD_HEADER", f"unexpected header size {size}")

    fourcc = raw[84:88]
    is_dx10 = fourcc == b"DX10"
    header_len = 148 if is_dx10 else 128

    dxgi = dimension = array_size = 0
    cubemap = False
    if is_dx10:
        _require(len(raw) >= 148, "DDS_TRUNCATED", "DX10 header is incomplete")
        dxgi, dimension, misc, array_size, _ = struct.unpack_from("<5I", raw, 128)
        cubemap = bool(misc & 0x4)
        array_size = max(1, array_size)
        _require(dimension != 4, "DDS_VOLUME_UNSUPPORTED",
                 "3D textures keep mip data interleaved by depth; not supported")
        unit = DXGI_BYTES.get(dxgi)
        _require(unit is not None, "DDS_FORMAT_UNKNOWN", f"unmapped DXGI format {dxgi}")
        block = dxgi in BLOCK_FORMATS
    elif fourcc in FOURCC_BLOCK:
        unit = FOURCC_BYTES[fourcc]
        block = True
    else:
        bits = struct.unpack_from("<I", raw, 88)[0]
        _require(bits in (8, 16, 24, 32), "DDS_FORMAT_UNKNOWN",
                 f"unmapped uncompressed format (bits={bits})")
        unit = bits // 8
        block = False

    mips = max(mips, 1)
    sizes = _mip_sizes(width, height, mips, unit, block)
    per_slice = sum(sizes)
    expected = header_len + per_slice * array_size
    return dict(
        header_len=header_len, width=width, height=height, mips=mips,
        unit=unit, block=block, array_size=array_size, cubemap=cubemap,
        dxgi_format=dxgi, fourcc=fourcc.decode("latin-1"),
        mip_sizes=sizes, per_slice=per_slice, expected_bytes=expected,
        actual_bytes=len(raw),
    )


def audit_dds(dds_path: Path | str, limit: int = DEFAULT_TEXTURE_LIMIT) -> Dict[str, Any]:
    """Measure one DDS. Writes nothing."""
    dds_path = Path(dds_path)
    _require(dds_path.is_file(), "DDS_NOT_FOUND", f"texture not found: {dds_path}")
    raw = dds_path.read_bytes()
    info = parse_dds(raw)
    top = info["mip_sizes"][0]
    issues = []
    if info["actual_bytes"] != info["expected_bytes"]:
        issues.append("SIZE_MISMATCH")
    if info["actual_bytes"] > limit:
        issues.append("OVER_LIMIT")
    return dict(
        ok=True, file=str(dds_path),
        **{key: info[key] for key in
           ("width", "height", "mips", "array_size", "cubemap",
            "dxgi_format", "fourcc", "block", "unit")},
        top_mip_bytes=top,
        total_bytes=info["actual_bytes"],
        expected_bytes=info["expected_bytes"],
        over_limit=info["actual_bytes"] > limit,
        limit_bytes=limit,
        droppable=info["mips"] > 1,
        largest_kept_bytes=info["per_slice"] - top,
        issues=issues,
        evidence="exact",
    )


def drop_mip0(
    dds_path: Path | str,
    output_path: Path | str,
    limit: int = DEFAULT_TEXTURE_LIMIT,
    allow_still_over: bool = False,
) -> Dict[str, Any]:
    """Drop the top mip of **every** slice into a new file.

    Refuses when there is nothing to drop, when the file disagrees with its own
    header, or when the result would still exceed the limit and the caller did
    not ask for that outcome explicitly.
    """
    dds_path, output_path = Path(dds_path), Path(output_path)
    _require(dds_path.is_file(), "DDS_NOT_FOUND", f"texture not found: {dds_path}")
    _require(dds_path.resolve() != output_path.resolve(), "DDS_WOULD_OVERWRITE",
             "output must differ from input")

    raw = dds_path.read_bytes()
    info = parse_dds(raw)
    _require(info["mips"] > 1, "DDS_SINGLE_MIP",
             "texture has a single mip level; dropping it would leave no image "
             "(resize the texture instead)")
    _require(info["actual_bytes"] == info["expected_bytes"], "DDS_SIZE_MISMATCH",
             "file length disagrees with its header; refusing to guess the layout",
             actual=info["actual_bytes"], expected=info["expected_bytes"])

    cut = info["mip_sizes"][0]
    per_slice = info["per_slice"]
    body = raw[info["header_len"]:]
    rebuilt = bytearray()
    for slice_index in range(info["array_size"]):
        base = slice_index * per_slice
        rebuilt += body[base + cut: base + per_slice]

    new_width = max(info["width"] >> 1, 1)
    new_height = max(info["height"] >> 1, 1)
    new_mips = info["mips"] - 1
    new_top = _mip_sizes(new_width, new_height, 1, info["unit"], info["block"])[0]

    header = bytearray(raw[:info["header_len"]])
    struct.pack_into("<I", header, 12, new_height)
    struct.pack_into("<I", header, 16, new_width)
    struct.pack_into("<I", header, 20, new_top)      # pitchOrLinearSize
    struct.pack_into("<I", header, 28, new_mips)

    new_bytes = bytes(header) + bytes(rebuilt)
    expected_new = info["header_len"] + (per_slice - cut) * info["array_size"]
    _require(len(new_bytes) == expected_new, "DDS_REBUILD_MISMATCH",
             "rebuilt length differs from the computed layout",
             rebuilt=len(new_bytes), expected=expected_new)

    if len(new_bytes) > limit and not allow_still_over:
        raise Refused(
            "DDS_STILL_OVER_LIMIT",
            "dropping one mip is not enough; pass --allow-still-over to write it anyway",
            new_bytes=len(new_bytes), limit=limit, slices=info["array_size"],
        )

    output_path.parent.mkdir(parents=True, exist_ok=True)
    output_path.write_bytes(new_bytes)
    return dict(
        ok=True, file=str(dds_path), output=str(output_path),
        dropped=1, slices=info["array_size"],
        original_dimensions=[info["width"], info["height"], info["mips"]],
        new_dimensions=[new_width, new_height, new_mips],
        original_bytes=info["actual_bytes"], new_bytes=len(new_bytes),
        saved_bytes=info["actual_bytes"] - len(new_bytes),
        still_over_limit=len(new_bytes) > limit,
        evidence="computed",
    )


# ---------------------------------------------------------------------------
# 3. Donor search
# ---------------------------------------------------------------------------

def _norm_rel(value: str) -> str:
    return value.replace("\\", "/").strip("/").lower()


def find_donors(need: str, root: Path | str, with_hash: bool = False) -> Dict[str, Any]:
    """List same-name files under ``root``; nothing is copied.

    Two tiers: ``path`` means the candidate's relative path ends with the
    requested relative path (strong), ``name`` means only the basename matched
    (weak, and usually a different asset that happens to share a name).
    Candidates are grouped by size so "same name, different bytes" shows up as
    a conflict instead of being silently resolved.
    """
    root = Path(root)
    _require(root.is_dir(), "DONOR_ROOT_MISSING", f"search root not found: {root}")
    rel = _norm_rel(need)
    base = rel.split("/")[-1]
    _require(bool(base), "DONOR_BAD_NAME", "need a file name to search for")

    found: List[Dict[str, Any]] = []
    for path in root.rglob("*"):
        if not path.is_file() or path.name.lower() != base:
            continue
        candidate_rel = path.relative_to(root).as_posix().lower()
        tier = "path" if candidate_rel.endswith(rel) else "name"
        entry = dict(path=str(path), rel=candidate_rel, size=path.stat().st_size, tier=tier)
        if with_hash:
            entry["sha256"] = _sha256(path)
        found.append(entry)

    sizes: Dict[int, int] = {}
    for entry in found:
        sizes[entry["size"]] = sizes.get(entry["size"], 0) + 1
    conflicts = len(sizes) > 1
    strongest = None
    if found:
        strongest = "path" if any(e["tier"] == "path" for e in found) else "name"
    return dict(
        ok=True, need=need, root=str(root), candidates=found, count=len(found),
        distinct_sizes=sorted(sizes), conflict=conflicts,
        best_tier=strongest,
        usable=bool(found) and not conflicts,
        note=("a candidate is not proof of identity: same name and same size still do not "
              "show that this is the copy the author used"),
        evidence="exact" if found else "none",
    )


def copy_donor(
    selected: Path | str,
    output_dir: Path | str,
    expect_size: Optional[int] = None,
    expect_sha256: Optional[str] = None,
    force: bool = False,
) -> Dict[str, Any]:
    """Copy one explicitly chosen donor after checking the supplied evidence."""
    selected, output_dir = Path(selected), Path(output_dir)
    _require(selected.is_file(), "DONOR_NOT_FOUND", f"donor not found: {selected}")
    _require(expect_size is not None or expect_sha256 is not None, "DONOR_NO_EVIDENCE",
             "refusing to copy without --expect-size or --expect-sha256")
    actual_size = selected.stat().st_size
    if expect_size is not None:
        _require(actual_size == expect_size, "DONOR_SIZE_MISMATCH",
                 "donor size differs from the expected size",
                 actual=actual_size, expected=expect_size)
    actual_hash = _sha256(selected) if expect_sha256 else None
    if expect_sha256:
        _require(actual_hash.lower() == expect_sha256.lower(), "DONOR_HASH_MISMATCH",
                 "donor hash differs from the expected hash",
                 actual=actual_hash, expected=expect_sha256)

    output_dir.mkdir(parents=True, exist_ok=True)
    destination = output_dir / selected.name
    _require(destination.resolve() != selected.resolve(), "DONOR_WOULD_OVERWRITE",
             "output directory is the donor's own directory; choose another target")
    if destination.exists() and not force:
        if destination.stat().st_size == actual_size and _sha256(destination) == _sha256(selected):
            return dict(ok=True, copied=False, output=str(destination),
                        reason="identical file already present", evidence="exact")
        raise Refused("DONOR_TARGET_EXISTS",
                      "a different file already occupies the destination; pass --force to replace",
                      output=str(destination))
    shutil.copy2(selected, destination)
    return dict(ok=True, copied=True, source=str(selected), output=str(destination),
                size=actual_size, sha256=_sha256(destination), evidence="exact")


# ---------------------------------------------------------------------------
# 4. Manifest edits (BEM 1.5)
# ---------------------------------------------------------------------------

PROP_RE = re.compile(r"^_[a-zA-Z0-9_]{1,95}$")
KEYWORD_RE = re.compile(r"^[A-Z0-9_]{2,96}$")
BONE_PATH_RE = re.compile(r"^[A-Za-z0-9_ /-]{1,256}$")
LIMITS = dict(floats=32, colors=16, vectors=8, keywords=16)
ROOT_OFFSET_MAX = 0.2


def load_project(path: Path | str) -> Tuple[Dict[str, Any], bool]:
    path = Path(path)
    _require(path.is_file(), "PROJECT_NOT_FOUND", f"project not found: {path}")
    data = json.loads(path.read_text(encoding="utf-8"))
    is_project = isinstance(data, dict) and "manifest" in data and "payload_files" in data
    manifest = data["manifest"] if is_project else data
    _require(isinstance(manifest, dict) and "meshes" in manifest, "PROJECT_NO_MESHES",
             "no meshes array in this manifest")
    return data, is_project


def _check_name(name: str, kind: str) -> None:
    _require(isinstance(name, str) and 2 <= len(name) <= 96, "OVERRIDE_BAD_NAME",
             f"{kind} name must be 2..96 characters: {name!r}")
    _require(PROP_RE.match(name) is not None, "OVERRIDE_BAD_NAME",
             f"{kind} name must match ^_[a-zA-Z0-9_]{{1,95}}$: {name}")


def _check_float(name: str, value: Any) -> float:
    _require(isinstance(value, (int, float)) and not isinstance(value, bool)
             and math.isfinite(value), "OVERRIDE_BAD_VALUE",
             f"{name} must be a finite number")
    _require(-128.0 <= value <= 128.0, "OVERRIDE_OUT_OF_RANGE",
             f"{name}={value} is outside [-128, 128]")
    return float(value)


def _check_rgba(name: str, value: Any, high: float, label: str) -> List[float]:
    _require(isinstance(value, (list, tuple)) and len(value) == 4, "OVERRIDE_BAD_VALUE",
             f"{name} must be four numbers")
    out = []
    for channel in value:
        _require(isinstance(channel, (int, float)) and not isinstance(channel, bool)
                 and math.isfinite(channel), "OVERRIDE_BAD_VALUE",
                 f"{name} channels must be finite numbers")
        _require(-high <= channel <= high, "OVERRIDE_OUT_OF_RANGE",
                 f"{label} channel {channel} is outside [0, {high}]")
        out.append(float(channel))
    return out


def apply_overrides(
    data: Dict[str, Any],
    is_project: bool,
    mesh_index: int = 0,
    draw_index: int = 0,
    floats: Optional[Dict[str, float]] = None,
    colors: Optional[Dict[str, List[float]]] = None,
    vectors: Optional[Dict[str, List[float]]] = None,
    enable_keywords: Optional[Iterable[str]] = None,
    disable_keywords: Optional[Iterable[str]] = None,
) -> Dict[str, Any]:
    """Write validated material overrides onto one draw."""
    manifest = data["manifest"] if is_project else data
    meshes = manifest["meshes"]
    _require(0 <= mesh_index < len(meshes), "OVERRIDE_NO_MESH",
             f"mesh {mesh_index} out of range (0..{len(meshes) - 1})")
    draws = meshes[mesh_index].get("draws", [])
    _require(0 <= draw_index < len(draws), "OVERRIDE_NO_DRAW",
             f"draw {draw_index} out of range (0..{len(draws) - 1})")
    draw = draws[draw_index]
    overrides = draw.setdefault("material_overrides", {})

    if floats:
        _require(len(floats) <= LIMITS["floats"], "OVERRIDE_TOO_MANY",
                 f"at most {LIMITS['floats']} float overrides per draw")
        target = overrides.setdefault("floats", {})
        for name, value in floats.items():
            _check_name(name, "float property")
            target[name] = _check_float(name, value)

    if colors:
        _require(len(colors) <= LIMITS["colors"], "OVERRIDE_TOO_MANY",
                 f"at most {LIMITS['colors']} color overrides per draw")
        target = overrides.setdefault("colors", {})
        for name, value in colors.items():
            _check_name(name, "color property")
            target[name] = _check_rgba(name, value, 8.0, "color")

    if vectors:
        _require(len(vectors) <= LIMITS["vectors"], "OVERRIDE_TOO_MANY",
                 f"at most {LIMITS['vectors']} vector overrides per draw")
        target = overrides.setdefault("vectors", {})
        for name, value in vectors.items():
            _check_name(name, "vector property")
            target[name] = _check_rgba(name, value, 65536.0, "vector")

    for key, words in (("enable_keywords", enable_keywords),
                       ("disable_keywords", disable_keywords)):
        if not words:
            continue
        words = list(words)
        _require(len(words) <= LIMITS["keywords"], "OVERRIDE_TOO_MANY",
                 f"at most {LIMITS['keywords']} keywords in {key}")
        bucket = overrides.setdefault(key, [])
        for word in words:
            _require(isinstance(word, str) and KEYWORD_RE.match(word) is not None,
                     "OVERRIDE_BAD_KEYWORD",
                     f"keyword must match ^[A-Z0-9_]{{2,96}}$: {word!r}")
            if word not in bucket:
                bucket.append(word)

    capabilities = manifest.setdefault("required_capabilities", [])
    if "material-overrides-v1" not in capabilities:
        capabilities.append("material-overrides-v1")
    return overrides


def apply_root_offset(
    data: Dict[str, Any],
    is_project: bool,
    translation: Tuple[float, float, float],
    bone: str = "Bip001",
) -> Dict[str, Any]:
    """Write a validated root offset. Magnitude is limited to 0.2 m."""
    _require(BONE_PATH_RE.match(bone) is not None and ".." not in bone and "//" not in bone,
             "ROOT_OFFSET_BAD_BONE", f"invalid bone path: {bone!r}")
    values = [float(value) for value in translation]
    _require(all(math.isfinite(value) for value in values), "ROOT_OFFSET_BAD_VALUE",
             "translation must be finite numbers")
    magnitude = math.sqrt(sum(value * value for value in values))
    _require(magnitude <= ROOT_OFFSET_MAX, "ROOT_OFFSET_TOO_LARGE",
             f"translation length {magnitude:.3f} m exceeds the {ROOT_OFFSET_MAX} m limit")
    manifest = data["manifest"] if is_project else data
    capabilities = manifest.setdefault("required_capabilities", [])
    if "root-offset-v1" not in capabilities:
        capabilities.append("root-offset-v1")
    manifest["root_offset"] = {"path": bone, "translation": values}
    return manifest["root_offset"]


# ---------------------------------------------------------------------------
# 5. INI duplicate-section normalisation
# ---------------------------------------------------------------------------
# Why this exists: ``component_auto.prepare()`` parses the entry INI with
# ``efmi_source.sections()``, which raises ``duplicate INI section`` on the
# second occurrence of any name. That is a purely syntactic gate — it fires
# whether or not the duplicate is harmless. The loader itself has two different
# rules for duplicates, and getting them backwards changes behaviour:
#
#   * prefix-less "global" sections (Constants, Present, ...) arriving from an
#     included file **append** their statements, in source order;
#   * prefixed sections (TextureOverride*, CommandList*, Resource*, ...) are
#     **first-occurrence-only**; later copies are parsed and thrown away.
#
# So "just keep the first one" is wrong for globals and "merge everything" is
# wrong for the prefixed ones. This module rewrites a file so the loader's
# effective result is unchanged and the parser sees no duplicates, and it
# *proves* that by projecting both texts to (active statement vectors, key
# lookup values) and comparing. A rewrite that changes the projection is
# refused rather than written.

HEADER_LINE = re.compile(r"^[ \t]*\[([^\]\n]+)\]([^\n]*)$", re.M)


def _section_taxonomy() -> Tuple[set, set]:
    """Global vs prefixed section names, single-sourced from :mod:`bem_merge`."""
    try:
        from bem_merge import GLOBAL_SECTIONS, PREFIX_SECTIONS  # type: ignore
    except Exception as exc:  # pragma: no cover - only when shipped alone
        raise Refused("INI_TAXONOMY_UNAVAILABLE",
                      "bem_merge.py must sit beside bem_repair.py for INI work",
                      detail={"error": str(exc)}) from None
    return set(GLOBAL_SECTIONS), set(PREFIX_SECTIONS)


def _classify_section(name: str) -> str:
    """``global`` / ``prefixed`` / ``unreviewed`` for a section name.

    The loader matches prefixed sections by *string prefix*, not by exact name:
    ``[TextureOverrideComponent0]`` matches the ``TextureOverride`` prefix. An
    exact-membership test here would silently classify every real override as
    "unreviewed" and refuse the whole file.
    """
    globals_set, prefixed_set = _section_taxonomy()
    key = name.strip().casefold()
    if key in globals_set:
        return "global"
    if any(key.startswith(prefix) for prefix in prefixed_set):
        return "prefixed"
    return "unreviewed"


def split_blocks(text: str) -> Tuple[str, List[Dict[str, Any]]]:
    """Split into ``(preamble, blocks)`` keeping every duplicate occurrence.

    Unlike :func:`bem_ini.read_ini` this does **not** drop later copies: the
    whole point is to see them.
    """
    text = text.replace("\r\n", "\n").replace("\r", "")
    for line in text.split("\n"):
        stripped = line.lstrip()
        if stripped.startswith("[") and not HEADER_LINE.match(line):
            raise Refused("INI_UNREVIEWED_HEADER",
                          f"section header the loader would read differently: {line[:120]!r}")
    matches = list(HEADER_LINE.finditer(text))
    blocks: List[Dict[str, Any]] = []
    for index, match in enumerate(matches):
        end = matches[index + 1].start() if index + 1 < len(matches) else len(text)
        blocks.append({
            "name": match.group(1).strip(),
            "line": text.count("\n", 0, match.start()) + 1,
            "header": match.group(0),
            "body": text[match.end():end],
        })
    preamble = text[:matches[0].start()] if matches else text
    return preamble, blocks


def _active_lines(body: str) -> List[str]:
    """Statements the loader executes: drop blanks, ``;`` comments and banners."""
    return [line.strip() for line in body.split("\n")
            if line.strip() and not line.lstrip().startswith(";")
            and not _is_watermark(line.strip())]


def ini_projection(text: str) -> Dict[str, Any]:
    """Model section collection only, before command execution or shader parsing.

    Two texts with equal projections are interchangeable as far as *which*
    statements run and *what* a key lookup returns; anything else is a change
    of behaviour and must not be silently produced.
    """
    preamble, blocks = split_blocks(text)
    vectors: Dict[str, List[str]] = {}
    maps: Dict[str, Dict[str, str]] = {}
    for block in blocks:
        key = block["name"].casefold()
        is_global = _classify_section(block["name"]) == "global"
        if key in vectors and not is_global:
            continue  # first-occurrence-only for prefixed sections
        target = vectors.setdefault(key, [])
        lookup = maps.setdefault(key, {})
        for line in _active_lines(block["body"]):
            target.append(line)
            if "=" in line:
                name, _, value = line.partition("=")
                name, value = name.strip().casefold(), value.strip()
                if is_global:
                    lookup[name] = value          # later assignment wins
                else:
                    lookup.setdefault(name, value)  # first assignment wins
    return {"preamble": _active_lines(preamble), "vectors": vectors, "maps": maps}


def audit_ini(ini_path: Path | str, include_prefixed: bool = True) -> Dict[str, Any]:
    """Report duplicate sections and the rule that governs each. Writes nothing."""
    ini_path = Path(ini_path)
    _require(ini_path.is_file(), "INI_NOT_FOUND", f"not a file: {ini_path}")
    text = ini_path.read_text(encoding="utf-8-sig", errors="replace")
    preamble, blocks = split_blocks(text)

    grouped: Dict[str, List[Dict[str, Any]]] = {}
    for block in blocks:
        grouped.setdefault(block["name"].casefold(), []).append(block)

    duplicates, unreviewed = [], []
    for key, occurrences in grouped.items():
        if len(occurrences) < 2:
            continue
        kind = _classify_section(occurrences[0]["name"])
        if not include_prefixed and kind != "global":
            continue
        rule = {"global": "append_in_source_order",
                "prefixed": "first_occurrence_only"}.get(kind, "unreviewed")
        row = {
            "section": occurrences[0]["name"],
            "occurrences": len(occurrences),
            "lines": [block["line"] for block in occurrences],
            "rule": rule,
            "active_statements": [len(_active_lines(block["body"])) for block in occurrences],
            "identical_bodies": len({
                tuple(_active_lines(block["body"])) for block in occurrences}) == 1,
        }
        if rule == "unreviewed":
            unreviewed.append(row)
        else:
            duplicates.append(row)

    empty_namespace = [line for line in _active_lines(preamble)
                       if re.fullmatch(r"(?i)namespace\s*=\s*", line)]
    declarations = [line for line in _active_lines(preamble)
                    if re.match(r"(?i)^(namespace|include|include_recursive|exclude_recursive)\s*=", line)]

    report: Dict[str, Any] = {
        "ok": not unreviewed,
        "ini": str(ini_path),
        "bytes": ini_path.stat().st_size,
        "section_count": len(blocks),
        "unique_names": len(grouped),
        "preamble_declarations": declarations,
        "duplicates": duplicates,
        "unreviewed_duplicates": unreviewed,
        "projection": {
            "section_vectors": len(ini_projection(text)["vectors"]),
        },
        "evidence": "exact",
    }
    if empty_namespace:
        report["ok"] = False
        report["empty_namespace"] = empty_namespace
        report["note"] = ("an explicitly empty namespace changes global duplicate "
                          "semantics; do not infer it")
    return report


def _normalize_text(text: str, keep_archived: bool = True) -> Tuple[str, List[Dict[str, Any]]]:
    """Pure text rewrite: duplicate-free, loader projection unchanged.

    Returns ``(result, changes)``. Raises :class:`Refused` when the rewrite
    cannot be proven equivalent. Safe to call twice — that is how idempotence
    is checked.
    """
    preamble, blocks = split_blocks(text)

    for line in _active_lines(preamble):
        if re.fullmatch(r"(?i)namespace\s*=\s*", line):
            raise Refused("INI_EMPTY_NAMESPACE",
                          "an explicitly empty namespace changes the global append rule; "
                          "this file cannot be normalised by inference")

    # ``efmi_source.sections()`` only recognises a header that starts at column
    # zero, while a human reading the file would count an indented one. Rather
    # than pick a side, refuse: the two readers disagree about what the file
    # even contains. Measured across 2695 headers in 30 real INI files, this
    # shape never occurs, so the refusal costs nothing.
    for block in blocks:
        if block["header"].lstrip() != block["header"]:
            raise Refused("INI_INDENTED_HEADER",
                          f"[{block['name']}] at line {block['line']} is indented; the "
                          "entry parser would not see it as a section",
                          detail={"line": block["line"], "header": block["header"][:80]})

    grouped: Dict[str, List[Dict[str, Any]]] = {}
    for block in blocks:
        grouped.setdefault(block["name"].casefold(), []).append(block)

    changes: List[Dict[str, Any]] = []
    emitted: List[str] = []
    for block in blocks:
        key = block["name"].casefold()
        group = grouped[key]
        first = group[0]
        if block["header"].strip() != f"[{block['name']}]":
            changes.append({
                "section": block["name"], "operation": "canonicalize_header",
                "line": block["line"], "source_header": block["header"],
                "reason": "the loader takes the text before the first ']' and trims it; "
                          "trailing text on a header line is not a command",
            })
        if len(group) == 1:
            emitted.append(f"[{block['name']}]" + block["body"])
            continue

        is_global = _classify_section(block["name"]) == "global"
        kind = _classify_section(block["name"])
        if kind == "unreviewed":
            raise Refused("INI_UNREVIEWED_GLOBAL_DUPLICATE",
                          f"[{block['name']}] repeats but is neither a known global nor a "
                          f"prefixed section; the loader's rule is not established",
                          detail={"section": block["name"],
                                  "lines": [item["line"] for item in group]})

        if block is first:
            if is_global:
                body = ""
                for occurrence in group:
                    body += (f"\n; BEM repair: [{first['name']}] occurrence at line "
                             f"{occurrence['line']}\n" + occurrence["body"])
                emitted.append(f"[{first['name']}]" + body)
                changes.append({
                    "section": first["name"], "operation": "append_in_source_order",
                    "lines": [item["line"] for item in group],
                    "active_statements": sum(len(_active_lines(item["body"])) for item in group),
                    "reason": "included-file global command vectors append; repeated "
                              "assignments are retained, later value wins",
                })
            else:
                emitted.append(f"[{first['name']}]" + first["body"])
            continue

        if is_global:
            emitted.append(f"\n; BEM repair: [{first['name']}] line {block['line']} "
                           f"appended to the first occurrence.\n")
        else:
            identical = _active_lines(first["body"]) == _active_lines(block["body"])
            changes.append({
                "section": first["name"], "operation": "archive_loader_ignored_duplicate",
                "first_line": first["line"], "ignored_line": block["line"],
                "effective_statements_equal": identical,
                "ignored_statements": len(_active_lines(block["body"])),
                "reason": "the loader does not parse a later duplicate of a prefixed "
                          "section; merging it would add behaviour",
            })
            if keep_archived:
                emitted.append(f"\n; BEM repair: begin loader-ignored duplicate "
                               f"at line {block['line']}\n")
                emitted.append("".join("; " + line for line in
                                       (block["header"] + block["body"]).splitlines(True)))
                emitted.append("\n; BEM repair: end loader-ignored duplicate\n")

    # Each emitted piece already carries the newline that separated it from the
    # next header in the source, so joining with "" preserves the original
    # spacing. Joining with "\n" would add a blank line per section per pass,
    # which is exactly what the idempotence check below is there to catch.
    result = preamble + "".join(emitted)

    before, after = ini_projection(text), ini_projection(result)
    if before != after:
        differing = [key for key in before["vectors"]
                     if before["vectors"].get(key) != after["vectors"].get(key)]
        raise Refused("INI_PROJECTION_CHANGED",
                      "the rewrite would change which statements run; refusing to write",
                      detail={"sections": differing[:20],
                              "map_changes": [key for key in before["maps"]
                                              if before["maps"].get(key) != after["maps"].get(key)][:20]})

    remaining = [block["name"] for block in split_blocks(result)[1]]
    folded = [name.casefold() for name in remaining]
    if len(set(folded)) != len(folded):
        raise Refused("INI_DUPLICATE_REMAINED",
                      "case-insensitive duplicate section survived normalisation",
                      detail={"sections": sorted({name for name in folded
                                                  if folded.count(name) > 1})})
    return result, changes


def normalize_ini(
    ini_path: Path | str,
    output: Path,
    keep_archived: bool = True,
) -> Dict[str, Any]:
    """Write a duplicate-free copy whose loader projection is provably unchanged.

    Globals append in source order; prefixed duplicates keep the first copy and
    the dead copies are archived as comments (so a human can still see them).
    """
    ini_path, output = Path(ini_path), Path(output)
    _require(ini_path.is_file(), "INI_NOT_FOUND", f"not a file: {ini_path}")
    _require(ini_path.resolve() != output.resolve(), "INI_WOULD_OVERWRITE",
             "output must differ from the input; inputs are never modified")

    text = ini_path.read_text(encoding="utf-8-sig", errors="replace")
    result, changes = _normalize_text(text, keep_archived)

    # Idempotence: a second pass over our own output must be a no-op. If it is
    # not, the first pass left work behind and the file is not stable.
    second, second_changes = _normalize_text(result, keep_archived)
    if second != result or second_changes:
        raise Refused("INI_NOT_IDEMPOTENT",
                      "a second normalisation pass would change the file again",
                      detail={"second_pass_changes": second_changes[:10]})

    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_name(output.name + ".tmp")
    temporary.write_text(result, encoding="utf-8", newline="")
    if temporary.read_text(encoding="utf-8") != result:
        temporary.unlink(missing_ok=True)
        raise Refused("INI_ROUNDTRIP_MISMATCH", "written bytes do not read back identically")
    os.replace(temporary, output)

    return {
        "ok": True,
        "input": str(ini_path),
        "output": str(output),
        "bytes_before": len(text.encode("utf-8")),
        "bytes_after": len(result.encode("utf-8")),
        "sections_before": len(split_blocks(text)[1]),
        "sections_after": len(split_blocks(result)[1]),
        "changes": changes,
        "projection_unchanged": True,
        "idempotent": True,
        "evidence": "exact",
    }



def _write_json(data: Dict[str, Any], output: Path) -> None:
    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_name(output.name + ".tmp")
    temporary.write_text(json.dumps(data, ensure_ascii=False, indent=2), encoding="utf-8")
    os.replace(temporary, output)


# ---------------------------------------------------------------------------
# 6. Conditional texture overrides -> one static selection
# ---------------------------------------------------------------------------
# ``component_auto.texture_table`` accepts exactly one shape:
#
#     [TextureOverride_X]
#     hash = <hex>
#     if $object_detected
#     this = ResourceY
#     endif
#
# A real source often writes ``if $swapvar_body==0 / this = ResourceA /
# else / this = ResourceB / endif``. That is not convertible as-is, and it is
# not a defect either — it is a *state choice*. This command resolves the choice
# for a stated variable assignment and rewrites the section into the accepted
# shape. Anything that is not a static ``this =`` replacement (per-draw
# ``ps-tN =``, ``run =``) is classified and left alone, because collapsing it
# would change which texture a draw binds.

TEXTURE_ENTRY = re.compile(r"TextureOverride_(?:EntryPoint_)?Component(\d+)(?:_LOD\d+)?$")
TEXTURE_HEADER = re.compile(r"(hash|match_priority|match_first_index|match_index_count)\s*=\s*[0-9a-fA-F-]+$")
TEXTURE_THIS = re.compile(r"this\s*=\s*(?:ref\s+)?(Resource\S+)$")
TEXTURE_SLOT = re.compile(r"ps-t\d+\s*=")
DEFAULT_ASSUMPTIONS = {
    "$object_detected": 1.0,
    "$mod_enabled": 1.0,
    "$lod_level": 0.0,
    "$lod_detected": 0.0,
}


def _numeric_environment(preamble: str, blocks: List[Dict[str, Any]],
                         overrides: Dict[str, float]) -> Tuple[Dict[str, float], Dict[str, Any]]:
    """Constants plus stated assumptions, as ``name -> float``."""
    env: Dict[str, float] = {}
    declared, skipped = {}, []
    for block in blocks:
        if block["name"].casefold() != "constants":
            continue
        for line in _active_lines(block["body"]):
            match = re.match(r"^(?:global\s+)?(?:persist\s+)?(\$[^\s=]+)\s*=\s*(.*)$", line, re.I)
            if not match:
                continue
            name, raw = match.group(1), match.group(2).strip()
            try:
                declared[name] = float(raw)
            except ValueError:
                skipped.append({"variable": name, "value": raw})
    # Order matters. ``Constants`` declares the *pre-detection* values (often 0),
    # while a conversion is by definition the "object detected, mod enabled"
    # state. So the stated assumptions override the declaration, and an explicit
    # ``--set`` overrides both. Getting this backwards silently drops every
    # texture override guarded by ``if $object_detected``.
    env.update(declared)
    env.update(DEFAULT_ASSUMPTIONS)
    env.update(overrides)
    return env, {"declared": declared, "assumed": dict(DEFAULT_ASSUMPTIONS),
                 "overridden": dict(overrides), "non_numeric": skipped}


def _resolve_condition(expression: str, env: Dict[str, float]) -> bool:
    try:
        from bem_ini import Condition  # type: ignore
    except Exception as exc:  # pragma: no cover
        raise Refused("INI_TAXONOMY_UNAVAILABLE",
                      "bem_ini.py must sit beside bem_repair.py", detail={"error": str(exc)}) from None

    # 3Dmigoto variable names are case-insensitive: `Constants` declares
    # `$SocksColor` while the condition reads `$sockscolor`. An exact-match
    # environment reports "unknown" for every such condition, which then looks
    # like a defect in the mod.
    class _Folded(dict):
        def __init__(self, data: Dict[str, float]):
            super().__init__({str(key).casefold(): value for key, value in data.items()})

        def __contains__(self, key: object) -> bool:
            return super().__contains__(str(key).casefold())

        def __getitem__(self, key: object) -> Any:
            return super().__getitem__(str(key).casefold())

        def get(self, key: object, default: Any = None) -> Any:
            return super().get(str(key).casefold(), default)

    try:
        value = Condition(expression).evaluate(_Folded(env))
    except Exception:
        value = None
    _require(value is not None, "TEXTURE_CONDITION_UNKNOWN",
             f"cannot resolve condition: {expression!r}",
             detail={"expression": expression,
                     "hint": "supply the variable with --set NAME=VALUE"})
    return bool(value)


def _walk_texture_section(block: Dict[str, Any], env: Dict[str, float]) -> Dict[str, Any]:
    """Resolve one TextureOverride to a single resource for this environment."""
    header: List[str] = []
    stack: List[List[bool]] = []      # [parent_active, any_branch_taken]
    active = True
    resource: Optional[str] = None
    per_draw: List[str] = []
    unsupported: List[str] = []

    for line in _active_lines(block["body"]):
        if TEXTURE_HEADER.match(line):
            header.append(line)
            continue
        if TEXTURE_SLOT.search(line):
            per_draw.append(line)
            continue
        if line.startswith("if ") or line.startswith("if("):
            stack.append([active, False])
            chosen = _resolve_condition(line[2:].strip(), env)
            active = active and chosen
            stack[-1][1] = chosen
            continue
        if line in ("else",) or line.startswith("elif ") or line.startswith("else if "):
            _require(bool(stack), "TEXTURE_BRANCH_UNBALANCED",
                     f"'{line}' without a matching if in [{block['name']}]")
            parent, taken = stack[-1]
            if line == "else":
                chosen = True
            else:
                expression = line.split(" ", 1)[1]
                expression = expression[len("if "):] if expression.startswith("if ") else expression
                chosen = _resolve_condition(expression.strip(), env)
            active = parent and not taken and chosen
            stack[-1][1] = taken or chosen
            continue
        if line == "endif":
            _require(bool(stack), "TEXTURE_BRANCH_UNBALANCED",
                     f"'endif' without a matching if in [{block['name']}]")
            active = stack.pop()[0]
            continue
        match = TEXTURE_THIS.match(line)
        if match:
            if active:
                resource = match.group(1)
            continue
        unsupported.append(line)

    _require(not stack, "TEXTURE_BRANCH_UNBALANCED",
             f"unclosed if in [{block['name']}]")
    return {"header": header, "resource": resource,
            "per_draw": per_draw, "unsupported": unsupported}


def normalize_textures(
    ini_path: Path | str,
    output: Path,
    overrides: Optional[Dict[str, float]] = None,
) -> Dict[str, Any]:
    """Rewrite conditional texture overrides into one static selection each."""
    ini_path, output = Path(ini_path), Path(output)
    _require(ini_path.is_file(), "INI_NOT_FOUND", f"not a file: {ini_path}")
    _require(ini_path.resolve() != output.resolve(), "INI_WOULD_OVERWRITE",
             "output must differ from the input; inputs are never modified")

    text = ini_path.read_text(encoding="utf-8-sig", errors="replace")
    preamble, blocks = split_blocks(text)
    env, env_report = _numeric_environment(preamble, blocks, overrides or {})

    selected: List[Dict[str, Any]] = []
    rewritten: List[str] = []
    dropped, untouched, refused = [], [], []

    for block in blocks:
        name = block["name"]
        if not name.startswith("TextureOverride") or TEXTURE_ENTRY.fullmatch(name):
            rewritten.append(f"[{name}]" + block["body"])
            continue
        try:
            walk = _walk_texture_section(block, env)
        except Refused as problem:
            # One section we cannot resolve must not abort the whole file: the
            # other sections are still valid, and the caller needs to see which
            # ones are blocked and why.
            refused.append({"section": name, "refused": problem.code,
                            "reason": problem.message, "detail": problem.detail})
            untouched.append(name)
            rewritten.append(f"[{name}]" + block["body"])
            continue
        if walk["unsupported"]:
            refused.append({"section": name, "statements": walk["unsupported"],
                            "reason": "not a static 'this =' replacement"})
            untouched.append(name)
            rewritten.append(f"[{name}]" + block["body"])
            continue
        if walk["per_draw"]:
            untouched.append(name)
            rewritten.append(f"[{name}]" + block["body"])
            continue
        if walk["resource"] is None:
            dropped.append({"section": name, "header": walk["header"],
                            "reason": "no branch selects a resource in this state"})
            continue
        _require(bool(walk["header"]), "TEXTURE_NO_IDENTITY",
                 f"[{name}] has no hash/match_* line, so it cannot be a texture override")
        body = "\n" + "\n".join(walk["header"]) + "\nif $object_detected\nthis = " + walk["resource"] + "\nendif\n"
        rewritten.append(f"[{name}]" + body)
        selected.append({"section": name, "resource": walk["resource"], "header": walk["header"]})

    result = preamble + "".join(rewritten)

    # Prove the rewrite selects what the source selected: re-walk the output.
    _pre2, blocks2 = split_blocks(result)
    by_name = {block["name"]: block for block in blocks2}
    for row in selected:
        try:
            again = _walk_texture_section(by_name[row["section"]], env)
        except Refused as problem:
            raise Refused("TEXTURE_SELECTION_CHANGED",
                          f"[{row['section']}] cannot be re-walked after rewrite: {problem.message}",
                          detail={"section": row["section"]}) from None
        if again["resource"] != row["resource"]:
            raise Refused("TEXTURE_SELECTION_CHANGED",
                          f"[{row['section']}] would select a different resource after rewrite",
                          detail={"expected": row["resource"], "got": again["resource"]})

    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_name(output.name + ".tmp")
    temporary.write_text(result, encoding="utf-8", newline="")
    if temporary.read_text(encoding="utf-8") != result:
        temporary.unlink(missing_ok=True)
        raise Refused("INI_ROUNDTRIP_MISMATCH", "written bytes do not read back identically")
    os.replace(temporary, output)

    return {
        "ok": not refused,
        "input": str(ini_path),
        "output": str(output),
        "environment": env_report,
        "selected": selected,
        "dropped": dropped,
        "left_untouched": untouched,
        "refused_sections": refused,
        "sections_before": len(blocks),
        "sections_after": len(blocks2),
        "evidence": "exact" if not refused else "inferred",
    }


# ---------------------------------------------------------------------------
# 7. Mesh data: skin palette, weights, vertex compaction
# ---------------------------------------------------------------------------
# These operate on an *unpacked* project (``project.json`` + ``payloads/``),
# because that is the editable form. Byte-exact edits are possible there and
# nowhere else: a packed ``.bem`` stores payloads compressed, so "fix one
# vertex" would mean rewriting the whole container.
#
# The rules come from the validator, not from taste:
#   * palette indices are UINT8 with at most 256 entries;
#   * every weighted vertex's weights sum to 65535 (UNORM16) or 1.0 (float);
#   * an index may exceed the palette only when its weight is zero.

ATTR_BYTES = [4, 2, 1, 1, 2, 2, 1, 1, 2, 2, 4, 4]
SEM_BLENDWEIGHTS = 12
SEM_BLENDINDICES = 13
MAX_PALETTE = 256


def load_payloads(project_path: Path | str, data: Dict[str, Any],
                  is_project: bool) -> List[bytes]:
    """Read every payload file of an unpacked project."""
    _require(is_project, "PROJECT_NO_PAYLOADS",
             "this command needs an unpacked project (project.json + payload_files); "
             "run 'unpack' first",
             detail={"hint": "a packed .bem stores payloads compressed"})
    project_path = Path(project_path)
    root = project_path.parent
    files = data.get("payload_files") or []
    _require(bool(files), "PROJECT_NO_PAYLOADS", "payload_files is empty")
    payloads: List[bytes] = []
    for relative in files:
        candidate = (root / relative).resolve()
        _require(candidate.is_relative_to(root.resolve()), "PAYLOAD_PATH_ESCAPE",
                 f"payload path escapes the project: {relative}")
        _require(candidate.is_file(), "PAYLOAD_MISSING", f"payload file is missing: {relative}")
        payloads.append(candidate.read_bytes())
    return payloads


def _mesh_skin_layout(mesh: Dict[str, Any]) -> Dict[str, Any]:
    """Locate the weight and index attributes of the skin stream."""
    attributes = mesh.get("attributes") or []
    weights = next((a for a in attributes if a[0] == SEM_BLENDWEIGHTS), None)
    indices = next((a for a in attributes if a[0] == SEM_BLENDINDICES), None)
    _require(indices is not None, "SKIN_NO_INDICES",
             "mesh has no BLENDINDICES attribute; is it a static mesh?")
    streams = mesh.get("streams") or []
    skin_stream = indices[3]
    _require(0 <= skin_stream < len(streams), "SKIN_BAD_STREAM",
             f"BLENDINDICES names stream {skin_stream}, which does not exist")
    stride = int(streams[skin_stream]["stride"])
    return {
        "stream": skin_stream,
        "stride": stride,
        "weights": None if weights is None else {
            "offset": int(weights[4]), "bytes": ATTR_BYTES[weights[1]], "count": int(weights[2]),
            "format": int(weights[1]),
        },
        "indices": {
            "offset": int(indices[4]), "bytes": ATTR_BYTES[indices[1]], "count": int(indices[2]),
            "format": int(indices[1]),
        },
    }


def _read_weights(raw: bytes, base: int, layout: Dict[str, Any]) -> Optional[List[float]]:
    """Weights normalised to 0..1, or ``None`` when the layout has no weights."""
    weights = layout["weights"]
    if weights is None:
        return None
    offset = base + weights["offset"]
    count = weights["count"]
    _require(count == 4, "SKIN_UNSUPPORTED_WEIGHT_COUNT",
             f"only four weights per vertex are supported, found {count}")
    if weights["bytes"] == 2:
        values = struct.unpack_from("<4H", raw, offset)
        return [value / 65535.0 for value in values]
    if weights["bytes"] == 4:
        return list(struct.unpack_from("<4f", raw, offset))
    raise Refused("SKIN_UNSUPPORTED_WEIGHT_FORMAT",
                  f"unsupported weight element width: {weights['bytes']} bytes")


def _read_indices(raw: bytes, base: int, layout: Dict[str, Any]) -> List[int]:
    indices = layout["indices"]
    offset = base + indices["offset"]
    count, width = indices["count"], indices["bytes"]
    _require(count == 4, "SKIN_UNSUPPORTED_INDEX_COUNT",
             f"only four bone indices per vertex are supported, found {count}")
    if width == 1:
        return list(struct.unpack_from("<4B", raw, offset))
    if width == 2:
        return list(struct.unpack_from("<4H", raw, offset))
    if width == 4:
        return list(struct.unpack_from("<4I", raw, offset))
    raise Refused("SKIN_UNSUPPORTED_INDEX_FORMAT",
                  f"unsupported index element width: {width} bytes")


def audit_skin(project_path: Path | str, mesh_index: Optional[int] = None) -> Dict[str, Any]:
    """Measure palette, weight sums and out-of-palette indices. Writes nothing."""
    data, is_project = load_project(project_path)
    manifest = data["manifest"] if is_project else data
    payloads = load_payloads(project_path, data, is_project)
    meshes = manifest["meshes"]
    targets = range(len(meshes)) if mesh_index is None else [mesh_index]
    _require(all(0 <= index < len(meshes) for index in targets), "MESH_INDEX_OUT_OF_RANGE",
             f"mesh index must be 0..{len(meshes) - 1}")

    reports: List[Dict[str, Any]] = []
    for index in targets:
        mesh = meshes[index]
        palette = len(mesh.get("bones") or [])
        layout = _mesh_skin_layout(mesh)
        raw = payloads[mesh["streams"][layout["stream"]]["payload"]]
        stride = layout["stride"]
        vertex_count = int(mesh["vertex_count"])
        _require(len(raw) == vertex_count * stride, "SKIN_STREAM_SIZE",
                 f"mesh {index}: skin stream is {len(raw)} bytes, "
                 f"expected {vertex_count} x {stride}")

        zero_weight, bad_sum, over_nonzero, over_zero = [], [], [], []
        max_index = 0
        for vertex in range(vertex_count):
            base = vertex * stride
            weights = _read_weights(raw, base, layout)
            indices = _read_indices(raw, base, layout)
            if weights is None:
                weights = [1.0, 0.0, 0.0, 0.0]
            total = sum(weights)
            if total == 0.0:
                zero_weight.append(vertex)
                continue
            if layout["weights"] is not None and layout["weights"]["bytes"] == 2:
                if abs(total * 65535.0 - 65535.0) > 655:
                    bad_sum.append({"vertex": vertex, "sum": round(total * 65535.0)})
            elif abs(total - 1.0) > 0.01:
                bad_sum.append({"vertex": vertex, "sum": round(total, 6)})
            for slot, bone in enumerate(indices):
                if weights[slot] == 0.0:
                    if bone >= palette:
                        over_zero.append({"vertex": vertex, "slot": slot, "index": bone})
                    continue
                if bone >= palette:
                    over_nonzero.append({"vertex": vertex, "slot": slot, "index": bone,
                                         "weight": round(weights[slot], 6)})
                else:
                    max_index = max(max_index, bone)

        reports.append({
            "mesh": index,
            "vertex_count": vertex_count,
            "palette_size": palette,
            "palette_over_limit": palette > MAX_PALETTE,
            "skin_stride": stride,
            "weights_present": layout["weights"] is not None,
            "weight_element_bytes": None if layout["weights"] is None else layout["weights"]["bytes"],
            "index_element_bytes": layout["indices"]["bytes"],
            "max_weighted_palette_index": max_index,
            "all_zero_weight_vertices": len(zero_weight),
            "zero_weight_examples": zero_weight[:10],
            "weight_sum_violations": len(bad_sum),
            "weight_sum_examples": bad_sum[:10],
            "out_of_palette_with_zero_weight": len(over_zero),
            "out_of_palette_with_nonzero_weight": len(over_nonzero),
            "out_of_palette_examples": (over_nonzero or over_zero)[:10],
        })

    blocking = [row["mesh"] for row in reports
                if row["out_of_palette_with_nonzero_weight"] or row["palette_over_limit"]]
    fixable = [row["mesh"] for row in reports
               if row["out_of_palette_with_zero_weight"] or row["weight_sum_violations"]]
    return {
        "ok": not blocking,
        "project": str(project_path),
        "meshes": reports,
        "blocking_meshes": blocking,
        "fixable_meshes": fixable,
        "note": ("an index above the palette with a non-zero weight cannot be repaired "
                 "by zeroing; it needs a wider palette or a re-map"),
        "evidence": "exact",
    }


def fix_skin(
    project_path: Path | str,
    output_dir: Path,
    mesh_index: Optional[int] = None,
    zero_weight_policy: str = "keep",
) -> Dict[str, Any]:
    """Zero out-of-palette indices that carry no weight; write a new project."""
    data, is_project = load_project(project_path)
    manifest = data["manifest"] if is_project else data
    payloads = load_payloads(project_path, data, is_project)
    output_dir = Path(output_dir)
    _require(Path(project_path).resolve() != output_dir.resolve(), "PROJECT_WOULD_OVERWRITE",
             "output directory must differ from the input project directory")
    _require(zero_weight_policy in ("keep", "first"), "SKIN_BAD_POLICY",
             "zero_weight_policy must be 'keep' or 'first'")

    meshes = manifest["meshes"]
    targets = range(len(meshes)) if mesh_index is None else [mesh_index]
    changed: List[Dict[str, Any]] = []
    new_payloads = list(payloads)

    for index in targets:
        mesh = meshes[index]
        palette = len(mesh.get("bones") or [])
        layout = _mesh_skin_layout(mesh)
        payload_id = mesh["streams"][layout["stream"]]["payload"]
        raw = bytearray(new_payloads[payload_id])
        stride = layout["stride"]
        vertex_count = int(mesh["vertex_count"])

        zeroed, fixed_vertices, refused_vertices = 0, [], []
        for vertex in range(vertex_count):
            base = vertex * stride
            weights = _read_weights(raw, base, layout) or [1.0, 0.0, 0.0, 0.0]
            indices = _read_indices(raw, base, layout)
            total = sum(weights)
            if total == 0.0 and zero_weight_policy == "first":
                # Give the vertex a full weight on bone 0. This is a visible
                # change (the vertex follows the root), so it is opt-in.
                if layout["weights"] is not None:
                    offset = base + layout["weights"]["offset"]
                    if layout["weights"]["bytes"] == 2:
                        struct.pack_into("<4H", raw, offset, 65535, 0, 0, 0)
                    else:
                        struct.pack_into("<4f", raw, offset, 1.0, 0.0, 0.0, 0.0)
                for slot in range(4):
                    _write_index(raw, base, layout, slot, 0)
                fixed_vertices.append(vertex)
                continue
            for slot, bone in enumerate(indices):
                if bone < palette:
                    continue
                if weights[slot] != 0.0:
                    refused_vertices.append({"vertex": vertex, "slot": slot,
                                             "index": bone, "weight": weights[slot]})
                    continue
                _write_index(raw, base, layout, slot, 0)
                zeroed += 1

        _require(not refused_vertices, "SKIN_BONE_OUT_OF_PALETTE",
                 f"mesh {index}: {len(refused_vertices)} weighted indices point outside the "
                 f"{palette}-bone palette; zeroing them would move geometry",
                 detail={"mesh": index, "examples": refused_vertices[:10]})
        if zeroed or fixed_vertices:
            new_payloads[payload_id] = bytes(raw)
            changed.append({"mesh": index, "payload": payload_id,
                            "zeroed_indices": zeroed,
                            "zero_weight_vertices_fixed": len(fixed_vertices)})

    if not changed:
        # Nothing to write. Copying a multi-megabyte project to prove that is
        # not a repair, so say so and stop.
        return {
            "ok": True,
            "output": None,
            "written": False,
            "changed_meshes": [],
            "zero_weight_policy": zero_weight_policy,
            "evidence": "exact",
            "note": "no vertex needed a change; the input project is still the right one",
        }

    output_dir.mkdir(parents=True, exist_ok=True)
    written = _write_project(output_dir, data, is_project, new_payloads)
    return {
        "ok": True,
        "output": str(output_dir),
        "project": str(written),
        "written": True,
        "changed_meshes": changed,
        "zero_weight_policy": zero_weight_policy,
        "evidence": "exact",
        "note": "no payload was modified in place; the input project is untouched",
    }


def _write_index(raw: bytearray, base: int, layout: Dict[str, Any], slot: int, value: int) -> None:
    offset = base + layout["indices"]["offset"] + slot * layout["indices"]["bytes"]
    width = layout["indices"]["bytes"]
    if width == 1:
        raw[offset] = value
    elif width == 2:
        struct.pack_into("<H", raw, offset, value)
    else:
        struct.pack_into("<I", raw, offset, value)


def _write_project(output_dir: Path, data: Dict[str, Any], is_project: bool,
                   payloads: List[bytes]) -> Path:
    """Write payloads plus a manifest, atomically per file."""
    payload_dir = output_dir / "payloads"
    payload_dir.mkdir(parents=True, exist_ok=True)
    names: List[str] = []
    for index, raw in enumerate(payloads):
        name = f"payloads/{index:04d}.bin"
        target = output_dir / name
        temporary = target.with_name(target.name + ".tmp")
        temporary.write_bytes(raw)
        os.replace(temporary, target)
        names.append(name)
    manifest = data["manifest"] if is_project else data
    document = {"manifest": manifest, "payload_files": names} if is_project else manifest
    project = output_dir / "project.json"
    _write_json(document, project)
    return project


PAYLOAD_KEYS = ("payload", "indices", "preview")


def _payload_refs(node: Any, path: str, found: Dict[int, List[str]]) -> None:
    """Collect every manifest integer that names a payload file."""
    if isinstance(node, dict):
        for key, value in node.items():
            child = f"{path}.{key}"
            if key in PAYLOAD_KEYS and isinstance(value, int) and not isinstance(value, bool):
                found.setdefault(value, []).append(child)
            else:
                _payload_refs(value, child, found)
    elif isinstance(node, list):
        for position, value in enumerate(node):
            _payload_refs(value, f"{path}[{position}]", found)


def prune_payloads(manifest: Dict[str, Any],
                   payloads: List[bytes]) -> Tuple[Dict[str, Any], List[bytes], Dict[str, Any]]:
    """Drop payload files nothing references and renumber the rest.

    Compaction adds a new vertex buffer; without this step the old one stays in
    the package and the file gets *bigger*. The reference scan walks the whole
    manifest rather than a hand-written field list, so a payload referenced from
    an unusual place (an animation frame, an audio clip, the preview image) is
    never dropped by mistake.
    """
    found: Dict[int, List[str]] = {}
    _payload_refs(manifest, "manifest", found)
    stray = sorted(index for index in found if not 0 <= index < len(payloads))
    _require(not stray, "PAYLOAD_REF_OUT_OF_RANGE",
             f"manifest references payload ids that do not exist: {stray[:10]}")
    keep = sorted(found)
    remap = {old: new for new, old in enumerate(keep)}

    def rewrite(node: Any) -> Any:
        if isinstance(node, dict):
            return {key: (remap[value] if key in PAYLOAD_KEYS and isinstance(value, int)
                          and not isinstance(value, bool) else rewrite(value))
                    for key, value in node.items()}
        if isinstance(node, list):
            return [rewrite(value) for value in node]
        return node

    report = {
        "payloads_before": len(payloads),
        "payloads_after": len(keep),
        "dropped": len(payloads) - len(keep),
        "dropped_bytes": sum(len(payloads[index])
                             for index in range(len(payloads)) if index not in found),
    }
    return rewrite(manifest), [payloads[index] for index in keep], report


def _stream_group(meshes: List[Dict[str, Any]], mesh_index: int) -> List[int]:
    """Every mesh that reads the same vertex buffer as ``mesh_index``.

    Compacting one mesh of a shared buffer is pointless — the old buffer must
    stay for its siblings, so the package grows. The group is compacted as one
    unit instead.
    """
    signature = tuple(stream["payload"] for stream in meshes[mesh_index]["streams"])
    return [index for index, mesh in enumerate(meshes)
            if tuple(stream["payload"] for stream in mesh["streams"]) == signature]


def vertex_compact(
    project_path: Path | str,
    output_dir: Path,
    mesh_index: int,
) -> Dict[str, Any]:
    """Keep only referenced vertices, remap indices to u32, rewrite the mesh.

    The rewrite is verified before anything is written: for every draw and every
    index, the vertex *row bytes* of the new mesh must equal the row bytes the
    original index pointed at. That is a stronger statement than "the same
    triangle count came out" — it proves no vertex moved, changed weight, or
    changed bone.
    """
    data, is_project = load_project(project_path)
    manifest = data["manifest"] if is_project else data
    payloads = load_payloads(project_path, data, is_project)
    meshes = manifest["meshes"]
    _require(0 <= mesh_index < len(meshes), "MESH_INDEX_OUT_OF_RANGE",
             f"mesh index must be 0..{len(meshes) - 1}")
    mesh = meshes[mesh_index]
    _require("indices" not in mesh, "VERTEX_COMPACT_NEEDS_DRAW_INDICES",
             "this mesh stores one index payload for the whole grid (BEM 1.0 layout); "
             "compaction only applies once every draw carries its own index payload")
    _require(int(mesh["index_size"]) == 4, "VERTEX_COMPACT_NEEDS_U32",
             "vertex compaction rewrites indices to u32; the mesh must already declare "
             "index_size 4 so the draw byte count stays meaningful")

    group = _stream_group(meshes, mesh_index)
    for member in group:
        _require("indices" not in meshes[member], "VERTEX_COMPACT_NEEDS_DRAW_INDICES",
                 f"mesh {member} shares this vertex buffer but still uses the BEM 1.0 "
                 "single-payload layout, so the group cannot be rewritten together")
        _require(int(meshes[member]["index_size"]) == 4, "VERTEX_COMPACT_NEEDS_U32",
                 f"mesh {member} shares this vertex buffer but declares index_size "
                 f"{meshes[member]['index_size']}")

    source_count = int(mesh["vertex_count"])
    _require(source_count > 0, "VERTEX_COMPACT_EMPTY_MESH", "mesh declares no vertices")
    widths = [int(stream["stride"]) for stream in mesh["streams"]]
    for position, stream in enumerate(mesh["streams"]):
        _require(len(payloads[stream["payload"]]) == source_count * widths[position],
                 "STREAM_SIZE",
                 f"mesh {mesh_index}: stream {position} is "
                 f"{len(payloads[stream['payload']])} bytes, expected "
                 f"{source_count} x {widths[position]}")

    used: List[int] = []
    seen = set()
    draw_indices: Dict[int, List[List[int]]] = {}
    for member in group:
        _require(int(meshes[member]["vertex_count"]) == source_count, "STREAM_GROUP_MISMATCH",
                 f"mesh {member} shares the buffer of mesh {mesh_index} but declares "
                 f"{meshes[member]['vertex_count']} vertices, not {source_count}")
        rows: List[List[int]] = []
        for draw in meshes[member]["draws"]:
            raw = payloads[draw["indices"]]
            count = int(draw["count"])
            _require(len(raw) == count * 4, "INDEX_PAYLOAD_SIZE",
                     f"mesh {member}: a draw payload is {len(raw)} bytes, "
                     f"expected {count} x 4")
            _require(count % 3 == 0, "INDEX_NOT_TRIANGLES",
                     f"mesh {member}: a draw count of {count} is not a multiple of 3")
            values = list(struct.unpack_from(f"<{count}I", raw, 0))
            _require(all(value < source_count for value in values), "INDEX_OUT_OF_RANGE",
                     f"mesh {member}: a draw index exceeds vertex_count")
            rows.append(values)
            for value in values:
                if value not in seen:
                    seen.add(value)
                    used.append(value)
        draw_indices[member] = rows

    _require(bool(used), "VERTEX_COMPACT_NO_DRAWS",
             "every draw in the group is empty, so there is nothing to compact")
    dropped = source_count - len(used)
    _require(dropped > 0, "VERTEX_COMPACT_NOOP",
             f"all {source_count} vertices are referenced by a draw; nothing to drop",
             detail={"hint": "compaction only pays off when the donor buffer carries "
                              "vertices no draw uses, usually other LODs"})

    order = sorted(used)
    remap = {old: new for new, old in enumerate(order)}
    new_payloads = list(payloads)
    new_stream_ids = []
    for position, stream in enumerate(mesh["streams"]):
        raw = payloads[stream["payload"]]
        width = widths[position]
        packed = bytearray()
        for old in order:
            packed += raw[old * width:(old + 1) * width]
        new_payloads.append(bytes(packed))
        new_stream_ids.append(len(new_payloads) - 1)

    # Verify before writing: the row bytes reached through the new index must be
    # identical to the row bytes reached through the old index.
    rows_by_stream = [payloads[stream["payload"]] for stream in mesh["streams"]]
    packed_rows = [new_payloads[payload_id] for payload_id in new_stream_ids]
    for member in group:
        for position, values in enumerate(draw_indices[member]):
            for old in values:
                new = remap[old]
                for stream_position, width in enumerate(widths):
                    before = rows_by_stream[stream_position][old * width:(old + 1) * width]
                    after = packed_rows[stream_position][new * width:(new + 1) * width]
                    _require(before == after, "VERTEX_COMPACT_ROW_MISMATCH",
                             f"mesh {member}: draw {position} vertex {old} would move to "
                             f"{new} with different bytes in stream {stream_position}")

    draw_cache: Dict[bytes, int] = {}
    new_manifest = json.loads(json.dumps(manifest))
    for member in group:
        member_new_ids: List[int] = []
        for values in draw_indices[member]:
            packed = struct.pack(f"<{len(values)}I", *[remap[value] for value in values])
            if packed not in draw_cache:
                new_payloads.append(packed)
                draw_cache[packed] = len(new_payloads) - 1
            member_new_ids.append(draw_cache[packed])
        replacement = dict(meshes[member])
        replacement["vertex_count"] = len(used)
        replacement["streams"] = [dict(stream, payload=new_stream_ids[position])
                                  for position, stream in enumerate(meshes[member]["streams"])]
        replacement["draws"] = [dict(draw, indices=member_new_ids[position])
                                for position, draw in enumerate(meshes[member]["draws"])]
        replacement["draws"] = [dict(draw, start=0) if "start" in draw else draw
                                for draw in replacement["draws"]]
        new_manifest["meshes"][member] = replacement

    new_manifest, new_payloads, pruned = prune_payloads(new_manifest, new_payloads)

    index_before = sum(len(payloads[payload_id]) for payload_id in
                       {draw["indices"] for member in group for draw in meshes[member]["draws"]})
    index_after = sum(len(payload) for payload in draw_cache)

    document = ({"manifest": new_manifest, "payload_files": []} if is_project
                else new_manifest)
    output_dir = Path(output_dir)
    _require(Path(project_path).resolve() != output_dir.resolve(), "PROJECT_WOULD_OVERWRITE",
             "output directory must differ from the input project directory")
    output_dir.mkdir(parents=True, exist_ok=True)
    project = _write_project(output_dir, document, is_project, new_payloads)

    return {
        "ok": True,
        "output": str(output_dir),
        "project": str(project),
        "mesh": mesh_index,
        "compacted_meshes": group,
        "source_vertices": source_count,
        "used_vertices": len(used),
        "dropped_vertices": dropped,
        "draws": sum(len(draw_indices[member]) for member in group),
        "distinct_draw_payloads": len(draw_cache),
        "vertex_bytes_before": sum(widths) * source_count,
        "vertex_bytes_after": sum(widths) * len(used),
        "index_bytes_before": index_before,
        "index_bytes_after": index_after,
        "payload_pruning": pruned,
        "input_payloads": len(payloads),
        "geometry_verified": True,
        "evidence": "exact",
        "note": ("every draw and every vertex row was compared byte for byte before the "
                 "write, so the drawn surface is identical; only unreferenced vertices "
                 "are gone. Draws are rewritten to start at 0 because each now owns its "
                 "index payload. Meshes sharing the buffer were compacted together and "
                 "the orphaned payloads were dropped."),
    }



# ---------------------------------------------------------------------------
# 8. Entry program: old guarded skip+draw -> fixed LOD0 callback
# ---------------------------------------------------------------------------
# ``component_auto.validate_entry`` accepts exactly one dispatcher program:
#
#     handling = skip
#     $\EFMIv1\component_count = $component_count
#     $\EFMIv1\identification_min_components = 3
#     run = CommandList\EFMIv1\Object_ReadConfig
#     $\EFMIv1\lod_level = $lod_level
#     run = CommandList\EFMIv1\Component_ReadConfig
#     Pool\EFMIv1\Input_ObjectSpatialIdentity = ref Pool_ObjectSpatialIdentity
#     run = CommandList\EFMIv1\SpatialIdentity_IdentifyComponentInstances
#     run = CommandList\EFMIv1\Component_DrawInstances
#
# Older packages instead write the whole thing inline:
#
#     if $mod_enabled && DRAW_TYPE == 4
#     handling = skip
#     run = CommandListSomeDirectDraw
#     endif
#
# That is not a defect in the mod; it is an older template. Lowering it is
# mechanical, and the replacement is *not* free: the direct draw is replaced by
# the catalog-driven instance walk, so the callback must be a LOD0 draw that the
# runtime can re-target. This command performs the lowering and then re-parses
# its own output to prove the gate now passes.

ENTRY_SECTION = re.compile(r"TextureOverride_(?:EntryPoint_)?Component(\d+)$")
ENTRY_GUARD = "if $mod_enabled && DRAW_TYPE == 4"
ENTRY_DISPATCHER = "CommandListBEMReviewedDispatcher"
ENTRY_PROGRAM = [
    "handling = skip",
    r"$\EFMIv1\component_count = $component_count",
    r"$\EFMIv1\identification_min_components = 3",
    r"run = CommandList\EFMIv1\Object_ReadConfig",
    r"$\EFMIv1\lod_level = $lod_level",
    r"run = CommandList\EFMIv1\Component_ReadConfig",
    r"Pool\EFMIv1\Input_ObjectSpatialIdentity = ref Pool_ObjectSpatialIdentity",
    r"run = CommandList\EFMIv1\SpatialIdentity_IdentifyComponentInstances",
    r"run = CommandList\EFMIv1\Component_DrawInstances",
]
ENTRY_HEADER = re.compile(r"(?:hash|match_index_count|match_first_index)\s*=\s*[0-9a-fA-F]+$")
ENTRY_HASH = re.compile(r"^hash\s*=\s*([0-9a-fA-F]{8})$")
ENTRY_SAFE_HEADER = re.compile(
    r"(?:(?:hash|match_index_count|match_first_index)\s*=\s*[0-9a-fA-F]+"
    r"|\$object_detected\s*=\s*1|\$lod_level\s*=\s*0|\$lod_detected\s*=\s*0)$")
ENTRY_CALLBACK_BODY = ([r"run = CommandList\EFMIv1\OverrideTextures", "draw = from_caller"],
                       ["draw = from_caller"])


def _tools_root() -> Optional[Path]:
    """Locate the directory holding the CLI modules, if this is a checkout.

    In an installed toolchain the CLI is a compiled executable and these modules
    do not exist. Every caller must degrade to a named refusal rather than
    guess, which is why this returns ``None`` instead of raising.
    """
    for parent in Path(__file__).resolve().parents:
        if (parent / "component_auto.py").is_file():
            return parent
    return None


def _component_auto() -> Any:
    root = _tools_root()
    _require(root is not None, "ENTRY_TEMPLATE_UNAVAILABLE",
             "component_auto.py is not reachable from this script, so the entry gate "
             "cannot be checked after the rewrite",
             detail={"hint": "run this command from a repository checkout, not from an "
                              "installed toolchain"})
    assert root is not None
    if str(root) not in sys.path:
        sys.path.insert(0, str(root))
    import component_auto  # type: ignore
    return component_auto


def _watermark_patterns() -> Tuple[Any, Any]:
    """The injected-banner patterns, single-sourced from :mod:`bem_merge`."""
    try:
        from bem_merge import _WATERMARK, _WATERMARK_SUFFIX  # type: ignore
    except Exception:  # pragma: no cover - only when shipped alone
        return None, None
    return _WATERMARK, _WATERMARK_SUFFIX


def _is_watermark(statement: str) -> bool:
    """A scraper banner spliced into the file, not a command.

    ``bem_merge`` removes these on the same rule: no ``=`` and a bracketed CJK
    banner. Reusing its patterns keeps the two tools from disagreeing about what
    counts as a statement.
    """
    if "=" in statement:
        return False
    banner, suffix = _watermark_patterns()
    if banner is None:
        return False
    return bool(banner.match(statement) or suffix.search(statement))


def _entry_lines(body: str) -> List[str]:
    """Statements only: comments, blanks and injected banners removed."""
    out = []
    for line in body.split("\n"):
        statement = re.sub(r"\s+", " ", line.split(";", 1)[0]).strip()
        if statement and not _is_watermark(statement):
            out.append(statement)
    return out


def _canonical(statement: str) -> str:
    return re.sub(r"\s+", "", statement.split(";", 1)[0])


def _tail(body: str) -> str:
    """The trailing newlines of a section body, so a rewrite keeps the spacing."""
    stripped = body.rstrip("\n")
    return body[len(stripped):] or "\n"


def _dispatcher_program(body: str) -> List[str]:
    return [re.sub(r"\s+", " ", line.split(";", 1)[0]).strip()
            for line in body.split("\n") if line.split(";", 1)[0].strip()]


def _dispatcher_ok(body: str) -> bool:
    return ([_canonical(line) for line in _dispatcher_program(body)]
            == [_canonical(line) for line in ENTRY_PROGRAM])


def _dispatcher_diff(body: str) -> Dict[str, List[str]]:
    """Statements the template has but the dispatcher lacks, and vice versa."""
    actual = _dispatcher_program(body)
    template = {_canonical(line): line for line in ENTRY_PROGRAM}
    present = {_canonical(line) for line in actual}
    missing = [line for key, line in template.items() if key not in present]
    extra = [line for line in actual if _canonical(line) not in template]
    return {"missing": missing, "extra": extra}


def _entry_diagnose(sections: Dict[str, str], name: str,
                    component: int) -> Dict[str, Any]:
    """Compare one entry against ``component_auto.validate_entry``'s template.

    Returns the shape verdict plus, when the shape is right but the gate still
    fails, *which* dispatcher statements differ. That distinction is the whole
    point: a missing line means the mod predates the template and can be
    upgraded; an extra line means the mod is *newer* than the converter and the
    template is what needs fixing.
    """
    lines = _entry_lines(sections[name])
    opened = ended = False
    callback = dispatcher = None
    shape_ok = True
    reason = ""
    gpu_posed: Optional[int] = None
    for line in lines:
        if ENTRY_HEADER.match(line) or line == "$object_detected = 1":
            continue
        if line == ENTRY_GUARD and not opened:
            opened = True
            continue
        if line == "endif" and opened and not ended:
            ended = True
            continue
        if not opened or ended:
            shape_ok, reason = False, f"statement outside the guard: {line}"
            break
        if re.fullmatch(r"\$\\EFMIv1\\component_id = " + str(component), line):
            continue
        match = re.fullmatch(r"\$\\EFMIv1\\gpu_posed = ([01])", line)
        if match:
            gpu_posed = int(match[1])
            continue
        if line == "$lod_level = 0":
            continue
        match = re.fullmatch(r"CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref (CommandList\w+)",
                             line)
        if match and callback is None:
            callback = match[1]
            continue
        match = re.fullmatch(r"run = (CommandList\w+)", line)
        if match and dispatcher is None:
            dispatcher = match[1]
            continue
        shape_ok, reason = False, f"unrecognised entry statement: {line}"
        break
    if shape_ok and not (opened and ended and callback and dispatcher):
        shape_ok = False
        reason = "entry is missing the guard, the callback ref, or the dispatcher run"

    report: Dict[str, Any] = {
        "shape_ok": shape_ok,
        "reason": reason,
        "callback": callback,
        "dispatcher": dispatcher,
        "gpu_posed": gpu_posed,
        "dispatcher_present": dispatcher in sections if dispatcher else False,
        "missing": [],
        "extra": [],
        "standard": False,
    }
    if shape_ok and dispatcher:
        if dispatcher not in sections:
            report["reason"] = f"dispatcher [{dispatcher}] is not defined in this file"
            report["shape_ok"] = False
        else:
            diff = _dispatcher_diff(sections[dispatcher])
            report.update(diff)
            report["standard"] = not diff["missing"] and not diff["extra"]
    return report


def entry_normalize(
    ini_path: Path | str,
    output: Path,
    zero_draw_policy: str = "keep",
) -> Dict[str, Any]:
    """Lower old inline entry programs to the fixed LOD0 callback form.

    ``zero_draw_policy`` decides what to do with a callback whose whole body is
    ``draw = from_caller`` (a pure original CPU-posed draw):

    * ``keep``  — rewrite it to ``drawindexed = INDEX_COUNT, FIRST_INDEX, 0`` so
      BEM keeps the original geometry;
    * ``refuse`` — stop and report it, because keeping a CPU-posed draw also
      means any texture override in the package has no effect on it.
    """
    ini_path, output = Path(ini_path), Path(output)
    _require(ini_path.is_file(), "INI_NOT_FOUND", f"not a file: {ini_path}")
    _require(ini_path.resolve() != output.resolve(), "INI_WOULD_OVERWRITE",
             "output must differ from the input; inputs are never modified")
    _require(zero_draw_policy in ("keep", "refuse"), "ENTRY_BAD_POLICY",
             "zero_draw_policy must be 'keep' or 'refuse'")

    text = ini_path.read_text(encoding="utf-8-sig", errors="replace")
    preamble, blocks = split_blocks(text)
    sections: Dict[str, str] = {}
    for block in blocks:
        if block["name"] in sections:
            raise Refused("ENTRY_DUPLICATE_SECTION",
                          f"[{block['name']}] appears more than once; run 'ini-normalize' "
                          "first so the entry rewrite has one body per name",
                          detail={"section": block["name"]})
        sections[block["name"]] = block["body"]

    targets = []
    for block in blocks:
        match = ENTRY_SECTION.fullmatch(block["name"])
        if not match:
            continue
        identity = ENTRY_HASH.match(_entry_lines(block["body"])[0]
                                    if _entry_lines(block["body"]) else "")
        _require(identity is not None, "ENTRY_NO_IDENTITY",
                 f"[{block['name']}] has no 8-hex 'hash =' line, so it cannot be a "
                 "component entry")
        targets.append({"section": block["name"], "component": int(match[1]),
                        "hash": identity[1].lower()})
    if not targets:
        # A companion/auxiliary INI has no ComponentN entry at all. That is not
        # a defect and not a refusal: this command simply does not apply.
        return {
            "ok": True,
            "input": str(ini_path),
            "output": None,
            "written": False,
            "applicable": False,
            "reason": "no TextureOverride_(EntryPoint_)?ComponentN section in this file",
            "evidence": "exact",
        }
    duplicated = {row["component"] for row in targets
                  if [item["component"] for item in targets].count(row["component"]) > 1}
    _require(not duplicated, "ENTRY_DUPLICATE_COMPONENT",
             f"components {sorted(duplicated)} have more than one entry")

    already, lowered, refused_rows = [], [], []
    stale_by_dispatcher: Dict[str, Dict[str, Any]] = {}
    replacements: Dict[str, str] = {}
    evidence: List[Dict[str, Any]] = []
    uses_dispatcher = False

    for row in targets:
        name, component = row["section"], row["component"]
        diagnosis = _entry_diagnose(sections, name, component)
        if diagnosis["standard"]:
            already.append(name)
            continue

        # Case 0: the component is not GPU-posed at all. Its draw stays in the
        # original skeleton, so there is no callback the converter can re-target
        # and no rewrite that preserves it.
        if diagnosis["gpu_posed"] == 0:
            refused_rows.append({
                "section": name, "refused": "ENTRY_CPU_POSED_COMPONENT",
                "reason": (f"component {component} declares $\\EFMIv1\\gpu_posed = 0, so its "
                           "draw stays in the original skeleton; BEM cannot re-target it"),
            })
            continue

        # Case 1: the entry shape is right, the dispatcher only lacks template
        # statements. The mod predates the template, so upgrading it is a
        # faithful rewrite.
        if diagnosis["shape_ok"] and diagnosis["missing"]:
            dispatcher = diagnosis["dispatcher"]
            assert isinstance(dispatcher, str)
            replacements[dispatcher] = ("\n".join(ENTRY_PROGRAM)
                                        + _tail(sections[dispatcher]))
            evidence.append({
                "section": dispatcher, "component": component, "operation": "upgrade_dispatcher",
                "added_statements": diagnosis["missing"],
                "semantics": "the local dispatcher lacked standard EFMIv1 statements; the "
                             "canonical program is written in its place",
            })
            lowered.append(name)
            continue

        # Case 2: the dispatcher has statements the template does not. Rewriting
        # it would delete behaviour (instance counts, merged-skeleton wiring), so
        # the mod is correct and the converter is behind.
        if diagnosis["shape_ok"] and diagnosis["extra"]:
            key = str(diagnosis["dispatcher"])
            row_stale = stale_by_dispatcher.setdefault(key, {
                "dispatcher": key,
                "extra_statements": diagnosis["extra"],
                "missing_statements": diagnosis["missing"],
                "entries": [],
            })
            row_stale["entries"].append(name)
            refused_rows.append({
                "section": name, "refused": "ENTRY_TEMPLATE_STALE",
                "reason": (f"dispatcher [{key}] carries {len(diagnosis['extra'])} statement(s) "
                           "the accepted template does not know about; rewriting it would "
                           "delete runtime behaviour"),
            })
            continue

        # Case 3: the old inline guarded skip+draw, which is genuinely lowerable.
        body = _entry_lines(sections[name])
        try:
            _require(body.count(ENTRY_GUARD) == 1, "ENTRY_UNSUPPORTED",
                     f"[{name}] does not have exactly one '{ENTRY_GUARD}' guard"
                     + (f" ({diagnosis['reason']})" if diagnosis["reason"] else ""))
            guard = body.index(ENTRY_GUARD)
            _require(body.count("endif") == 1, "ENTRY_UNSUPPORTED",
                     f"[{name}] does not have exactly one 'endif'")
            end = body.index("endif")
            _require(guard < end, "ENTRY_UNSUPPORTED", f"[{name}] has 'endif' before the guard")
            after = body[end + 1:]
            _require(not after, "ENTRY_UNSUPPORTED",
                     f"[{name}] has statements after 'endif': {after[:3]}")
            before, inside = body[:guard], body[guard + 1:end]
            for line in before:
                _require(bool(ENTRY_SAFE_HEADER.match(line)), "ENTRY_UNSUPPORTED",
                         f"[{name}] has an unexpected header statement: {line}")
            _require("handling = skip" in inside, "ENTRY_UNSUPPORTED",
                     f"[{name}] does not suppress the original draw ('handling = skip')")
            callback = None
            for line in inside:
                if line == "handling = skip":
                    continue
                match = re.fullmatch(r"run = (CommandList\w+)", line)
                _require(match is not None and callback is None, "ENTRY_UNSUPPORTED",
                         f"[{name}] has an unexpected draw statement: {line}")
                callback = match[1]
                _require(callback in sections, "ENTRY_MISSING_CALLBACK",
                         f"[{name}] runs {callback}, which this file does not define")
        except Refused as problem:
            refused_rows.append({"section": name, "refused": problem.code,
                                 "reason": problem.message})
            continue

        declaration = [line for line in before if ENTRY_HEADER.match(line)]
        if callback is None:
            # A suppressed entry with no draw at all still needs a callback
            # section for the runtime to call; an empty one draws nothing.
            callback = "CommandListBEMReviewedHide" + str(component)
            _require(callback not in sections, "ENTRY_NAME_TAKEN",
                     f"[{callback}] already exists in the source")
            replacements[callback] = ""
            evidence.append({"section": callback, "component": component,
                             "semantics": "synthesised empty callback: the source entry "
                                          "suppressed the original draw and drew nothing"})

        if _entry_lines(sections.get(callback, "")) in ENTRY_CALLBACK_BODY:
            has_overrides = any(
                key.startswith("TextureOverride") and not ENTRY_SECTION.fullmatch(key)
                and not re.fullmatch(r"TextureOverride_(?:EntryPoint_)?Component\d+_LOD\d+", key)
                for key in sections)
            _require(zero_draw_policy == "keep" or not has_overrides, "ENTRY_CPU_DRAW_WITH_TEXTURES",
                     f"[{callback}] is a pure original CPU-posed draw, but this package also "
                     "replaces textures; keeping the draw would leave those overrides unused",
                     detail={"callback": callback, "section": name,
                             "hint": "pass --zero-draw-policy keep to accept the CPU draw"})
            replacements[callback] = "drawindexed = INDEX_COUNT, FIRST_INDEX, 0\n"
            evidence.append({"section": callback, "component": component,
                             "semantics": "pure original CPU-posed draw is retained as a BEM "
                                          "keep; no texture replacement is applied to it"})

        replacements[name] = "\n".join(
            declaration + ["$object_detected = 1", ENTRY_GUARD,
                           f"$\\EFMIv1\\component_id = {component}",
                           "$\\EFMIv1\\gpu_posed = 1", "$lod_level = 0",
                           f"CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref {callback}",
                           f"run = {ENTRY_DISPATCHER}", "endif"]) + "\n"
        evidence.append({"section": name, "component": component, "hash": row["hash"],
                         "callback": callback, "original_statements": body,
                         "semantics": "old direct guarded skip+draw lowered to a fixed LOD0 "
                                      "callback; no source GPU program is executed"})
        lowered.append(name)
        uses_dispatcher = True

    if not lowered:
        return {
            "ok": not refused_rows,
            "input": str(ini_path),
            "output": None,
            "written": False,
            "applicable": True,
            "components": len(targets),
            "already_standard": already,
            "lowered": [],
            "template_stale": list(stale_by_dispatcher.values()),
            "refused_sections": refused_rows,
            "evidence": "exact",
            "note": ("every entry either already matches the accepted template or cannot be "
                     "rewritten without deleting runtime behaviour; nothing to write"),
        }

    if uses_dispatcher and ENTRY_DISPATCHER not in sections:
        replacements[ENTRY_DISPATCHER] = "\n".join(ENTRY_PROGRAM) + "\n"

    # $component_count must exist as a global; the dispatcher reads it.
    constants = [block for block in blocks if block["name"].casefold() == "constants"]
    needs_count = uses_dispatcher and not any(
        re.search(r"(?m)^\s*global\s+\$component_count\s*=", block["body"])
        for block in constants)
    if needs_count:
        assignment = f"\nglobal $component_count = {len(targets)}\n"
        if constants:
            first = constants[0]
            replacements[first["name"]] = sections[first["name"]] + assignment
        else:
            replacements["Constants"] = assignment.lstrip("\n")
            evidence.append({"section": "Constants", "semantics": "created: the dispatcher "
                             "reads $component_count and the source never declared it"})

    emitted: List[str] = []
    written_names = set()
    for block in blocks:
        name = block["name"]
        if name in replacements:
            emitted.append(f"[{name}]\n" + replacements[name].lstrip("\n"))
            written_names.add(name)
        else:
            emitted.append(f"[{name}]" + block["body"])
    for name, body in replacements.items():
        if name not in written_names:
            emitted.append(f"[{name}]\n" + body.lstrip("\n"))
    result = preamble + "".join(emitted)

    # Prove the gate now passes on our own output.
    gate = _component_auto()
    _preamble2, blocks2 = split_blocks(result)
    after_sections = {block["name"]: block["body"] for block in blocks2}
    _require(len(after_sections) == len(blocks2), "ENTRY_DUPLICATE_AFTER_REWRITE",
             "the rewrite produced a duplicate section name")
    for row in targets:
        if row["section"] in {item["section"] for item in refused_rows}:
            continue
        try:
            gate.validate_entry(after_sections, {"section": row["section"],
                                                 "source_component": row["component"]})
        except ValueError as exc:
            raise Refused("ENTRY_STILL_INVALID",
                          f"[{row['section']}] still fails the entry gate after rewrite: {exc}",
                          detail={"section": row["section"]}) from None

    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_name(output.name + ".tmp")
    temporary.write_text(result, encoding="utf-8", newline="")
    if temporary.read_text(encoding="utf-8") != result:
        temporary.unlink(missing_ok=True)
        raise Refused("INI_ROUNDTRIP_MISMATCH", "written bytes do not read back identically")
    os.replace(temporary, output)

    return {
        "ok": not refused_rows,
        "input": str(ini_path),
        "output": str(output),
        "written": True,
        "components": len(targets),
        "already_standard": already,
        "lowered": lowered,
        "template_stale": list(stale_by_dispatcher.values()),
        "refused_sections": refused_rows,
        "dispatcher_added": uses_dispatcher and ENTRY_DISPATCHER not in sections,
        "component_count_added": needs_count,
        "evidence": evidence,
        "gate_rechecked": True,
        "note": ("the rewritten file was re-parsed and every entry passed "
                 "component_auto.validate_entry before it was written"),
    }


# ---------------------------------------------------------------------------
# 9. Skin stream conversion: merged skeleton -> mesh-local palette
# ---------------------------------------------------------------------------
# EFMI merged-skeleton sources store the *global* bone id in the skin stream,
# not an index into the mesh's own palette:
#
#   stride 8   four UINT16 bone ids, rigid (weight 1 on slot 0)
#   stride 16  four UNORM16 weights at 0, four UINT16 bone ids at 8
#
# BEM only accepts the native layouts (spec §7):
#
#   stride 12  [12,4,4,2,0] UNORM16 weights, [13,6,4,2,8]  UINT8  indices
#   stride 32  [12,0,4,2,0] Float32 weights, [13,10,4,2,16] UINT32 indices
#
# So the conversion is two jobs at once: re-encode the elements *and* translate
# every global bone id into a mesh-local palette index, building the palette in
# first-use order. Getting the palette wrong is silent — the mesh deforms against
# the wrong bones — so the rewrite is verified by comparing the (weight, bone
# name) pairs of every vertex before and after.

SKIN_NATIVE = {
    12: {"weights": (0, 2), "indices": (8, 1)},
    32: {"weights": (0, 4), "indices": (16, 4)},
}
SKIN_MERGED = {8: "rigid", 16: "weighted"}
SKIN_MERGED_LAYOUTS = {"merged8", "merged16"}


def _skin_layout_name(mesh: Dict[str, Any]) -> str:
    """``native12`` / ``native32`` / ``merged8`` / ``merged16`` / ``static`` / ``unknown``."""
    attributes = mesh.get("attributes") or []
    indices = next((a for a in attributes if a[0] == SEM_BLENDINDICES), None)
    weights = next((a for a in attributes if a[0] == SEM_BLENDWEIGHTS), None)
    if indices is None:
        return "static"
    stream = int(indices[3])
    streams = mesh.get("streams") or []
    if not 0 <= stream < len(streams):
        return "unknown"
    stride = int(streams[stream]["stride"])
    if weights is None:
        # Stride 8 with no weight attribute is the rigid merged form: four
        # UINT16 global bone ids, weight 1 on the first slot.
        if (stride == 8 and int(indices[1]) == 4
                and int(indices[4]) == 0 and int(indices[2]) == 4):
            return "merged8"
        return "unknown"
    pair = (int(weights[4]), ATTR_BYTES[weights[1]],
            int(indices[4]), ATTR_BYTES[indices[1]])
    for native_stride, spec in SKIN_NATIVE.items():
        if pair == (spec["weights"][0], spec["weights"][1],
                    spec["indices"][0], spec["indices"][1]):
            return f"native{native_stride}"
    if pair == (0, 2, 8, 2):
        return "merged16"
    return "unknown"


def _bone_table_from_target(manifest: Dict[str, Any]) -> List[Dict[str, Any]]:
    """Concatenate ``target.components[].bone_names`` into the merged table.

    That concatenation *is* the global bone id space an EFMI merged-skeleton
    source indexes into, so it is the natural default when the project already
    carries a 1.4 target block.
    """
    target = manifest.get("target") or {}
    components = target.get("components") or []
    table: List[Dict[str, Any]] = []
    for position, component in enumerate(components):
        component_id = int(component.get("id", position))
        for index, name in enumerate(component.get("bone_names") or []):
            table.append({"component": component_id, "index": index, "name": str(name)})
    return table


def _load_bone_table(project_path: Path | str, manifest: Dict[str, Any],
                     palette_path: Optional[Path]) -> List[Dict[str, Any]]:
    if palette_path is not None:
        raw = json.loads(Path(palette_path).read_text(encoding="utf-8"))
        _require(isinstance(raw, list), "SKIN_BAD_PALETTE",
                 "the palette file must be a JSON array of bone records")
        table = []
        for position, row in enumerate(raw):
            _require(isinstance(row, dict) and "name" in row, "SKIN_BAD_PALETTE",
                     f"palette entry {position} needs at least a 'name'",
                     detail={"entry": row})
            table.append({"component": int(row.get("component", 0)),
                          "index": int(row.get("index", position)),
                          "name": str(row["name"])})
        _require(bool(table), "SKIN_BAD_PALETTE", "the palette file is empty")
        return table
    table = _bone_table_from_target(manifest)
    _require(bool(table), "SKIN_NO_BONE_TABLE",
             "the project has no target.components[].bone_names and no --palette was given",
             detail={"hint": "pass --palette bones.json: a JSON array of "
                              "{component, index, name} in global bone-id order"})
    return table


def _skin_group(meshes: List[Dict[str, Any]], mesh_index: int) -> List[int]:
    """Meshes that read the same skin payload as ``mesh_index``."""
    mesh = meshes[mesh_index]
    indices = next((a for a in (mesh.get("attributes") or [])
                    if a[0] == SEM_BLENDINDICES), None)
    _require(indices is not None, "SKIN_NO_INDICES",
             f"mesh {mesh_index} has no BLENDINDICES attribute; is it a static mesh?")
    stream = int(indices[3])
    payload_id = int(mesh["streams"][stream]["payload"])
    group = []
    for index, candidate in enumerate(meshes):
        other = next((a for a in (candidate.get("attributes") or [])
                      if a[0] == SEM_BLENDINDICES), None)
        if other is None or int(other[3]) != stream:
            continue
        if int(candidate["streams"][stream]["payload"]) == payload_id:
            group.append(index)
    return group


def skin_convert(
    project_path: Path | str,
    output_dir: Path,
    mesh_index: int,
    target_stride: int = 12,
    palette_path: Optional[Path] = None,
) -> Dict[str, Any]:
    """Re-encode a merged-skeleton skin stream into a BEM-native layout."""
    _require(target_stride in SKIN_NATIVE, "SKIN_BAD_STRIDE",
             f"target stride must be one of {sorted(SKIN_NATIVE)}")
    data, is_project = load_project(project_path)
    manifest = data["manifest"] if is_project else data
    payloads = load_payloads(project_path, data, is_project)
    meshes = manifest["meshes"]
    _require(0 <= mesh_index < len(meshes), "MESH_INDEX_OUT_OF_RANGE",
             f"mesh index must be 0..{len(meshes) - 1}")

    group = _skin_group(meshes, mesh_index)
    kinds = {member: _skin_layout_name(meshes[member]) for member in group}
    native = [member for member, kind in kinds.items() if kind.startswith("native")]
    _require(not native, "SKIN_ALREADY_NATIVE",
             f"mesh(es) {native} already use a native skin layout; nothing to convert",
             detail={"layouts": {str(k): v for k, v in kinds.items()}})
    unknown = [member for member, kind in kinds.items()
               if kind not in SKIN_MERGED_LAYOUTS]
    _require(not unknown, "SKIN_UNKNOWN_LAYOUT",
             f"mesh(es) {unknown} have a skin layout this tool does not recognise",
             detail={"layouts": {str(k): v for k, v in kinds.items()}})

    mesh = meshes[mesh_index]
    layout = _mesh_skin_layout(mesh)
    stream_position = layout["stream"]
    source_stride = layout["stride"]
    source_payload = mesh["streams"][stream_position]["payload"]
    source = payloads[source_payload]
    source_kind = SKIN_MERGED[source_stride]
    table = _load_bone_table(project_path, manifest, palette_path)

    # Every vertex a draw can reach, across the whole sharing group.
    vertex_counts = {int(meshes[member]["vertex_count"]) for member in group}
    _require(len(vertex_counts) == 1, "SKIN_GROUP_MISMATCH",
             f"meshes {group} share a skin payload but disagree on vertex_count")
    vertex_count = vertex_counts.pop()
    _require(len(source) == vertex_count * source_stride, "SKIN_STREAM_SIZE",
             f"skin payload is {len(source)} bytes, expected "
             f"{vertex_count} x {source_stride}")
    used: List[int] = []
    seen = set()
    for member in group:
        for draw in meshes[member]["draws"]:
            raw = payloads[draw["indices"]]
            count = int(draw["count"])
            _require(len(raw) == count * 4, "INDEX_PAYLOAD_SIZE",
                     f"mesh {member}: draw payload is {len(raw)} bytes for {count} indices")
            for value in struct.unpack_from(f"<{count}I", raw, 0):
                _require(value < vertex_count, "INDEX_OUT_OF_RANGE",
                         f"mesh {member}: a draw index exceeds vertex_count")
                if value not in seen:
                    seen.add(value)
                    used.append(value)
    _require(bool(used), "SKIN_NO_DRAWS", "every draw in the group is empty")

    # Translate global bone ids into a mesh-local palette, in first-use order.
    palette: List[Dict[str, Any]] = []
    local: Dict[int, int] = {}
    plan: Dict[int, List[Tuple[int, int]]] = {}
    for vertex in used:
        base = vertex * source_stride
        if source_kind == "weighted":
            weights = struct.unpack_from("<4H", source, base)
            bones = struct.unpack_from("<4H", source, base + 8)
        else:
            weights = (65535, 0, 0, 0)
            bones = struct.unpack_from("<4H", source, base)
        pairs = []
        for weight, bone in zip(weights, bones):
            if weight == 0:
                pairs.append((0, 0))
                continue
            _require(bone < len(table), "SKIN_BONE_OUT_OF_TABLE",
                     f"vertex {vertex} weights global bone {bone}, but the bone table has "
                     f"{len(table)} entries",
                     detail={"vertex": vertex, "bone": bone, "table_size": len(table)})
            if bone not in local:
                local[bone] = len(palette)
                palette.append(table[bone])
            pairs.append((weight, local[bone]))
        plan[vertex] = pairs
    _require(len(palette) <= MAX_PALETTE, "SKIN_PALETTE_OVERFLOW",
             f"the used vertices reference {len(palette)} distinct bones, over the "
             f"{MAX_PALETTE}-entry palette limit",
             detail={"hint": "split the mesh, or drop the components that carry the "
                              "extra bones"})

    spec = SKIN_NATIVE[target_stride]
    converted = bytearray(vertex_count * target_stride)
    weight_offset, weight_bytes = spec["weights"]
    index_offset, index_bytes = spec["indices"]
    for vertex in used:
        base = vertex * target_stride
        pairs = plan[vertex]
        if weight_bytes == 2:
            struct.pack_into("<4H", converted, base + weight_offset,
                             *[weight for weight, _ in pairs])
        else:
            struct.pack_into("<4f", converted, base + weight_offset,
                             *[weight / 65535.0 for weight, _ in pairs])
        if index_bytes == 1:
            struct.pack_into("<4B", converted, base + index_offset,
                             *[bone for _, bone in pairs])
        else:
            struct.pack_into("<4I", converted, base + index_offset,
                             *[bone for _, bone in pairs])

    # Verify: the (weight, bone name) pairs must be identical before and after.
    for vertex in used:
        base = vertex * target_stride
        if weight_bytes == 2:
            after_weights = struct.unpack_from("<4H", converted, base + weight_offset)
        else:
            after_weights = tuple(int(round(value * 65535.0))
                                  for value in struct.unpack_from("<4f", converted,
                                                                  base + weight_offset))
        if index_bytes == 1:
            after_bones = struct.unpack_from("<4B", converted, base + index_offset)
        else:
            after_bones = struct.unpack_from("<4I", converted, base + index_offset)
        before = plan[vertex]
        for slot in range(4):
            _require(after_weights[slot] == before[slot][0], "SKIN_WEIGHT_CHANGED",
                     f"vertex {vertex} slot {slot}: weight {before[slot][0]} became "
                     f"{after_weights[slot]}")
            if before[slot][0] == 0:
                continue
            name = palette[after_bones[slot]]["name"]
            _require(name == palette[before[slot][1]]["name"], "SKIN_BONE_CHANGED",
                     f"vertex {vertex} slot {slot}: bone {palette[before[slot][1]]['name']} "
                     f"became {name}")

    new_payloads = list(payloads)
    new_payloads.append(bytes(converted))
    new_payload_id = len(new_payloads) - 1

    new_manifest = json.loads(json.dumps(manifest))
    for member in group:
        replacement = dict(new_manifest["meshes"][member])
        streams = [dict(stream) for stream in replacement["streams"]]
        streams[stream_position]["payload"] = new_payload_id
        streams[stream_position]["stride"] = target_stride
        replacement["streams"] = streams
        replacement["bones"] = [dict(bone) for bone in palette]
        attributes = [list(attribute) for attribute in replacement["attributes"]]
        attributes = [attribute for attribute in attributes
                      if attribute[0] not in (SEM_BLENDWEIGHTS, SEM_BLENDINDICES)]
        weight_format = 4 if weight_bytes == 2 else 0
        index_format = 6 if index_bytes == 1 else 10
        attributes.append([SEM_BLENDWEIGHTS, weight_format, 4, stream_position, weight_offset])
        attributes.append([SEM_BLENDINDICES, index_format, 4, stream_position, index_offset])
        replacement["attributes"] = attributes
        new_manifest["meshes"][member] = replacement

    new_manifest, new_payloads, pruned = prune_payloads(new_manifest, new_payloads)
    document = ({"manifest": new_manifest, "payload_files": []} if is_project
                else new_manifest)
    output_dir = Path(output_dir)
    _require(Path(project_path).resolve() != output_dir.resolve(), "PROJECT_WOULD_OVERWRITE",
             "output directory must differ from the input project directory")
    output_dir.mkdir(parents=True, exist_ok=True)
    project = _write_project(output_dir, document, is_project, new_payloads)

    return {
        "ok": True,
        "output": str(output_dir),
        "project": str(project),
        "mesh": mesh_index,
        "converted_meshes": group,
        "source_layout": kinds[mesh_index],
        "source_encoding": source_kind,
        "source_stride": source_stride,
        "target_stride": target_stride,
        "vertices": vertex_count,
        "referenced_vertices": len(used),
        "palette_size": len(palette),
        "palette_bones": [bone["name"] for bone in palette],
        "global_bones_used": len(local),
        "payload_pruning": pruned,
        "weights_verified": True,
        "evidence": "exact",
        "note": ("every referenced vertex kept its exact weight and bone name; only the "
                 "encoding changed, and the global bone ids were translated into a "
                 "mesh-local palette"),
    }


# ---------------------------------------------------------------------------
# 10. Texture overrides: duplicate identities, slot writes, slot clearing
# ---------------------------------------------------------------------------
# ``component_auto.texture_table`` walks every ``TextureOverride*`` section that
# is not a base entry or a ``_LODn`` variant, keys it by its ``hash``, and
# raises ``TEXTURE_MAPPING: 重复贴图覆盖`` the moment two sections claim the same
# identity. It then accepts only the static ``this = Resource`` shape.
#
# Two more shapes appear in real packages and neither is a syntax error:
#
#   * per-draw slot writes  ``ps-t0 = ResourceBody`` — the override applies to a
#     specific draw rather than replacing the component's texture;
#   * slot clearing         ``ps-t18 = null`` — the draw must bind *nothing* in
#     that slot, which BEM 1.5 expresses with ``texture_slots``.
#
# This command reports all of it and writes nothing. Duplicate identities are
# the one case that cannot be repaired by rewriting: two sections claiming one
# hash means the source itself is ambiguous about which texture wins, so the
# caller has to choose.

TEXTURE_SLOT_LINE = re.compile(r"(ps-t\d+)\s*=\s*(.+)$")
TEXTURE_NULL = re.compile(r"(?:null|0|none)\s*$", re.I)
TEXTURE_LOD_VARIANT = re.compile(r"TextureOverride_(?:EntryPoint_)?Component\d+_LOD\d+$")


def audit_textures(ini_path: Path | str) -> Dict[str, Any]:
    """Classify every texture override and find duplicate identities."""
    ini_path = Path(ini_path)
    _require(ini_path.is_file(), "INI_NOT_FOUND", f"not a file: {ini_path}")
    text = ini_path.read_text(encoding="utf-8-sig", errors="replace")
    _preamble, blocks = split_blocks(text)

    entries, overrides = [], []
    by_hash: Dict[str, List[str]] = {}
    for block in blocks:
        name = block["name"]
        if not name.startswith("TextureOverride"):
            continue
        lines = _entry_lines(block["body"])
        identity = next((match[1].lower() for match in
                         (re.match(r"hash\s*=\s*([0-9a-fA-F]{8})$", line) for line in lines)
                         if match), None)
        if ENTRY_SECTION.fullmatch(name):
            entries.append({"section": name,
                            "component": int(ENTRY_SECTION.fullmatch(name)[1]),
                            "hash": identity})
            continue
        if TEXTURE_LOD_VARIANT.fullmatch(name):
            continue

        slots: List[str] = []
        slot_lines: List[str] = []
        null_slots: List[str] = []
        static: List[str] = []
        unsupported: List[str] = []
        for line in lines:
            if TEXTURE_HEADER.match(line):
                continue
            slot = TEXTURE_SLOT_LINE.match(line)
            if slot:
                slots.append(slot[1])
                slot_lines.append(line)
                if TEXTURE_NULL.match(slot[2].strip()):
                    null_slots.append(slot[1])
                continue
            if TEXTURE_THIS.match(line) or line.startswith(("if ", "elif ", "else", "endif")):
                static.append(line)
                continue
            unsupported.append(line)

        if unsupported:
            kind = "unsupported"
        elif slots:
            kind = "per_draw"
        elif static:
            kind = "static"
        else:
            kind = "empty"
        # Reproduce the verdict ``component_auto.texture_table`` would reach, so the
        # report names the same failure the build would hit instead of a new dialect.
        if not identity:
            auto_error = "TEXTURE_MAPPING: 缺少贴图身份"
        elif unsupported:
            auto_error = f"TEXTURE_STATE: 不支持的贴图语句 {unsupported[0]}"
        elif slots:
            auto_error = f"TEXTURE_STATE: 不支持的贴图语句 {slot_lines[0]}"
        elif not static:
            auto_error = f"TEXTURE_STATE: 不完整的贴图覆盖 {name}"
        else:
            auto_error = None
        row = {
            "section": name,
            "hash": identity,
            "kind": kind,
            "statements": len(lines),
            "slots": slots,
            "null_slots": null_slots,
            "static_statements": static,
            "unsupported_statements": unsupported,
            "auto_error": auto_error,
        }
        overrides.append(row)
        if identity:
            by_hash.setdefault(identity, []).append(name)

    duplicates = [{"hash": identity, "sections": sections}
                  for identity, sections in by_hash.items() if len(sections) > 1]
    no_identity = [row["section"] for row in overrides if not row["hash"]]
    per_draw = [row for row in overrides if row["kind"] == "per_draw"]
    unsupported = [row for row in overrides if row["kind"] == "unsupported"]
    cleared = sorted({slot for row in per_draw for slot in row["null_slots"]})

    # Two different kinds of bad news. A duplicate identity is *unrepairable by
    # rewriting* — the source is ambiguous, so the caller must choose. Every other
    # finding is a statement the auto path cannot express but that a human can
    # still carry across verbatim; it must be surfaced without blocking the build.
    blockers = [f"TEXTURE_MAPPING: 重复贴图覆盖 {item['hash']}" for item in duplicates]
    blockers += [f"TEXTURE_MAPPING: 缺少贴图身份 {name}" for name in no_identity]
    manual_review = [{"section": row["section"], "error": row["auto_error"],
                      "statements": row["unsupported_statements"]}
                     for row in unsupported]

    return {
        "ok": not blockers,
        "blockers": blockers,
        "manual_review": manual_review,
        "ini": str(ini_path),
        "sections": len(blocks),
        "entries": entries,
        "texture_overrides": overrides,
        "duplicate_hashes": duplicates,
        "overrides_without_identity": no_identity,
        "convertible_static": sum(1 for row in overrides if row["kind"] == "static"),
        "per_draw_overrides": len(per_draw),
        "per_draw_sections": [row["section"] for row in per_draw],
        "cleared_slots": cleared,
        "unsupported": [{"section": row["section"],
                         "statements": row["unsupported_statements"]}
                        for row in unsupported],
        "evidence": "exact",
        "note": ("a duplicate hash is not repairable by rewriting: two sections claiming "
                 "one identity means the source is ambiguous about which texture wins"
                 if duplicates else
                 "no duplicate identities; per-draw slot writes are reported, not merged"),
    }

# ---------------------------------------------------------------------------
# 11. Catalog coverage: which entry hashes the character catalogs do not know
# ---------------------------------------------------------------------------
# ``component_auto.match_catalog`` accepts a package only when *every* base
# ``ComponentN`` hash is present in one and the same catalog's ``entries``.
# One unknown hash therefore sinks the whole package, and the error it raises
# ("没有覆盖这些资源身份的角色资料") does not say *which* hash was unknown. This
# command answers that: it lists the missing identities, whether any catalog
# knows them at all, and which profiles are the closest fits.
#
# The distinction matters for triage:
#
#   * a hash no catalog knows  -> the identity join itself is missing; the pack
#     is probably a character the catalogs have not covered yet, or a retexture
#     whose entry hash changed with the game version;
#   * a hash another catalog knows -> the package mixes components from more
#     than one character, which no single catalog can ever satisfy.
#
# Report only; the catalogs are read, never written.

CATALOG_STATUS_BLOCKED = "source-resource-identities-recheck-required"
# Shared reference tables that live beside the per-character catalogs and must
# not be mistaken for one.
CATALOG_REFERENCE_KINDS = {"bem-material-attributes"}

# Four entry families appear in real EFMI exports. Only the first two have a
# reader in the toolchain (``component_auto.BASE`` and ``efmi_lod_source.ENTRY``);
# the bare ``TextureOverride_<hash>_<count>_<first>`` form has none, and the
# CrossIB ``TextureOverride_IB_<hash>_<label>_ComponentN`` form is only known to
# ``bem_triage`` as a redirection, so neither can be lowered even with every
# identity resolved.
ENTRY_FAMILY_COMPONENT = re.compile(r"TextureOverride_(?:EntryPoint_)?Component(\d+)$")
ENTRY_FAMILY_LOD = re.compile(r"TextureOverride_(?:EntryPoint_)?LOD(\d+)\.([0-9a-fA-F]{8})_(\d+)_(\d+)$")
ENTRY_FAMILY_BARE = re.compile(r"TextureOverride_([0-9a-fA-F]{8})_(\d+)_(\d+)$")
ENTRY_FAMILY_IB = re.compile(r"TextureOverride_IB_([0-9a-fA-F]{8})_(.+?)_Component(\d+)$")
ENTRY_FAMILY_SUPPORTED = {"component-n", "hash-lod"}


def _load_catalogs(catalog_dir: Path) -> List[Dict[str, Any]]:
    """Read every ``*.json``; unusable files are kept, with a reason.

    Files whose ``kind`` marks them as shared reference data rather than a
    per-character catalog are skipped: they are not candidates for matching and
    reporting them as "不是角色资料" would look like a broken catalog.
    """
    loaded: List[Dict[str, Any]] = []
    for path in sorted(catalog_dir.glob("*.json")):
        try:
            data = json.loads(path.read_text(encoding="utf-8-sig"))
        except (OSError, json.JSONDecodeError) as error:
            loaded.append({"path": str(path), "usable": False,
                           "reason": f"不可读: {error}"})
            continue
        if not isinstance(data, dict):
            loaded.append({"path": str(path), "usable": False, "reason": "不是对象"})
            continue
        if data.get("kind") in CATALOG_REFERENCE_KINDS:
            continue
        reason = None
        if data.get("schema") != 1 or data.get("kind") != "bem-character-catalog":
            reason = "不是角色资料"
        elif not data.get("verified"):
            reason = "未标记 verified"
        elif data.get("platform") != "windows-x64":
            reason = f"平台为 {data.get('platform')}"
        elif data.get("mapping_status") == CATALOG_STATUS_BLOCKED:
            reason = "资料标记为待重新核对"
        loaded.append({
            "path": str(path),
            "usable": reason is None,
            "reason": reason,
            "character_id": data.get("character_id"),
            "name": data.get("name"),
            "mapping_status": data.get("mapping_status"),
            "entries": {str(item).lower() for item in data.get("entries") or []},
        })
    return loaded


def catalog_gap(ini_path: Path | str, catalog_dir: Path | str | None = None) -> Dict[str, Any]:
    """Diff a package's base entry hashes against every character catalog."""
    ini_path = Path(ini_path)
    _require(ini_path.is_file(), "INI_NOT_FOUND", f"not a file: {ini_path}")

    if catalog_dir is None:
        root = _tools_root()
        _require(root is not None, "TOOLS_ROOT_NOT_FOUND",
                 "找不到 CLI 模块目录，无法定位 catalog/")
        catalog_dir = root / "catalog"
    catalog_dir = Path(catalog_dir)
    _require(catalog_dir.is_dir(), "CATALOG_DIR_NOT_FOUND", f"not a directory: {catalog_dir}")
    catalogs = _load_catalogs(catalog_dir)
    usable = [item for item in catalogs if item["usable"]]
    _require(bool(usable), "CATALOG_EMPTY",
             f"{catalog_dir} 下没有可用角色资料（schema/kind/verified/platform 均需通过）")

    text = ini_path.read_text(encoding="utf-8-sig", errors="replace")
    _preamble, blocks = split_blocks(text)

    entries: List[Dict[str, Any]] = []
    for block in blocks:
        name = block["name"]
        component = ENTRY_FAMILY_COMPONENT.fullmatch(name)
        lod = ENTRY_FAMILY_LOD.fullmatch(name)
        bare = ENTRY_FAMILY_BARE.fullmatch(name)
        ib = None if (component or lod or bare) else ENTRY_FAMILY_IB.fullmatch(name)
        if not (component or lod or bare or ib):
            continue
        lines = _entry_lines(block["body"])
        declared = next((hit[1].lower() for hit in
                         (re.match(r"hash\s*=\s*([0-9a-fA-F]{8})$", line) for line in lines)
                         if hit), None)
        first = next((hit[1] for hit in
                      (re.match(r"match_first_index\s*=\s*(\d+)$", line) for line in lines)
                      if hit), None)
        if component:
            family, identity = "component-n", declared
            index = int(component[1])
            tag = None
        elif lod:
            family, identity = "hash-lod", lod[2].lower()
            index = None
            tag = None
        elif bare:
            family, identity = "bare-hash", bare[1].lower()
            index = None
            tag = None
        else:
            # CrossIB: the 8-hex in the name is the *resource group* the buffers
            # were exported under, not the draw hash — the body's ``hash =`` is
            # what the game matches on. The same component index repeats once per
            # part, so the index alone does not identify the entry.
            family, identity = "ib-component", declared
            index = int(ib[3])
            tag = ib[1].lower()
        # For the families whose name carries the draw hash, the body has to
        # agree; a mismatch means the identity is not trustworthy either way.
        if family != "ib-component" and identity and declared and identity != declared:
            raise Refused("SOURCE_IDENTITY",
                          f"{name}: 段名 hash {identity} 与 hash = {declared} 不一致")
        entries.append({"section": name,
                        "family": family,
                        "component": index,
                        "hash": identity or declared,
                        "resource_tag": tag,
                        "first_index": int(first) if first is not None else None})
    _require(bool(entries) or any(b["name"].startswith("TextureOverride") for b in blocks),
             "SOURCE_IDENTITY",
             "没有任何 TextureOverride 段；请确认这是入口 INI 而不是伴随 INI")
    if not entries:
        # TextureOverride sections exist but none of them is an entry this
        # toolchain can read. Reporting the observed shapes is the whole value
        # here: it says *how* the export is non-standard instead of just failing.
        shapes: Dict[str, int] = {}
        for block in blocks:
            if not block["name"].startswith("TextureOverride"):
                continue
            shape = re.sub(r"[0-9a-fA-F]{8}", "<hash>", block["name"])
            shape = re.sub(r"\d+", "<n>", shape)
            shapes[shape] = shapes.get(shape, 0) + 1
        return {
            "ok": False,
            "verdict": "unrecognized-entry",
            "catalog_required": None,
            "blockers": ["ENTRY_FAMILY: 没有可识别的入口段"
                         "（ComponentN / LODn.<hash> / <hash>_<count>_<first>）"],
            "ini": str(ini_path),
            "catalog_dir": str(catalog_dir),
            "entry_family": None,
            "entry_count": 0,
            "entries": [],
            "observed_shapes": dict(sorted(shapes.items(), key=lambda kv: -kv[1])),
            "matching_profiles": [],
            "best_profile": None,
            "best_missing": [],
            "unknown_identities": [],
            "identities_present_elsewhere": {},
            "candidates": [],
            "unusable_catalogs": [{"path": item["path"], "reason": item["reason"]}
                                  for item in catalogs if not item["usable"]],
            "evidence": "exact",
            "note": "入口段命名不属于任何已知族；先按 observed_shapes 判断是否需要新增读取器",
        }
    # Only the ComponentN family is keyed by index alone; a CrossIB export
    # repeats the same index once per part, so the check would false-positive.
    components = [row["component"] for row in entries
                  if row["family"] == "component-n" and row["component"] is not None]
    _require(len(set(components)) == len(components), "SOURCE_IDENTITY",
             f"重复组件入口: {sorted({c for c in components if components.count(c) > 1})}")
    bad = [row["section"] for row in entries if not row["hash"]]
    _require(not bad, "SOURCE_IDENTITY", f"缺少有效 hash: {', '.join(bad)}")

    families = {row["family"] for row in entries}
    family = families.pop() if len(families) == 1 else "mixed"

    hashes = [row["hash"] for row in entries]
    blockers = [f"SUBMESH_MAPPING: {row['section']} 非零 first_index "
                f"{row['first_index']} 需要专门映射"
                for row in entries if row["first_index"] not in (None, 0)]
    if family not in ENTRY_FAMILY_SUPPORTED:
        blockers.append(
            f"ENTRY_FAMILY: {family} 入口段没有对应的读取器"
            "（component_auto.BASE 只认 ComponentN，efmi_lod_source.ENTRY 只认 LODn.<hash>）；"
            "即使身份齐全也无法降低为 BEM")

    candidates = []
    for item in usable:
        covered = [identity for identity in hashes if identity in item["entries"]]
        candidates.append({
            "character_id": item["character_id"],
            "name": item["name"],
            "mapping_status": item["mapping_status"],
            "covered": len(covered),
            "missing": [identity for identity in hashes if identity not in item["entries"]],
        })
    # Best fit first; ties broken by name so the report is stable run to run.
    candidates.sort(key=lambda row: (-row["covered"], row["name"] or ""))

    full = [row for row in candidates if not row["missing"]]
    best = candidates[0]
    known_somewhere = {identity for item in usable for identity in item["entries"]}
    unknown = [identity for identity in hashes if identity not in known_somewhere]

    present_in: Dict[str, List[str]] = {}
    for identity in hashes:
        owners = [item["character_id"] for item in usable if identity in item["entries"]]
        if owners:
            present_in[identity] = owners

    # ``match_catalog`` demands exactly one covering profile; two is as fatal as none.
    # Only families whose identity is a real draw hash can be matched at all: a
    # hash-named entry carries its own index count and is lowered without a
    # catalog, so a gap there is informational rather than blocking.
    catalog_required = any(row["family"] in ("component-n", "ib-component")
                           for row in entries)
    stray = [identity for identity in best["missing"] if identity in known_somewhere]
    missing = best["missing"]
    verdict = ("not-required" if not catalog_required else
               "matched" if len(full) == 1 else
               "ambiguous-profile" if len(full) > 1 else
               "mixed-characters" if stray and len(stray) == len(missing) else
               "unknown-identity" if missing and len(unknown) == len(missing) else
               "partial")

    return {
        "ok": not blockers and (len(full) == 1 if catalog_required else True),
        "verdict": verdict,
        "catalog_required": catalog_required,
        "blockers": blockers,
        "ini": str(ini_path),
        "catalog_dir": str(catalog_dir),
        "entry_family": family,
        "entry_count": len(entries),
        "entries": entries,
        "matching_profiles": [row["character_id"] for row in full],
        "best_profile": best["character_id"],
        "best_missing": best["missing"],
        "unknown_identities": unknown,
        "identities_present_elsewhere": {identity: present_in[identity]
                                         for identity in best["missing"] if identity in present_in},
        "candidates": candidates,
        "unusable_catalogs": [{"path": item["path"], "reason": item["reason"]}
                              for item in catalogs if not item["usable"]],
        "evidence": "exact",
        "note": {
            "not-required": "入口段以 hash 命名，自带 index count，降低流程不查角色资料；"
                            "下面的覆盖情况仅供参考，缺口不阻断转换",
            "matched": "有且仅有一个角色资料覆盖全部入口身份时才能自动转换",
            "ambiguous-profile": "多个角色资料同时覆盖全部入口身份：match_catalog 会以"
                                 "'角色/资源版本匹配不唯一' 拒绝，需要核对是否资料重复",
            "mixed-characters": "缺口身份存在于其它角色资料中：该包混用了多个角色的部件，"
                                "任何单一资料都无法满足，需要拆包或手工映射",
            "unknown-identity": "缺口身份在所有角色资料中都不存在：需先补齐原资源与 EFMI hash 的对应",
            "partial": "部分缺口身份无人认识、部分属于其它角色：先补身份对应，再判断是否混包",
        }[verdict],
    }


# ---------------------------------------------------------------------------
# 12. Body parameters: `parameters`, `mesh_deformations`, delta payloads
# ---------------------------------------------------------------------------
# ``bem_v13.validate_manifest`` raises on the first violation, which is right for
# a builder and wrong for triage: a broken 4096-channel package would report one
# problem at a time. This command walks the same rules and collects every
# violation with a stable code, then decodes each delta payload far enough to
# prove the ``4 + J + 16N`` contract. Report only.

DEFORM_ENCODING = "sparse-position-f32"
DEFORM_RECORD = struct.Struct("<I3f")
MAX_DEFORM_PARAMETERS, MAX_DEFORM_CHANNELS, MAX_DEFORM_FRAMES = 64, 4096, 64
DEFORM_CAPABILITIES = {"body-parameters", "mesh-position-deltas"}
CONDITION_DEPTH = 16
CONDITION_FANOUT = 32


def _condition_groups(node: Any, groups: Dict[str, set], where: str,
                      bad: Callable[..., None], prefix: str, depth: int = 0) -> List[str]:
    """Validate one condition node and return the group ids it reads, in order.

    Shared by ``deformation-audit`` (``available_when``) and ``option-audit``
    (``when`` / ``selection_constraints``) so both report the same grammar
    errors under the same codes.
    """
    if depth > CONDITION_DEPTH:
        bad(f"{prefix}_CONDITION_DEPTH", where, f"条件深度超过 {CONDITION_DEPTH}")
        return []
    if isinstance(node, bool):
        return []
    if not isinstance(node, dict) or len(node) != 1:
        bad(f"{prefix}_CONDITION_SHAPE", where,
            f"条件必须是 true/false 或单键对象，实际是 {node!r}")
        return []
    key, value = next(iter(node.items()))
    if key == "eq":
        if (not isinstance(value, list) or len(value) != 2
                or not all(isinstance(item, str) for item in value)):
            bad(f"{prefix}_CONDITION_SHAPE", where, f"eq 必须是两个字符串，实际是 {value!r}")
            return []
        group, choice = value
        if group not in groups:
            bad(f"{prefix}_CONDITION_UNKNOWN_GROUP", where, f"条件引用了未声明的选项组 {group!r}")
            return []
        if choice not in groups[group]:
            bad(f"{prefix}_CONDITION_UNKNOWN_CHOICE", where,
                f"条件引用了 {group} 中不存在的选项 {choice!r}")
        return [group]
    if key in ("all", "any"):
        if not isinstance(value, list) or not 1 <= len(value) <= CONDITION_FANOUT:
            bad(f"{prefix}_CONDITION_FANOUT", where,
                f"{key} 需要 1..{CONDITION_FANOUT} 项，实际是 {value!r}")
            return []
        found: List[str] = []
        for index, child in enumerate(value):
            for group in _condition_groups(child, groups, f"{where}.{key}[{index}]",
                                           bad, prefix, depth + 1):
                if group not in found:
                    found.append(group)
        return found
    if key == "not":
        return _condition_groups(value, groups, f"{where}.not", bad, prefix, depth + 1)
    bad(f"{prefix}_CONDITION_SHAPE", where, f"未知条件键 {key!r}")
    return []


def _deform_payload(payload: bytes, count: int, vertex_count: int,
                    where: str, bad: Callable[..., None]) -> Dict[str, Any]:
    """Decode one delta payload and prove the documented length contract."""
    if len(payload) < 4:
        bad("DEFORM_PAYLOAD_SIZE", where, f"payload 只有 {len(payload)} 字节，不足 4 字节长度头")
        return {"bytes": len(payload), "records": 0}
    size = struct.unpack_from("<I", payload)[0]
    if not 0 < size <= 4096 or size > len(payload) - 4:
        bad("DEFORM_PAYLOAD_HEADER", where, f"长度头 J={size} 越界（payload {len(payload)} 字节）")
        return {"bytes": len(payload), "records": 0}
    try:
        header = json.loads(payload[4:4 + size].decode("utf-8"))
    except (UnicodeDecodeError, json.JSONDecodeError) as error:
        bad("DEFORM_PAYLOAD_HEADER", where, f"长度头不是合法 JSON: {error}")
        return {"bytes": len(payload), "records": 0}
    if not isinstance(header, dict) or set(header) != {"count", "encoding"}:
        bad("DEFORM_PAYLOAD_HEADER", where,
            f"长度头字段必须是 count/encoding，实际是 {sorted(header) if isinstance(header, dict) else header!r}")
        return {"bytes": len(payload), "records": 0}
    if header["encoding"] != DEFORM_ENCODING:
        bad("DEFORM_PAYLOAD_HEADER", where, f"长度头 encoding 是 {header['encoding']!r}")
    if type(header["count"]) is not int or header["count"] != count:
        bad("DEFORM_PAYLOAD_HEADER", where,
            f"长度头 count={header['count']!r} 与帧上的 count={count} 不一致")
    expected = 4 + size + count * DEFORM_RECORD.size
    if len(payload) != expected:
        bad("DEFORM_PAYLOAD_SIZE", where,
            f"payload 长度 {len(payload)} != 4+{size}+16x{count} = {expected}")

    seen, nonfinite = set(), 0
    body = payload[4 + size:4 + size + count * DEFORM_RECORD.size]
    for record in DEFORM_RECORD.iter_unpack(body):
        if record[0] >= vertex_count:
            bad("DEFORM_PAYLOAD_INDEX", where, f"顶点索引 {record[0]} 超出 {vertex_count}")
        if record[0] in seen:
            bad("DEFORM_PAYLOAD_INDEX", where, f"顶点索引 {record[0]} 在同一帧内重复")
        seen.add(record[0])
        if not all(math.isfinite(value) for value in record[1:]):
            nonfinite += 1
    if nonfinite:
        bad("DEFORM_PAYLOAD_RECORD", where, f"{nonfinite} 条记录的增量不是有限值")
    return {"bytes": len(payload), "records": len(seen)}


def deformation_audit(project_path: Path | str,
                      minor: Optional[int] = None) -> Dict[str, Any]:
    """Validate body parameters, deformation channels and their delta payloads."""
    data, is_project = load_project(project_path)
    manifest = data["manifest"] if is_project else data
    payloads = load_payloads(project_path, data, is_project)

    parameters = manifest.get("parameters") or []
    channels = manifest.get("mesh_deformations") or []
    if not parameters and not channels:
        return {"ok": True, "applicable": False, "project": str(project_path),
                "parameters": 0, "channels": 0, "findings": [],
                "evidence": "exact",
                "note": "包里没有 parameters/mesh_deformations，无需审计"}

    findings: List[Dict[str, Any]] = []

    def bad(code: str, where: str, message: str) -> None:
        findings.append({"code": code, "where": where, "message": message})

    header_minor = minor if minor is not None else data.get("minor") if is_project else None
    if header_minor is not None and header_minor not in (3, 4, 5):
        bad("DEFORM_VERSION", "header",
            f"header minor={header_minor}，形态参数需要 1.3 或更高")

    capabilities = set(manifest.get("required_capabilities") or [])
    for capability in sorted(DEFORM_CAPABILITIES):
        if capability not in capabilities:
            bad("DEFORM_CAPABILITY", "required_capabilities",
                f"缺少能力声明 {capability}")

    # -- option groups, only as far as conditions need them ----------------
    groups: Dict[str, set] = {}
    group_order: List[str] = []
    for index, group in enumerate(manifest.get("option_groups") or []):
        if not isinstance(group, dict) or "id" not in group:
            continue
        choices = {choice.get("id") for choice in group.get("choices") or []
                   if isinstance(choice, dict)}
        groups[group["id"]] = choices
        group_order.append(group["id"])

    # -- parameters ---------------------------------------------------------
    if not isinstance(parameters, list):
        bad("DEFORM_PARAMETER_FIELDS", "parameters", "parameters 必须是数组")
        parameters = []
    if len(parameters) > MAX_DEFORM_PARAMETERS:
        bad("DEFORM_PARAMETER_COUNT", "parameters",
            f"{len(parameters)} 个参数，上限 {MAX_DEFORM_PARAMETERS}")
    declared: Dict[str, Dict[str, Any]] = {}
    allowed = {"id", "name", "min", "max", "neutral", "default", "step", "available_when"}
    for index, parameter in enumerate(parameters):
        where = f"parameters[{index}]"
        if not isinstance(parameter, dict):
            bad("DEFORM_PARAMETER_FIELDS", where, "参数必须是对象")
            continue
        extra = set(parameter) - allowed
        if extra:
            bad("DEFORM_PARAMETER_FIELDS", where, f"未知字段 {sorted(extra)}")
        missing = allowed - {"available_when"} - set(parameter)
        if missing:
            bad("DEFORM_PARAMETER_FIELDS", where, f"缺少字段 {sorted(missing)}")
            continue
        pid = parameter["id"]
        if not isinstance(pid, str) or not pid:
            bad("DEFORM_PARAMETER_FIELDS", where, "id 必须是非空字符串")
            continue
        if pid in declared:
            bad("DEFORM_PARAMETER_DUPLICATE", where, f"参数 id {pid!r} 重复")
            continue
        name = parameter["name"]
        if not isinstance(name, str) or not 0 < len(name.encode("utf-8")) <= 256:
            bad("DEFORM_PARAMETER_FIELDS", where, f"name 非法: {name!r}")
        ticks = {key: parameter[key] for key in ("min", "max", "neutral", "default", "step")}
        if not all(type(value) is int for value in ticks.values()):
            bad("DEFORM_PARAMETER_RANGE", where, f"刻度必须都是整数: {ticks}")
            declared[pid] = parameter
            continue
        if not (0 <= ticks["min"] < ticks["max"] <= 1000):
            bad("DEFORM_PARAMETER_RANGE", where,
                f"min={ticks['min']} max={ticks['max']} 需满足 0<=min<max<=1000")
        if not 1 <= ticks["step"] <= 1000:
            bad("DEFORM_PARAMETER_RANGE", where, f"step={ticks['step']} 需在 1..1000")
        for key in ("max", "neutral", "default"):
            value = ticks[key]
            if not ticks["min"] <= value <= ticks["max"]:
                bad("DEFORM_PARAMETER_ALIGNMENT", where,
                    f"{key}={value} 不在 [{ticks['min']}, {ticks['max']}] 内")
            elif ticks["step"] and (value - ticks["min"]) % ticks["step"]:
                bad("DEFORM_PARAMETER_ALIGNMENT", where,
                    f"{key}={value} 未按 step={ticks['step']} 从 {ticks['min']} 对齐")
        if "available_when" in parameter:
            # Only discrete option groups may appear here; a slider id is not a
            # group, so ``_condition_groups`` already rejects it by name.
            _condition_groups(parameter["available_when"], groups, where, bad, "DEFORM")
        declared[pid] = parameter

    # -- channels -----------------------------------------------------------
    meshes = manifest.get("meshes") or []
    if not isinstance(channels, list):
        bad("DEFORM_CHANNEL_FIELDS", "mesh_deformations", "mesh_deformations 必须是数组")
        channels = []
    if len(channels) > MAX_DEFORM_CHANNELS:
        bad("DEFORM_CHANNEL_COUNT", "mesh_deformations",
            f"{len(channels)} 个通道，上限 {MAX_DEFORM_CHANNELS}")
    seen_pairs: set = set()
    for index, channel in enumerate(channels):
        where = f"mesh_deformations[{index}]"
        if not isinstance(channel, dict) or set(channel) != {"mesh", "parameter", "frames"}:
            bad("DEFORM_CHANNEL_FIELDS", where,
                f"通道字段必须恰好是 mesh/parameter/frames，实际是 "
                f"{sorted(channel) if isinstance(channel, dict) else channel!r}")
            continue
        mid, pid = channel["mesh"], channel["parameter"]
        if type(mid) is not int or not 0 <= mid < len(meshes):
            bad("DEFORM_CHANNEL_TARGET", where, f"mesh={mid!r} 不是 0..{len(meshes) - 1} 的网格下标")
            continue
        parameter = declared.get(pid)
        if parameter is None:
            bad("DEFORM_CHANNEL_TARGET", where, f"parameter={pid!r} 未在 parameters 中声明")
            continue
        if (mid, pid) in seen_pairs:
            bad("DEFORM_CHANNEL_DUPLICATE", where, f"(mesh {mid}, {pid!r}) 通道重复")
        seen_pairs.add((mid, pid))
        mesh = meshes[mid]
        attributes = mesh.get("attributes") or []
        position = [item for item in attributes if item[0] == 0]
        if len(position) != 1 or list(position[0][1:3]) != [0, 3]:
            bad("DEFORM_POSITION_ATTRIBUTE", where,
                f"mesh {mid} 的 semantic 0 属性不是唯一的 Float32 XYZ"
                f"（实际 {[list(item) for item in position]}）")

        frames = channel["frames"]
        if not isinstance(frames, list) or not 2 <= len(frames) <= MAX_DEFORM_FRAMES:
            bad("DEFORM_FRAME_COUNT", where,
                f"帧数需在 2..{MAX_DEFORM_FRAMES}，实际是 "
                f"{len(frames) if isinstance(frames, list) else frames!r}")
            continue
        ticks: List[int] = []
        neutral_count = 0
        for position_index, frame in enumerate(frames):
            frame_where = f"{where}.frames[{position_index}]"
            if not isinstance(frame, dict) or type(frame.get("value")) is not int:
                bad("DEFORM_FRAME_FIELDS", frame_where, f"帧必须有整数 value: {frame!r}")
                continue
            value = frame["value"]
            ticks.append(value)
            if not parameter["min"] <= value <= parameter["max"]:
                bad("DEFORM_FRAME_TICK", frame_where,
                    f"value={value} 不在 [{parameter['min']}, {parameter['max']}] 内")
            elif parameter["step"] and (value - parameter["min"]) % parameter["step"]:
                bad("DEFORM_FRAME_TICK", frame_where,
                    f"value={value} 未按 step={parameter['step']} 从 {parameter['min']} 对齐")
            if "neutral" in frame:
                neutral_count += 1
                if set(frame) != {"value", "neutral"} or frame["neutral"] is not True:
                    bad("DEFORM_FRAME_NEUTRAL", frame_where,
                        f"中性帧只能有 value 与 neutral=true: {frame!r}")
                if value != parameter["neutral"]:
                    bad("DEFORM_FRAME_NEUTRAL", frame_where,
                        f"中性帧 value={value} 必须等于 neutral={parameter['neutral']}")
                continue
            if set(frame) != {"value", "payload", "count", "encoding"}:
                bad("DEFORM_FRAME_FIELDS", frame_where,
                    f"非中性帧字段必须是 value/payload/count/encoding，实际是 {sorted(frame)}")
                continue
            if frame["encoding"] != DEFORM_ENCODING:
                bad("DEFORM_FRAME_FIELDS", frame_where,
                    f"encoding 必须是 {DEFORM_ENCODING}，实际是 {frame['encoding']!r}")
            if value == parameter["neutral"]:
                bad("DEFORM_FRAME_FIELDS", frame_where, "非中性帧不能落在 neutral 刻度上")
            payload_index, count = frame["payload"], frame["count"]
            if type(payload_index) is not int or not 0 <= payload_index < len(payloads):
                bad("DEFORM_PAYLOAD_INDEX", frame_where,
                    f"payload={payload_index!r} 不是 0..{len(payloads) - 1} 的载荷下标")
                continue
            vertex_count = int(mesh.get("vertex_count") or 0)
            if type(count) is not int or not 0 <= count <= vertex_count:
                bad("DEFORM_PAYLOAD_INDEX", frame_where,
                    f"count={count!r} 需在 0..{vertex_count}（网格顶点数）")
                continue
            _deform_payload(payloads[payload_index], count, vertex_count,
                            frame_where, bad)
        if len(ticks) == len(frames):
            if ticks != sorted(set(ticks)):
                bad("DEFORM_FRAME_TICK", where, f"帧值必须严格递增，实际是 {ticks}")
            elif ticks[0] != parameter["min"] or ticks[-1] != parameter["max"]:
                bad("DEFORM_FRAME_COVERAGE", where,
                    f"帧值 {ticks[0]}..{ticks[-1]} 未覆盖 "
                    f"[{parameter['min']}, {parameter['max']}]")
        if neutral_count != 1:
            bad("DEFORM_FRAME_NEUTRAL", where, f"需要恰好一个中性帧，实际有 {neutral_count} 个")

    return {
        "ok": not findings,
        "applicable": True,
        "project": str(project_path),
        "header_minor": header_minor,
        "parameters": len(parameters),
        "channels": len(channels),
        "payloads": len(payloads),
        "findings": findings,
        "codes": sorted({item["code"] for item in findings}),
        "evidence": "exact",
        "note": ("逐项复现 bem_v13.validate_manifest 的规则并解出每个增量载荷；"
                 "本命令只报告不写入"),
    }


# ---------------------------------------------------------------------------
# 13. Option groups: structure, state enumeration, per-mesh draw ceiling
# ---------------------------------------------------------------------------
# ``bem_v11.selection_plan`` proves the "exactly one candidate per target" rule
# with a symbolic decision graph, which needs the builder modules (and therefore
# zstandard). This command instead enumerates the legal option combinations and
# evaluates the rules concretely, so it runs anywhere and can name the *state*
# that breaks a rule — a graph says "somewhere", a list says which.
#
# The draw ceiling is per mesh, not the sum over targets: ``bem_v11`` checks
# ``logic.maximum(active, ...) <= 256`` for each selected mesh independently.
# Report only.

MAX_OPTION_GROUPS = 64
MAX_OPTION_STATES = 250000
MAX_SELECTED_DRAWS = 256
MAX_SELECTED_INDICES = 16777216
OPTION_OPERATIONS = {"keep", "hide", "replace"}


def _condition_holds(node: Any, values: Dict[str, Optional[str]]) -> bool:
    """Evaluate a condition under one assignment; ``None`` means unavailable."""
    if isinstance(node, bool):
        return node
    if not isinstance(node, dict) or len(node) != 1:
        return False
    key, value = next(iter(node.items()))
    if key == "eq":
        if not isinstance(value, list) or len(value) != 2:
            return False
        return values.get(value[0]) == value[1]
    if key == "all":
        return isinstance(value, list) and all(_condition_holds(item, values) for item in value)
    if key == "any":
        return isinstance(value, list) and any(_condition_holds(item, values) for item in value)
    if key == "not":
        return not _condition_holds(value, values)
    return False


def option_audit(project_path: Path | str) -> Dict[str, Any]:
    """Enumerate every legal option state and prove the per-state invariants."""
    data, is_project = load_project(project_path)
    manifest = data["manifest"] if is_project else data

    findings: List[Dict[str, Any]] = []

    def bad(code: str, where: str, message: str) -> None:
        findings.append({"code": code, "where": where, "message": message})

    raw_groups = manifest.get("option_groups") or []
    if not isinstance(raw_groups, list):
        bad("OPTION_GROUP_FIELDS", "option_groups", "option_groups 必须是数组")
        raw_groups = []
    if len(raw_groups) > MAX_OPTION_GROUPS:
        bad("OPTION_GROUP_COUNT", "option_groups",
            f"{len(raw_groups)} 个选项组，上限 {MAX_OPTION_GROUPS}")

    groups: Dict[str, Dict[str, Any]] = {}
    order: List[str] = []
    for index, group in enumerate(raw_groups):
        where = f"option_groups[{index}]"
        if not isinstance(group, dict):
            bad("OPTION_GROUP_FIELDS", where, "选项组必须是对象")
            continue
        extra = set(group) - {"id", "name", "default", "choices", "available_when"}
        if extra:
            bad("OPTION_GROUP_FIELDS", where, f"未知字段 {sorted(extra)}")
        gid = group.get("id")
        if not isinstance(gid, str) or not gid:
            bad("OPTION_GROUP_FIELDS", where, f"id 必须是非空字符串: {gid!r}")
            continue
        if gid in groups:
            bad("OPTION_GROUP_DUPLICATE", where, f"选项组 id {gid!r} 重复")
            continue
        name = group.get("name")
        if not isinstance(name, str) or not 0 < len(name.encode("utf-8")) <= 256:
            bad("OPTION_GROUP_FIELDS", where, f"name 非法: {name!r}")
        choices = group.get("choices")
        if not isinstance(choices, list) or not choices:
            bad("OPTION_CHOICES_FIELDS", where, f"choices 必须是非空数组: {choices!r}")
            continue
        choice_ids: List[str] = []
        for choice_index, choice in enumerate(choices):
            choice_where = f"{where}.choices[{choice_index}]"
            if not isinstance(choice, dict) or set(choice) != {"id", "name"}:
                bad("OPTION_CHOICES_FIELDS", choice_where,
                    f"选项字段必须恰好是 id/name: "
                    f"{sorted(choice) if isinstance(choice, dict) else choice!r}")
                continue
            if not isinstance(choice["id"], str) or not choice["id"]:
                bad("OPTION_CHOICES_FIELDS", choice_where, f"选项 id 非法: {choice['id']!r}")
                continue
            if choice["id"] in choice_ids:
                bad("OPTION_CHOICES_DUPLICATE", choice_where, f"选项 id {choice['id']!r} 重复")
                continue
            if not isinstance(choice["name"], str) or not 0 < len(choice["name"].encode("utf-8")) <= 256:
                bad("OPTION_CHOICES_FIELDS", choice_where, f"选项 name 非法: {choice['name']!r}")
            choice_ids.append(choice["id"])
        default = group.get("default")
        if default not in choice_ids:
            bad("OPTION_DEFAULT", where, f"default={default!r} 不在 choices 中")
        groups[gid] = {"id": gid, "choices": choice_ids,
                       "default": default,
                       "has_condition": "available_when" in group,
                       "available_when": group.get("available_when", True)}
        order.append(gid)

    known = {gid: set(groups[gid]["choices"]) for gid in order}
    # ``available_when`` may only read groups declared before it. Checking this
    # after the loop keeps the grammar error and the ordering error separate:
    # a forward reference is well-formed but unreachable.
    for index, gid in enumerate(order):
        if not groups[gid]["has_condition"]:
            continue
        referenced = _condition_groups(groups[gid]["available_when"], known,
                                       f"option_groups[{index}]", bad, "OPTION")
        for other in referenced:
            if order.index(other) >= index:
                bad("OPTION_AVAILABLE_WHEN_ORDER", f"option_groups[{index}]",
                    f"available_when 只能引用排在前面的组，{other!r} 在本组之后")

    constraints = manifest.get("selection_constraints") or []
    if not isinstance(constraints, list):
        bad("OPTION_CONSTRAINT_FIELDS", "selection_constraints", "必须是数组")
        constraints = []
    for index, constraint in enumerate(constraints):
        _condition_groups(constraint, known, f"selection_constraints[{index}]",
                          bad, "OPTION")

    # -- component rules ----------------------------------------------------
    components = (manifest.get("target") or {}).get("components") or []
    rules = manifest.get("component_rules") or []
    meshes = manifest.get("meshes") or []
    if not isinstance(rules, list):
        bad("OPTION_RULE_FIELDS", "component_rules", "component_rules 必须是数组")
        rules = []
    if len(rules) != len(components):
        bad("OPTION_RULE_COVERAGE", "component_rules",
            f"{len(rules)} 条规则对应 {len(components)} 个部件；必须按 target 0..N-1 全覆盖")
    for index, rule in enumerate(rules):
        where = f"component_rules[{index}]"
        if not isinstance(rule, dict) or set(rule) != {"target", "candidates"}:
            bad("OPTION_RULE_FIELDS", where,
                f"规则字段必须恰好是 target/candidates: "
                f"{sorted(rule) if isinstance(rule, dict) else rule!r}")
            continue
        if rule["target"] != index:
            bad("OPTION_RULE_COVERAGE", where,
                f"target={rule['target']!r} 必须按顺序等于下标 {index}")
        candidates = rule["candidates"]
        if not isinstance(candidates, list) or not candidates:
            bad("OPTION_RULE_FIELDS", where, f"candidates 必须是非空数组: {candidates!r}")
            continue
        for candidate_index, candidate in enumerate(candidates):
            candidate_where = f"{where}.candidates[{candidate_index}]"
            if not isinstance(candidate, dict):
                bad("OPTION_RULE_FIELDS", candidate_where, "候选必须是对象")
                continue
            operation = candidate.get("operation")
            if operation not in OPTION_OPERATIONS:
                bad("OPTION_RULE_OPERATION", candidate_where,
                    f"operation={operation!r} 必须是 keep/hide/replace")
                continue
            allowed = {"when", "operation"} | ({"mesh"} if operation == "replace" else set())
            if operation == "keep":
                allowed |= {"material_overrides"}
            extra = set(candidate) - allowed
            if extra:
                bad("OPTION_RULE_FIELDS", candidate_where, f"{operation} 不允许字段 {sorted(extra)}")
            if operation == "replace":
                mesh_index = candidate.get("mesh")
                if type(mesh_index) is not int or not 0 <= mesh_index < len(meshes):
                    bad("OPTION_RULE_TARGET", candidate_where,
                        f"mesh={mesh_index!r} 不是 0..{len(meshes) - 1} 的网格下标")
            _condition_groups(candidate.get("when", True), known, candidate_where,
                              bad, "OPTION")

    # -- state enumeration --------------------------------------------------
    states: List[Dict[str, Optional[str]]] = []
    budget = MAX_OPTION_STATES

    def walk(index: int, values: Dict[str, Optional[str]]) -> None:
        if len(states) >= budget:
            return
        if index == len(order):
            if all(_condition_holds(item, values) for item in constraints):
                states.append(dict(values))
            return
        gid = order[index]
        group = groups[gid]
        if not _condition_holds(group["available_when"], values):
            values[gid] = None
            walk(index + 1, values)
            del values[gid]
            return
        for choice in group["choices"]:
            values[gid] = choice
            walk(index + 1, values)
        del values[gid]

    walk(0, {})
    truncated = len(states) >= budget

    illegal: List[Dict[str, Any]] = []
    illegal_counts: Dict[Tuple[int, int], int] = {}
    over_draws: List[Dict[str, Any]] = []
    worst_draws, worst_indices, worst_state = 0, 0, None
    for state in states:
        picked: Dict[int, Dict[str, Any]] = {}
        for index, rule in enumerate(rules):
            if not isinstance(rule, dict) or not isinstance(rule.get("candidates"), list):
                continue
            hits = [candidate for candidate in rule["candidates"]
                    if isinstance(candidate, dict)
                    and _condition_holds(candidate.get("when", True), state)]
            if len(hits) != 1:
                illegal_counts[(index, len(hits))] = illegal_counts.get((index, len(hits)), 0) + 1
                if len(illegal) < 20:
                    illegal.append({"target": index, "matches": len(hits), "state": state})
                continue
            picked[index] = hits[0]
        draws = indices = 0
        for candidate in picked.values():
            if candidate.get("operation") != "replace":
                continue
            mesh_index = candidate.get("mesh")
            # A malformed candidate was already reported; it must not crash the
            # enumeration, so skip it here instead of indexing blindly.
            if type(mesh_index) is not int or not 0 <= mesh_index < len(meshes):
                continue
            mesh = meshes[mesh_index]
            active = [draw for draw in mesh.get("draws") or []
                      if _condition_holds(draw.get("when", True), state)]
            draws = max(draws, len(active))
            indices = max(indices, sum(int(draw.get("count") or 0) for draw in active))
        if draws > worst_draws:
            worst_draws, worst_state = draws, state
        worst_indices = max(worst_indices, indices)
        if draws > MAX_SELECTED_DRAWS and len(over_draws) < 20:
            over_draws.append({"draws": draws, "state": state})

    for (target, matches), count in sorted(illegal_counts.items()):
        if matches == 0:
            bad("OPTION_RULE_UNCOVERED", f"component_rules[{target}]",
                f"{count} 个合法状态下没有任何候选成立；该部件在那些状态下无法确定")
        else:
            bad("OPTION_RULE_AMBIGUOUS", f"component_rules[{target}]",
                f"{count} 个合法状态下有 {matches} 个候选同时成立；必须恰好一个")

    # Availability depends on the values already chosen, so the default state has
    # to be built in declaration order rather than in one comprehension.
    default_state: Dict[str, Optional[str]] = {}
    for gid in order:
        group = groups[gid]
        available = _condition_holds(group["available_when"], default_state)
        default_state[gid] = group["default"] if available else None
    if not all(_condition_holds(item, default_state) for item in constraints):
        bad("OPTION_DEFAULT_ILLEGAL", "selection_constraints", "默认组合不满足 selection_constraints")
    if not states and order:
        bad("OPTION_NO_LEGAL_STATE", "option_groups",
            "没有任何合法组合；检查 available_when 与 selection_constraints 是否互相矛盾")
    if worst_draws > MAX_SELECTED_DRAWS:
        bad("OPTION_DRAW_LIMIT", "meshes",
            f"某个网格在单个状态下最多 {worst_draws} 条 draw，上限 {MAX_SELECTED_DRAWS}")
    if worst_indices > MAX_SELECTED_INDICES:
        bad("OPTION_INDEX_LIMIT", "meshes",
            f"某个网格在单个状态下最多 {worst_indices} 个索引，上限 {MAX_SELECTED_INDICES}")

    return {
        "ok": not findings,
        "project": str(project_path),
        "groups": len(groups),
        "choices": sum(len(groups[gid]["choices"]) for gid in order),
        "states": len(states),
        "truncated": truncated,
        "default_state": default_state,
        "illegal_states": illegal,
        "states_over_draw_limit": over_draws,
        "max_draws_per_mesh": worst_draws,
        "max_indices_per_mesh": worst_indices,
        "worst_state": worst_state,
        "findings": findings,
        "codes": sorted({item["code"] for item in findings}),
        "evidence": "exact",
        "note": ("按 available_when 与 selection_constraints 枚举全部合法组合后逐态求值；"
                 "256 上限按单个网格判定，不是各部件求和"
                 if not truncated else
                 f"组合数超过 {MAX_OPTION_STATES}，已截断；结论只覆盖前 {MAX_OPTION_STATES} 个状态"),
    }


# ---------------------------------------------------------------------------
# 14. CrossIB grouping: which meshes really share index data
# ---------------------------------------------------------------------------
# A CrossIB source draws several components from one index buffer, and the
# redirect shaders (``CustomShader_ExtractCB1`` / ``RecordBones`` /
# ``RedirectCB1``) mean a draw's range no longer bounds against the buffer its
# section names — ``bem_triage`` refuses to check those for exactly that reason.
# On the BEM side the question becomes answerable again: group the meshes by the
# bytes they actually index, and the sharing either survived the conversion or it
# did not.
#
# Three outcomes matter:
#
#   * identical index bytes **and** identical vertex bytes -> one mesh stored
#     twice; the packer already dedups the payloads, so this is only a report;
#   * identical index bytes, different vertices -> the CrossIB signature: one
#     topology, several skins. Legal, and the reason the index payload is shared;
#   * different index bytes but the same triangle count -> the topology drifted,
#     which is the case a manual edit usually causes.
#
# Report only.

def _mesh_index_bytes(mesh: Dict[str, Any], payloads: List[bytes],
                      index: int) -> Tuple[bytes, str]:
    """Concatenated index bytes of a mesh, plus the layout that produced them."""
    if "indices" in mesh:
        payload_id = mesh["indices"]
        _require(0 <= int(payload_id) < len(payloads), "IB_PAYLOAD_MISSING",
                 f"mesh {index}: indices payload {payload_id} does not exist")
        return payloads[int(payload_id)], "single"
    parts: List[bytes] = []
    for draw_index, draw in enumerate(mesh.get("draws") or []):
        payload_id = draw.get("indices")
        _require(type(payload_id) is int and 0 <= payload_id < len(payloads),
                 "IB_PAYLOAD_MISSING",
                 f"mesh {index}: draw {draw_index} has no usable indices payload "
                 f"({payload_id!r})")
        parts.append(payloads[payload_id])
    return b"".join(parts), "per-draw"


def ib_groups(project_path: Path | str) -> Dict[str, Any]:
    """Group meshes by index-content fingerprint and report the sharing pattern."""
    data, is_project = load_project(project_path)
    manifest = data["manifest"] if is_project else data
    payloads = load_payloads(project_path, data, is_project)
    meshes = manifest["meshes"]

    rows: List[Dict[str, Any]] = []
    for index, mesh in enumerate(meshes):
        index_bytes, layout = _mesh_index_bytes(mesh, payloads, index)
        vertex_bytes = b"".join(payloads[stream["payload"]]
                                for stream in mesh.get("streams") or [])
        rows.append({
            "mesh": index,
            "index_layout": layout,
            "index_fingerprint": hashlib.sha256(index_bytes).hexdigest()[:16],
            "vertex_fingerprint": hashlib.sha256(vertex_bytes).hexdigest()[:16],
            "index_count": int(mesh.get("index_count") or 0),
            "index_size": int(mesh.get("index_size") or 0),
            "vertex_count": int(mesh.get("vertex_count") or 0),
            "draws": len(mesh.get("draws") or []),
            "components": sorted({draw.get("material_component")
                                  for draw in mesh.get("draws") or []
                                  if draw.get("material_component") is not None}),
        })

    by_index: Dict[str, List[Dict[str, Any]]] = {}
    for row in rows:
        by_index.setdefault(row["index_fingerprint"], []).append(row)

    groups: List[Dict[str, Any]] = []
    for fingerprint, members in sorted(by_index.items()):
        if len(members) < 2:
            continue
        vertices = {member["vertex_fingerprint"] for member in members}
        counts = {member["index_count"] for member in members}
        sizes = {member["index_size"] for member in members}
        # Inside one index group, an equal vertex fingerprint means the two meshes
        # are byte-for-byte the same mesh; anything else is one topology skinned
        # several ways, which is what CrossIB looks like after conversion.
        by_vertex: Dict[str, List[int]] = {}
        for member in members:
            by_vertex.setdefault(member["vertex_fingerprint"], []).append(member["mesh"])
        groups.append({
            "index_fingerprint": fingerprint,
            "meshes": [member["mesh"] for member in members],
            "components": sorted({component for member in members
                                  for component in member["components"]}),
            "identical_vertices": len(vertices) == 1,
            "vertex_variants": len(vertices),
            "index_count": sorted(counts),
            "index_size": sorted(sizes),
            "kind": "duplicate-mesh" if len(vertices) == 1 else "shared-topology",
            "exact_duplicates": [sorted(sub) for sub in by_vertex.values() if len(sub) > 1],
        })

    # Same triangle count, different bytes: the same primitive drawn with another
    # winding or another vertex order. Informational, not a defect.
    variants: List[Dict[str, Any]] = []
    by_count: Dict[Tuple[int, int], List[Dict[str, Any]]] = {}
    for row in rows:
        if row["index_count"]:
            by_count.setdefault((row["index_count"], row["index_size"]), []).append(row)
    for (count, size), members in sorted(by_count.items()):
        if len(members) < 2:
            continue
        if len({member["index_fingerprint"] for member in members}) == 1:
            continue
        variants.append({"index_count": count, "index_size": size,
                         "meshes": [member["mesh"] for member in members],
                         "fingerprints": sorted({member["index_fingerprint"]
                                                 for member in members})})

    # A shared index buffer cannot be indexed with two different widths.
    conflicts = [{"index_fingerprint": group["index_fingerprint"],
                  "meshes": group["meshes"], "index_size": group["index_size"]}
                 for group in groups if len(group["index_size"]) > 1]

    return {
        "ok": not conflicts,
        "project": str(project_path),
        "meshes": len(rows),
        "distinct_index_payloads": len(by_index),
        "shared_groups": groups,
        "shared_topology": sum(1 for group in groups if group["kind"] == "shared-topology"),
        # Redundant copies, not group members: a pair contributes one extra mesh.
        "duplicate_meshes": sum(len(pair) - 1 for group in groups
                                for pair in group["exact_duplicates"]),
        "same_count_variants": variants,
        "index_size_conflicts": conflicts,
        "meshes_detail": rows,
        "evidence": "exact",
        "note": ("索引指纹取自网格实际索引的字节，不是缓冲区长度；"
                 "同一指纹下顶点不同即 CrossIB 共享拓扑，属正常形态；"
                 "同长度不同字节只说明拓扑变体，不是缺陷"),
    }


# ---------------------------------------------------------------------------
# 15. Shape parameters: EFMI ShapeKey buffers -> BEM 1.3 slider spec
# ---------------------------------------------------------------------------
#
# The decode/encode chain already exists and is validated elsewhere:
# ``efmi_shapes.decode_binding`` reads the official buffers and
# ``bem_v13.apply_author_spec`` turns a ``bem-position-morphs`` spec into
# payloads. What was missing is discovering *which* (component, shape key,
# weight) triples a source mod actually drives and writing that spec out.
#
# Frames are emitted as 0 (neutral, no payload) and 1000 (the full buffer
# delta). Unity blend shapes are linear in the weight and the buffer stores the
# delta at weight 1, so two frames reproduce the whole range instead of only
# the value the source happens to bake at. The source constant becomes the
# parameter default, i.e. where the slider starts.

SHAPE_CONFIG = "ShapeKeyBatchConfigs"
SHAPE_IDS = "ShapeKeyVertexIds"
SHAPE_OFFSETS = "ShapeKeyVertexOffsets"
SHAPE_SUFFIXES = (SHAPE_CONFIG, SHAPE_IDS, SHAPE_OFFSETS)
SHAPE_RESOURCE = re.compile(
    r"^Resource_Component(\d+)_(ShapeKeyBatchConfigs|ShapeKeyVertexIds|ShapeKeyVertexOffsets)$")
SHAPE_ASSIGN = re.compile(r"^\$(component_id|shapekey_id|shapekey_value)\s*=\s*(.+)$", re.I)
SHAPE_RUN = re.compile(r"^run\s*=\s*CommandListSetShapeKey\s*$", re.I)
SHAPE_HEADER_WORDS = 8
SHAPE_BATCH_WORDS = 132
SHAPE_KEYS_PER_BATCH = 127
SHAPE_TICK_MAX = 1000


def _shape_open_source(path: Path | str) -> Any:
    """Open an EFMI mod folder or ``.zip`` with the standard library only.

    ``rar``/``7z`` need the external unpacking backend, so they are refused
    rather than half-read. Keeping this dependency-free matters: the command
    must work from an installed toolchain, where the CLI modules are compiled
    into an executable and cannot be imported.
    """
    return _ShapeSource(path)


class _ShapeSource:
    """Minimal read-only view over a mod folder or zip."""

    def __init__(self, path: Path | str):
        self.path = Path(path)
        self._zip = None
        if self.path.is_file() and self.path.suffix.lower() == ".zip":
            self._zip = zipfile.ZipFile(self.path)
            self.names = self._zip.namelist()
        elif self.path.is_dir():
            self.names = [row.relative_to(self.path).as_posix()
                          for row in self.path.rglob("*") if row.is_file()]
        elif self.path.is_file():
            raise Refused("SHAPE_SOURCE_ARCHIVE",
                          f"{self.path.suffix} 需要外部解包后端，请先解压成目录或 zip",
                          source=str(self.path))
        else:
            raise Refused("SHAPE_SOURCE_UNREADABLE",
                          f"源包既不是目录也不是 zip: {self.path}",
                          source=str(self.path))

    def close(self) -> None:
        if self._zip is not None:
            self._zip.close()

    def find(self, suffix: str) -> Optional[str]:
        wanted = suffix.replace("\\", "/")
        matches = [name for name in self.names
                   if name.replace("\\", "/") == wanted
                   or name.replace("\\", "/").endswith("/" + wanted)]
        if len(matches) > 1:
            raise Refused("SHAPE_INI_AMBIGUOUS",
                          f"{wanted} 在源包内有 {len(matches)} 个同名文件",
                          matches=matches[:8])
        return matches[0] if matches else None

    def read_exact(self, name: str) -> bytes:
        relative = name.replace("\\", "/")
        if self._zip is not None:
            return self._zip.read(relative)
        target = (self.path / relative).resolve()
        root = self.path.resolve()
        if root != target and root not in target.parents:
            raise Refused("SHAPE_SOURCE_ESCAPE", f"路径越界: {name}")
        return target.read_bytes()


def _shape_skip_name(name: str) -> bool:
    parts = [p.lower() for p in name.replace("\\", "/").split("/")]
    return any(p.startswith("disabled") or p in ("backup", "backups") for p in parts)


def _shape_ini_name(src: Any, override: Optional[str]) -> Tuple[str, List[str]]:
    """Pick the INI that declares shape resources; report every candidate."""
    if override:
        found = src.find(override)
        _require(found is not None, "SHAPE_INI_NOT_FOUND", f"源包内没有 {override}")
        return found, [found]
    candidates = []
    for name in src.names:
        if not name.lower().endswith(".ini") or _shape_skip_name(name):
            continue
        try:
            text = src.read_exact(name).decode("utf-8-sig", "replace")
        except (OSError, ValueError):
            continue
        hits = len(re.findall(r"ShapeKey(?:BatchConfigs|VertexIds|VertexOffsets)", text, re.I))
        if hits:
            candidates.append((hits, len(text), name))
    _require(bool(candidates), "SHAPE_DATA_ABSENT",
             "源包内没有 Resource_Component*_ShapeKey* 声明，这条包没有滑条可转",
             hint="确认是不是 LOD/贴图包；ShapeKey 数据只在带形变的部件上")
    candidates.sort(reverse=True)
    return candidates[0][2], [row[2] for row in candidates]


def _shape_sections(blocks: List[Dict[str, Any]]) -> Dict[str, str]:
    """First occurrence wins, matching loader semantics for prefixed sections."""
    sections: Dict[str, str] = {}
    seen = set()
    for block in blocks:
        key = block["name"].casefold()
        if key in seen:
            continue
        seen.add(key)
        sections[block["name"]] = block["body"]
    return sections


def _shape_statement(body: str, key: str) -> Optional[str]:
    for line in _active_lines(body):
        head, separator, tail = line.partition("=")
        if separator and head.strip().casefold() == key.casefold():
            return tail.strip()
    return None


def _shape_constants(sections: Dict[str, str]) -> Dict[str, str]:
    """Raw ``[Constants]`` expressions; variable names are case-insensitive."""
    values: Dict[str, str] = {}
    for name, body in sections.items():
        if name.casefold() != "constants":
            continue
        for line in _active_lines(body):
            head, separator, tail = line.partition("=")
            if not separator:
                continue
            key = head.strip().lstrip("$").casefold()
            if key and key not in values:
                values[key] = tail.strip()
    return values


def _shape_number(expression: Optional[str], constants: Dict[str, str],
                  depth: int = 0) -> Optional[float]:
    """Resolve a literal or a ``$constant`` chain; anything else is not a number."""
    if expression is None or depth > 8:
        return None
    text = expression.strip()
    if text.startswith("$"):
        return _shape_number(constants.get(text[1:].casefold()), constants, depth + 1)
    try:
        value = float(text)
    except ValueError:
        return None
    return value if math.isfinite(value) else None


def _shape_targets(sections: Dict[str, str],
                   constants: Dict[str, str]) -> Dict[int, Dict[int, float]]:
    """``component -> {shape key -> weight}`` from ``CommandListSetShapeKey`` calls."""
    targets: Dict[int, Dict[int, float]] = {}
    for body in sections.values():
        lines = _active_lines(body)
        if not any(SHAPE_RUN.match(line) for line in lines):
            continue
        component = shape_key = None
        weight = 0.0
        for line in lines:
            match = SHAPE_ASSIGN.match(line)
            if not match:
                continue
            number = _shape_number(match.group(2), constants)
            if number is None:
                continue
            field = match.group(1).casefold()
            if field == "component_id":
                component = int(number)
            elif field == "shapekey_id":
                shape_key = int(number)
            else:
                weight = float(number)
        if component is None or shape_key is None:
            continue
        targets.setdefault(component, {})[shape_key] = weight
    return targets


def _shape_resources(sections: Dict[str, str]) -> Dict[int, Dict[str, str]]:
    resources: Dict[int, Dict[str, str]] = {}
    for name, body in sections.items():
        match = SHAPE_RESOURCE.match(name)
        if not match:
            continue
        filename = _shape_statement(body, "filename")
        if filename:
            resources.setdefault(int(match.group(1)), {})[match.group(2)] = filename
    return resources


def _shape_resource_bytes(src: Any, ini: str, sections: Dict[str, str],
                          name: str) -> bytes:
    """Read one declared resource, rejecting dynamic or escaping paths."""
    body = next((text for key, text in sections.items()
                 if key.casefold() == name.casefold()), None)
    _require(body is not None, "SHAPE_RESOURCE_MISSING", f"缺少资源声明 {name}")
    filename = _shape_statement(body or "", "filename")
    _require(bool(filename), "SHAPE_RESOURCE_DYNAMIC",
             f"{name} 是动态/空资源，不能自动转换",
             section=name)
    assert filename is not None
    relative = filename.replace("\\", "/")
    _require(not relative.startswith("/") and ".." not in relative.split("/")
             and ":" not in filename, "SHAPE_RESOURCE_ESCAPE",
             f"{name} 的资源路径越界: {filename}")
    folder = ini.replace("\\", "/").rsplit("/", 1)[0] if "/" in ini.replace("\\", "/") else ""
    target = f"{folder}/{relative}" if folder else relative
    try:
        return src.read_exact(target)
    except (OSError, KeyError) as error:
        raise Refused("SHAPE_RESOURCE_UNREADABLE",
                      f"{name} 读取失败: {error}", file=target) from error


def _shape_batch_count(config: bytes) -> int:
    _require(len(config) >= SHAPE_HEADER_WORDS * 4 and len(config) % 4 == 0,
             "SHAPE_CONFIG_TRUNCATED",
             f"ShapeKeyBatchConfigs 只有 {len(config)} 字节，不足 8 个 u32 头部")
    words = len(config) // 4
    _require((words - SHAPE_HEADER_WORDS) % SHAPE_BATCH_WORDS == 0,
             "SHAPE_CONFIG_SHAPE",
             f"ShapeKeyBatchConfigs 有 {words} 个 u32，不符合 8 + 132N")
    return (words - SHAPE_HEADER_WORDS) // SHAPE_BATCH_WORDS


def _shape_highest_vertex(ids: bytes) -> int:
    _require(len(ids) % 4 == 0, "SHAPE_IDS_TRUNCATED",
             f"ShapeKeyVertexIds 有 {len(ids)} 字节，不是 u32 的整数倍")
    if not ids:
        return -1
    return max(value[0] for value in struct.iter_unpack("<I", ids))


def _shape_component_meshes(manifest: Dict[str, Any]) -> Tuple[Dict[int, int], Optional[str]]:
    """Map source component id -> BEM mesh index through the default appearance."""
    components = (manifest.get("target") or {}).get("components") or []
    identities = [row.get("id") for row in components]
    appearances = manifest.get("appearances") or []
    default = manifest.get("default_appearance_id")
    chosen = next((row for row in appearances if row.get("id") == default), None)
    if chosen is None and appearances:
        chosen = appearances[0]
    meshes = manifest.get("meshes") or []
    mapping: Dict[int, int] = {}
    for entry in (chosen or {}).get("components") or []:
        if entry.get("operation") != "replace":
            continue
        target, mesh = entry.get("target"), entry.get("mesh")
        if type(target) is not int or not 0 <= target < len(identities):
            continue
        if type(mesh) is not int or not 0 <= mesh < len(meshes):
            continue
        mapping.setdefault(identities[target], mesh)
    return mapping, (chosen or {}).get("id")


def _shape_position_ok(mesh: Dict[str, Any]) -> bool:
    positions = [row for row in mesh.get("attributes") or [] if row and row[0] == 0]
    return len(positions) == 1 and list(positions[0][1:3]) == [0, 3]


def shape_params(
    source: Path | str,
    project: Path | str,
    output: Optional[Path | str] = None,
    ini_name: Optional[str] = None,
    parameter_prefix: str = "shape",
) -> Dict[str, Any]:
    """Write a ``bem-position-morphs`` spec from a source mod's ShapeKey data.

    Refuses rather than guesses: a component whose exported mesh cannot be
    addressed with identity vertex order, or whose mesh has no Float32 XYZ
    position attribute, is reported as a blocker and left out of the spec.
    """
    data, is_project = load_project(project)
    # ``load_project`` returns the whole file; a project wraps the manifest.
    manifest = data["manifest"] if is_project else data
    meshes = manifest.get("meshes") or []
    mapping, appearance = _shape_component_meshes(manifest)

    src = _shape_open_source(source)
    try:
        ini, candidates = _shape_ini_name(src, ini_name)
        text = src.read_exact(ini).decode("utf-8-sig")
        _preamble, blocks = split_blocks(text)
        sections = _shape_sections(blocks)
        constants = _shape_constants(sections)
        targets = _shape_targets(sections, constants)
        resources = _shape_resources(sections)

        _require(bool(targets) or not resources, "SHAPE_NOT_DRIVEN",
                 "缓冲区声明齐全，但没有任何 CommandListSetShapeKey 调用把它们接上："
                 "源里这些滑条是死的（权重恒 0），没有可转的默认值，也不该凭空造一条滑条。"
                 "先在源里确认哪一处真正驱动它们，再重跑。",
                 ini=ini, components_with_buffers=sorted(resources))
        _require(bool(targets), "SHAPE_DATA_ABSENT",
                 "源包里既没有 Resource_Component*_ShapeKey* 声明，也没有 "
                 "CommandListSetShapeKey 调用，这条包没有滑条可转",
                 ini=ini)

        parameters: List[Dict[str, Any]] = []
        channels: List[Dict[str, Any]] = []
        blockers: List[Dict[str, Any]] = []
        skipped: List[Dict[str, Any]] = []
        rows: List[Dict[str, Any]] = []
        source_ref = _shape_source_ref(source, output)

        for component in sorted(targets):
            mesh_index = mapping.get(component)
            if mesh_index is None:
                skipped.append({"component": component,
                                "reason": "默认外观没有替换这个部件，没有可加形变的目标网格"})
                continue
            mesh = meshes[mesh_index]
            missing = [name for name in SHAPE_SUFFIXES
                       if not (resources.get(component) or {}).get(name)]
            if missing:
                blockers.append({"component": component, "code": "SHAPE_RESOURCE_MISSING",
                                 "mesh": mesh_index, "missing": missing})
                continue
            if not _shape_position_ok(mesh):
                blockers.append({"component": component, "code": "SHAPE_POSITION_ATTRIBUTE",
                                 "mesh": mesh_index,
                                 "message": "形变要求 Float32 XYZ 位置属性（semantic 0 / format 0 / dimension 3）"})
                continue
            stem = f"Resource_Component{component}_"
            config = _shape_resource_bytes(src, ini, sections, stem + SHAPE_CONFIG)
            ids = _shape_resource_bytes(src, ini, sections, stem + SHAPE_IDS)
            batches = _shape_batch_count(config)
            highest = _shape_highest_vertex(ids)
            vertex_count = mesh.get("vertex_count")
            if highest >= (vertex_count or 0):
                blockers.append({"component": component, "code": "SHAPE_VERTEX_MAP_REQUIRED",
                                 "mesh": mesh_index, "highest_source_vertex": highest,
                                 "mesh_vertex_count": vertex_count,
                                 "message": "源顶点号超出导出网格范围，需要显式 vertex_map 才能绑定"})
                continue
            made = []
            for shape_key in sorted(targets[component]):
                weight = targets[component][shape_key]
                if shape_key >= batches * SHAPE_KEYS_PER_BATCH:
                    blockers.append({"component": component, "code": "SHAPE_KEY_OUT_OF_RANGE",
                                     "shape_key": shape_key, "batches": batches})
                    continue
                tick = int(round(weight * SHAPE_TICK_MAX))
                if not weight or tick <= 0:
                    skipped.append({"component": component, "shape_key": shape_key,
                                    "reason": f"源权重为 {weight}，中性帧之外没有可转的形变"})
                    continue
                parameter = f"{parameter_prefix}_{component}_{shape_key}"
                made.append(dict(
                    id=parameter,
                    name=f"Component {component} shape {shape_key}",
                    min=0, max=SHAPE_TICK_MAX, neutral=0,
                    default=min(tick, SHAPE_TICK_MAX), step=1))
                channels.append(dict(mesh=mesh_index, parameter=parameter, frames=[
                    dict(value=0, neutral=True),
                    dict(value=SHAPE_TICK_MAX, efmi=dict(
                        source=source_ref, ini=ini, component=component,
                        shape_key=shape_key, vertex_order="exported"))]))
                rows.append({"component": component, "mesh": mesh_index,
                             "shape_key": shape_key, "source_weight": weight,
                             "default_tick": min(tick, SHAPE_TICK_MAX),
                             "sparse_records": None, "parameter": parameter})
            if made:
                parameters.extend(made)
            else:
                blockers.append({"component": component, "code": "SHAPE_NO_DRIVEN_KEY",
                                 "mesh": mesh_index,
                                 "message": "该部件的 ShapeKey 全部权重为 0"})
    finally:
        src.close()

    spec = {"schema": 1, "kind": "bem-position-morphs",
            "parameters": parameters, "mesh_deformations": channels}
    result: Dict[str, Any] = {
        "ok": not blockers and bool(parameters),
        "source": str(source),
        "project": str(project),
        "appearance": appearance,
        "ini": ini,
        "ini_candidates": candidates,
        "components": rows,
        "parameters": len(parameters),
        "channels": len(channels),
        "blockers": blockers,
        "skipped": skipped,
        "note": ("两帧 0/1000 覆盖整条滑条：源缓冲区存的是权重 1 的完整偏移，"
                 "Unity 混合按权重线性，所以不必只取源常量那一点。"
                 "default 取源常量，滑条初始位置与源一致。"),
    }
    if blockers and not parameters:
        result["spec"] = spec
        return result
    if output is None:
        result["spec"] = spec
        return result
    target = Path(output)
    target.parent.mkdir(parents=True, exist_ok=True)
    _write_json(spec, target)
    result["output"] = str(target)
    result["bytes"] = target.stat().st_size
    return result


def _shape_source_ref(source: Path | str, output: Optional[Path | str]) -> str:
    """Path as the spec will resolve it: relative to the spec when one is written."""
    resolved = Path(source).resolve()
    if output is None:
        return str(resolved)
    try:
        return os.path.relpath(resolved, Path(output).resolve().parent).replace("\\", "/")
    except ValueError:
        return str(resolved)


def _emit(payload: Dict[str, Any]) -> None:
    print(json.dumps(payload, ensure_ascii=False, indent=2))


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(
        description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    sub = parser.add_subparsers(dest="command", required=True)

    audit = sub.add_parser("ib-audit", help="measure a draw against its index buffer")
    audit.add_argument("ib", type=Path)
    audit.add_argument("--first", type=int, required=True)
    audit.add_argument("--count", type=int, required=True)
    audit.add_argument("--stride", type=int, choices=[2, 4], default=2)

    clamp = sub.add_parser("ib-clamp", help="emit a corrected draw count")
    clamp.add_argument("ib", type=Path)
    clamp.add_argument("--first", type=int, required=True)
    clamp.add_argument("--count", type=int, required=True)
    clamp.add_argument("--stride", type=int, choices=[2, 4], default=2)
    clamp.add_argument("--accept-truncation", action="store_true",
                       help="confirm the four playbook criteria are met")

    dds_audit = sub.add_parser("dds-audit", help="measure a DDS texture")
    dds_audit.add_argument("dds", type=Path)
    dds_audit.add_argument("--limit-mib", type=int, default=256)

    mip = sub.add_parser("dds-mipdrop", help="drop the top mip of every slice")
    mip.add_argument("dds", type=Path)
    mip.add_argument("-o", "--output", type=Path, required=True)
    mip.add_argument("--limit-mib", type=int, default=256)
    mip.add_argument("--allow-still-over", action="store_true")

    find = sub.add_parser("donor-find", help="list same-name donors, nothing copied")
    find.add_argument("need", help="relative path of the missing file")
    find.add_argument("--root", type=Path, required=True)
    find.add_argument("--hash", action="store_true", help="also compute sha256")

    copy = sub.add_parser("donor-copy", help="copy one donor after checking evidence")
    copy.add_argument("selected", type=Path, help="one candidate from donor-find")
    copy.add_argument("-o", "--output-dir", type=Path, required=True)
    copy.add_argument("--expect-size", type=int)
    copy.add_argument("--expect-sha256")
    copy.add_argument("--force", action="store_true")

    override = sub.add_parser("override-set", help="write validated material overrides")
    override.add_argument("project", type=Path)
    override.add_argument("-o", "--output", type=Path, required=True)
    override.add_argument("--mesh", type=int, default=0)
    override.add_argument("--draw", type=int, default=0)
    override.add_argument("--floats", help="JSON object, e.g. '{\"_Cutoff\": 0.5}'")
    override.add_argument("--colors", help="JSON object of RGBA arrays")
    override.add_argument("--vectors", help="JSON object of 4-component arrays")
    override.add_argument("--enable-keywords", nargs="*")
    override.add_argument("--disable-keywords", nargs="*")

    root = sub.add_parser("root-offset", help="write a validated root offset")
    root.add_argument("project", type=Path)
    root.add_argument("-o", "--output", type=Path, required=True)
    root.add_argument("--bone", default="Bip001")
    root.add_argument("--translation", default="0,0.04,0",
                      help="three comma-separated metres, e.g. 0,0.04,0")

    ini_audit = sub.add_parser(
        "ini-audit", help="report duplicate sections and the rule governing each")
    ini_audit.add_argument("ini", type=Path)
    ini_audit.add_argument("--globals-only", action="store_true",
                           help="only report prefix-less global duplicates")

    ini_norm = sub.add_parser(
        "ini-normalize", help="write a duplicate-free copy with an unchanged loader projection")
    ini_norm.add_argument("ini", type=Path)
    ini_norm.add_argument("-o", "--output", type=Path, required=True)
    ini_norm.add_argument("--drop-archived", action="store_true",
                          help="do not keep loader-ignored duplicates as comments")

    tex_norm = sub.add_parser(
        "texture-normalize",
        help="resolve conditional texture overrides to one static selection")
    tex_norm.add_argument("ini", type=Path)
    tex_norm.add_argument("-o", "--output", type=Path, required=True)
    tex_norm.add_argument("--set", action="append", default=[], metavar="NAME=VALUE",
                          help="state assumption, e.g. --set $swapvar_body=1")

    skin_audit = sub.add_parser(
        "skin-audit", help="measure skin palette, weight sums and out-of-palette indices")
    skin_audit.add_argument("project", type=Path)
    skin_audit.add_argument("--mesh", type=int, default=None,
                            help="limit the report to one mesh index")

    skin_fix = sub.add_parser(
        "skin-fix", help="zero out-of-palette indices that carry no weight")
    skin_fix.add_argument("project", type=Path)
    skin_fix.add_argument("-o", "--output-dir", type=Path, required=True)
    skin_fix.add_argument("--mesh", type=int, default=None)
    skin_fix.add_argument("--zero-weight-policy", choices=["keep", "first"], default="keep",
                          help="'first' also gives all-zero-weight vertices a full weight "
                               "on bone 0, which visibly pins them to the root")

    compact = sub.add_parser(
        "vertex-compact", help="drop unreferenced vertices and rewrite the draws")
    compact.add_argument("project", type=Path)
    compact.add_argument("-o", "--output-dir", type=Path, required=True)
    compact.add_argument("--mesh", type=int, required=True)

    entry = sub.add_parser(
        "entry-normalize",
        help="lower old inline ComponentN entry programs to the fixed LOD0 callback")
    entry.add_argument("ini", type=Path)
    entry.add_argument("-o", "--output", type=Path, required=True)
    entry.add_argument("--zero-draw-policy", choices=["keep", "refuse"], default="keep",
                       help="'refuse' stops when a callback is a pure CPU-posed draw and "
                            "the package also replaces textures")

    convert = sub.add_parser(
        "skin-convert",
        help="re-encode a merged-skeleton skin stream into a BEM-native layout")
    convert.add_argument("project", type=Path)
    convert.add_argument("-o", "--output-dir", type=Path, required=True)
    convert.add_argument("--mesh", type=int, required=True)
    convert.add_argument("--target-stride", type=int, choices=[12, 32], default=12,
                         help="12 = UNORM16 weights + UInt8 indices (default), "
                              "32 = Float32 weights + UInt32 indices")
    convert.add_argument("--palette", type=Path, default=None,
                         help="JSON array of {component, index, name} in global bone-id "
                              "order; defaults to target.components[].bone_names")

    tex_audit = sub.add_parser(
        "texture-audit",
        help="classify texture overrides; find duplicate hashes and slot writes")
    tex_audit.add_argument("ini", type=Path)

    cat_gap = sub.add_parser(
        "catalog-gap",
        help="diff a package's entry hashes against the character catalogs")
    cat_gap.add_argument("ini", type=Path)
    cat_gap.add_argument("--catalog-dir", type=Path, default=None,
                         help="override the catalog directory (defaults to <tool root>/catalog)")

    deform = sub.add_parser(
        "deformation-audit",
        help="validate parameters/mesh_deformations and decode delta payloads")
    deform.add_argument("project", type=Path)
    deform.add_argument("--minor", type=int, choices=[3, 4, 5], default=None,
                        help="header minor when the project file does not record it")

    option = sub.add_parser(
        "option-audit",
        help="enumerate option states; prove one candidate per target and the draw ceiling")
    option.add_argument("project", type=Path)

    ibs = sub.add_parser(
        "ib-groups",
        help="group meshes by index-content fingerprint; find shared topology and drift")
    ibs.add_argument("project", type=Path)

    shapes = sub.add_parser(
        "shape-params",
        help="write a BEM 1.3 slider spec from a source mod's EFMI ShapeKey data")
    shapes.add_argument("source", type=Path,
                        help="EFMI mod folder or .zip holding the ShapeKey buffers")
    shapes.add_argument("project", type=Path,
                        help="BEM project or export task that the spec will attach to")
    shapes.add_argument("--output", type=Path, default=None,
                        help="write the bem-position-morphs spec here instead of stdout")
    shapes.add_argument("--ini", default=None,
                        help="INI file name to read (defaults to the one declaring shape resources)")
    shapes.add_argument("--parameter-prefix", default="shape",
                        help="stable-id prefix for generated parameters")

    return parser


def main(argv: Optional[List[str]] = None) -> int:
    args = build_parser().parse_args(argv)
    try:
        if args.command == "ib-audit":
            _emit(audit_ib(args.ib, args.first, args.count, args.stride))
        elif args.command == "ib-clamp":
            _emit(clamp_ib(args.ib, args.first, args.count, args.stride,
                           args.accept_truncation))
        elif args.command == "dds-audit":
            _emit(audit_dds(args.dds, args.limit_mib * MIB))
        elif args.command == "dds-mipdrop":
            _emit(drop_mip0(args.dds, args.output, args.limit_mib * MIB,
                            args.allow_still_over))
        elif args.command == "donor-find":
            _emit(find_donors(args.need, args.root, args.hash))
        elif args.command == "donor-copy":
            _emit(copy_donor(args.selected, args.output_dir, args.expect_size,
                             args.expect_sha256, args.force))
        elif args.command == "override-set":
            _require(args.project.resolve() != args.output.resolve(), "PROJECT_WOULD_OVERWRITE",
                     "output must differ from the input project")
            data, is_project = load_project(args.project)
            floats = json.loads(args.floats) if args.floats else None
            colors = json.loads(args.colors) if args.colors else None
            vectors = json.loads(args.vectors) if args.vectors else None
            result = apply_overrides(data, is_project, args.mesh, args.draw, floats,
                                     colors, vectors, args.enable_keywords,
                                     args.disable_keywords)
            _write_json(data, args.output)
            _emit(dict(ok=True, output=str(args.output), material_overrides=result,
                       evidence="exact"))
        elif args.command == "root-offset":
            _require(args.project.resolve() != args.output.resolve(), "PROJECT_WOULD_OVERWRITE",
                     "output must differ from the input project")
            data, is_project = load_project(args.project)
            parts = [float(value) for value in args.translation.split(",")]
            _require(len(parts) == 3, "ROOT_OFFSET_BAD_VALUE",
                     "--translation needs three comma-separated numbers")
            result = apply_root_offset(data, is_project, tuple(parts), args.bone)
            _write_json(data, args.output)
            _emit(dict(ok=True, output=str(args.output), root_offset=result,
                       evidence="exact"))
        elif args.command == "ini-audit":
            _emit(audit_ini(args.ini, include_prefixed=not args.globals_only))
        elif args.command == "ini-normalize":
            _emit(normalize_ini(args.ini, args.output, keep_archived=not args.drop_archived))
        elif args.command == "texture-normalize":
            overrides: Dict[str, float] = {}
            for item in args.set:
                name, _, raw = item.partition("=")
                _require(bool(name) and raw != "", "TEXTURE_BAD_SET",
                         f"--set needs NAME=VALUE, got {item!r}")
                try:
                    overrides[name.strip()] = float(raw)
                except ValueError:
                    raise Refused("TEXTURE_BAD_SET",
                                  f"--set value is not a number: {item!r}") from None
            _emit(normalize_textures(args.ini, args.output, overrides))
        elif args.command == "skin-audit":
            _emit(audit_skin(args.project, args.mesh))
        elif args.command == "skin-fix":
            _emit(fix_skin(args.project, args.output_dir, args.mesh,
                           args.zero_weight_policy))
        elif args.command == "vertex-compact":
            _emit(vertex_compact(args.project, args.output_dir, args.mesh))
        elif args.command == "entry-normalize":
            _emit(entry_normalize(args.ini, args.output, args.zero_draw_policy))
        elif args.command == "skin-convert":
            _emit(skin_convert(args.project, args.output_dir, args.mesh,
                               args.target_stride, args.palette))
        elif args.command == "texture-audit":
            _emit(audit_textures(args.ini))
        elif args.command == "catalog-gap":
            _emit(catalog_gap(args.ini, args.catalog_dir))
        elif args.command == "deformation-audit":
            _emit(deformation_audit(args.project, args.minor))
        elif args.command == "option-audit":
            _emit(option_audit(args.project))
        elif args.command == "ib-groups":
            _emit(ib_groups(args.project))
        elif args.command == "shape-params":
            _emit(shape_params(args.source, args.project, args.output,
                               args.ini, args.parameter_prefix))
        return 0
    except Refused as refused:
        _emit(refused.as_dict())
        return 2
    except (OSError, ValueError, json.JSONDecodeError) as error:
        _emit(dict(ok=False, refused="IO_OR_PARSE", message=str(error)))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
