#!/usr/bin/env python3
"""Source-package triage: decide whether a mod can be converted before touching it.

Answers the three questions that actually gate every conversion:

1. What kind of source is this?  (entry family, unsupported mechanisms)
2. Is the source complete?      (declared-but-absent DDS/BUF payloads)
3. Is the geometry self-consistent? (draw ranges that overrun their index buffer)

Question 2 is the single largest failure class in the reference corpus: roughly
half of all recorded failures are "the INI declares a resource whose payload file
does not exist anywhere". That condition is mechanically decidable, so it should
never be discovered halfway through a manual adaptation.

Zero third-party dependencies. Reads sources only; never writes inside them.

Usage:
    python bem_triage.py "F:\\zmd\\伊冯\\伊冯-26W by anlingQWQ"
    python bem_triage.py <package> --json
    python bem_triage.py --root "F:\\zmd" --limit 20
"""
from __future__ import annotations

import argparse
import json
import re
import sys
from pathlib import Path

from bem_ini import Section, iter_active_inis, is_active_ini, read_ini

# Entry naming in the wild, by frequency across the reference corpus:
#   TextureOverride_TextureN         3388  -> texture slot, NOT a mesh entry
#   TextureOverride_ComponentN       2310  -> entry (component family)
#   TextureOverride_ComponentN_LODN  1747  -> entry (component family, LOD-qualified)
#   TextureOverride_HASH_N_N          310  -> entry (hash family)
#   TextureOverride_LODN.HASH_N_N     169  -> entry (hash family, LOD-qualified)
#   TextureOverride_HASH              104  -> entry or draw-bound texture
#   TextureOverride_IB_HASH            84  -> index-buffer entry
# Matching "TextureN" as an entry is the classic first-order mistake here.
ENTRY_COMPONENT = re.compile(r"TextureOverride_(?:EntryPoint_)?Component\d+(?:_LOD\d+)?$")
ENTRY_HASH_LOD = re.compile(
    r"TextureOverride_(?:EntryPoint_)?(?:LOD\d+\.)?(?:IB_)?[0-9a-fA-F]{8}(?:_\d+){0,2}$"
)
RESOURCE_REF = re.compile(r"=\s*(?:ref\s+)?(Resource\S+)")
STRONG_REF = re.compile(
    r"^(?:ps-t\d+|vs-t\d+|gs-t\d+|this|vb\d+|ib)\s*=\s*(?:ref\s+)?(Resource\S+)", re.I
)
WEAK_REF = re.compile(r"=\s*(?:ref\s+)?(Resource\S+)", re.I)
DRAW_CALL = re.compile(r"^draw(indexed)?(instanced)?\s*=\s*(.+)$", re.I)
TEXTURE_SLOT = re.compile(r"^ps-t\d+\s*=", re.I)
# UI overlay resources are drawn by the author's own HUD command lists. An absent
# one breaks the on-screen panel, not the character mesh, so it is not a blocker.
UI_RESOURCE = re.compile(
    r"(?:UI[A-Z]|Version$|LeftBar|RightBar|Slider|ImageToRender|Font|Glyph|Button|Icon)", re.I)
# A PNG payload is only ever blitted by a HUD command list. Mesh materials need a
# DDS with a mip chain, so a PNG never feeds a draw-bound texture slot.
UI_SUFFIX = (".png", ".jpg", ".bmp", ".tga")
IDENTITY = re.compile(r"(?i)\bt=([0-9a-f]{8})\b")
# Some exports never write a file for a game texture: they reference it under the
# loader's own "Resource-<hash>-<n>-<slot>" name. The identity is in the name, so
# an absent payload there is expected rather than missing.
NAME_IDENTITY = re.compile(r"^Resource-([0-9a-f]{8})-", re.I)
TEXTURE_HASH = re.compile(r"^[0-9a-fA-F]{8}$")
# Markers that a command list redirects index data from another buffer.
CROSS_IB = re.compile(
    r"CustomShader_ExtractCB1|CustomShader_RecordBones|CustomShader_RedirectCB1|"
    r"ResourceDumpedCB1|ResourceFakeCB1|cs-t\d+\s*=\s*ResourceID_",
    re.I,
)

# Feature flags mirror the reference corpus classifier so verdicts stay compatible.
FEATURE_RULES = (
    ("custom_shader", re.compile(r"\[\s*CustomShader", re.I)),
    ("external_material_framework", re.compile(r"\\RabbitFX\\", re.I)),
    ("merged_skeleton", re.compile(r"MergedSkeleton", re.I)),
    ("bone_indices_u16", re.compile(r"ElementFormat\s*\(\s*BLENDINDICES\s*,\s*0\s*\)\s*=\s*R16G16B16A16_UINT", re.I)),
    ("shape_keys", re.compile(r"ShapeKey(BatchConfigs|VertexIds|VertexOffsets|s)?", re.I)),
    ("interactive_ui", re.compile(r"draw_image|\[\s*KeySwap|CommandList\\EFMIv1\\Object_", re.I)),
)

# Resource payloads that are engine-owned rather than shipped by the author.
GENERATED_PREFIXES = ("ResourceFake", "ResourceDumped", "ResourcePrev", "Rendered")


def _is_generated(resource: str) -> bool:
    return resource.startswith(GENERATED_PREFIXES)


def _strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in "\"'":
        return value[1:-1]
    return value


def _index_stride(section: Section) -> int:
    """Bytes per index element, inferred from the resource declaration."""
    for key in ("format", "stride"):
        value = (section.value(key) or "").upper()
        match = re.search(r"R(\d+)", value)
        if match:
            bits = int(match.group(1))
            if bits in (16, 32):
                return bits // 8
        if value.isdigit() and int(value) in (2, 4):
            return int(value)
    return 4  # BEM sources are overwhelmingly R32_UINT


def _draw_range(statement: str) -> tuple[int, int] | None:
    """Return ``(count, first_index)`` for a draw call, or None."""
    match = DRAW_CALL.match(statement)
    if not match:
        return None
    indexed, instanced, args = match.groups()
    parts = [part.strip() for part in args.split(",")]
    try:
        count = int(parts[0])
    except (IndexError, ValueError):
        return None
    first = 0
    if indexed:
        position = 2 if instanced else 1
        if len(parts) > position:
            try:
                first = int(parts[position])
            except ValueError:
                first = 0
    return count, first


def _resolve_payload(ini_path: Path, package_root: Path, filename: str) -> Path | None:
    """Locate a declared payload, tolerating quoting, prefixes and case drift."""
    filename = _strip_quotes(filename).replace("\\", "/").lstrip("./")
    for base in (ini_path.parent, package_root):
        candidate = base / filename
        if candidate.is_file():
            return candidate
    # Some authors ship paths relative to the package but with a stray folder name.
    tail = "/".join(filename.split("/")[-2:])
    for match in package_root.rglob(Path(tail).name):
        if match.is_file() and match.as_posix().endswith(tail):
            return match
    return None


def collect_references(documents) -> tuple[dict[str, list[str]], dict[str, list[str]]]:
    """Split resource consumers into strong (draw-path) and weak (framework) uses.

    Strong: a statement that feeds the draw itself — a texture slot ``ps-tN``,
    ``this`` on a TextureOverride, or a bound ``vbN``/``ib``. Losing one of those
    loses visible output.

    Weak: assignments into a framework namespace such as
    ``Resource\\RabbitFx\\Diffuse = ref Resource-...``. Those only reach the GPU when
    the framework's own command lists still run. A conversion that maps RabbitFX
    inputs onto native material properties drops the whole channel, so an absent
    file here is frequently irrelevant — the reference package succeeds despite
    dozens of them.

    Treating both alike is what turns a convertible package into a false "missing
    source files" verdict.
    """
    strong: dict[str, list[str]] = {}
    weak: dict[str, list[str]] = {}
    for relative, sections in documents:
        for name, section in sections.items():
            if name.lower().startswith("resource"):
                continue
            for line in section.lines:
                match = STRONG_REF.match(line)
                if match:
                    strong.setdefault(match.group(1), []).append(f"{relative} :: {name}")
                    continue
                for found in WEAK_REF.finditer(line):
                    weak.setdefault(found.group(1), []).append(f"{relative} :: {name}")
    return strong, weak


def collect_resources(package_root: Path, documents: list[tuple[str, dict[str, Section]]]):
    """Build a *scoped* resource table.

    Declarations are scoped to the directory of the INI that declares them, because
    3Dmigoto resolves payload paths relative to the INI. Packages that ship several
    variants (``mod.ini`` next to ``ult/mod.ini``) routinely reuse the same resource
    name for different files; a single global name->entry map silently lets the last
    variant win and produces nonsense capacity figures for the first one.
    """
    scoped: dict[str, dict[str, dict]] = {}
    global_table: dict[str, dict] = {}
    missing_ini: list[dict] = []
    seen_missing: set[tuple[str, str]] = set()

    for relative, sections in documents:
        ini_path = package_root / relative
        scope = Path(relative).parent.as_posix()
        bucket = scoped.setdefault(scope, {})
        for name, section in sections.items():
            lowered = name.lower()
            if not lowered.startswith("resource") or lowered.endswith(("_srv", "_uav")):
                continue
            filename = section.value("filename")
            if not filename:
                continue
            resolved = _resolve_payload(ini_path, package_root, filename)
            entry = {
                "resource": name,
                "ini": relative,
                "scope": scope,
                "declared": _strip_quotes(filename),
                "exists": resolved is not None,
                "path": str(resolved) if resolved else None,
                "size": resolved.stat().st_size if resolved else None,
                "stride": _index_stride(section),
                "kind": "index" if lowered.endswith(("_index", "_ib")) else "payload",
            }
            bucket[name] = entry
            global_table.setdefault(name, entry)
            if resolved is None and (scope, name) not in seen_missing:
                seen_missing.add((scope, name))
                missing_ini.append(entry)
    return scoped, global_table, missing_ini


def find_entry_family(documents) -> tuple[str | None, list[str]]:
    component, lod, where = False, False, []
    for relative, sections in documents:
        for name in sections:
            if ENTRY_COMPONENT.fullmatch(name):
                component = True
                where.append(f"{relative} :: {name}")
            elif ENTRY_HASH_LOD.fullmatch(name):
                lod = True
                where.append(f"{relative} :: {name}")
    if component and lod:
        return "mixed", where
    if component:
        return "component-n", where
    if lod:
        return "hash-lod", where
    return None, where


def detect_features(documents) -> tuple[dict[str, list[str]], str]:
    hits: dict[str, list[str]] = {}
    for relative, sections in documents:
        for name, section in sections.items():
            for feature, pattern in FEATURE_RULES:
                if pattern.search(f"[{name}]"):
                    hits.setdefault(feature, []).append(f"{relative} :: [{name}]")
            for line in section.lines:
                for feature, pattern in FEATURE_RULES:
                    if pattern.search(line):
                        hits.setdefault(feature, []).append(f"{relative} :: {line[:120]}")
    text = "\n".join(f"[{name}]" for _, sections in documents for name in sections)
    if re.search(r"^\s*global\s+persist\s+\$", text, re.M):
        hits.setdefault("static_appearance_switches", []).append("global persistence declarations")
    for relative, sections in documents:
        for name, section in sections.items():
            if any(TEXTURE_SLOT.match(line) for line in section.lines):
                hits.setdefault("per_draw_texture_slots", []).append(f"{relative} :: [{name}]")
    return {key: value[:12] for key, value in hits.items()}, "\n".join(text)


def _is_ui_channel(entry: dict) -> bool:
    """True when an absent payload only feeds the author's HUD, not the mesh."""
    declared = (entry.get("declared") or "").lower()
    return bool(UI_RESOURCE.search(entry["resource"])) or declared.endswith(UI_SUFFIX)


def declared_hashes(documents) -> dict[str, str]:
    """Map every ``hash = <8 hex>`` a TextureOverride in this package declares to its section."""
    by_hash: dict[str, str] = {}
    for _, sections in documents:
        for name, section in sections.items():
            if not name.lower().startswith("textureoverride"):
                continue
            value = (section.value("hash") or "").strip()
            if TEXTURE_HASH.match(value):
                by_hash.setdefault(value.casefold(), name)
    return by_hash


def identity_anchored(missing: list[dict], documents) -> dict[str, str]:
    """Map absent payloads that name a game texture identity to that identity.

    A texture resource whose filename carries ``t=<hash>`` is not an author asset
    at all: it is a reference to an original game texture, renamed so the loader
    can bind it. When a ``TextureOverride`` in the same package declares that same
    ``hash``, the converter can point at the native texture directly and never
    needs the DDS. Judging by file existence alone turns those into blockers on
    packages that convert cleanly.

    This proves an identity is *declared*, not that the native texture exists;
    resolving it still requires the character catalog, which is a separate stage.
    """
    by_hash = declared_hashes(documents)
    anchored: dict[str, str] = {}
    for entry in missing:
        match = IDENTITY.search(entry.get("declared") or "")
        if not match:
            continue
        owner = by_hash.get(match.group(1).casefold())
        if owner:
            anchored[entry["resource"]] = owner
    return anchored


RUN_CALL = re.compile(r"^run\s*=\s*(?:CommandList\\)?(\S+)", re.I)


def _expand_runs(name: str, sections: dict, depth: int = 0,
                 trail: tuple[str, ...] = ()) -> list[str]:
    """Inline ``run = CommandList\\X`` so buffer state is read in execution order.

    A draw's index buffer is whatever ``ib`` was last set to, and a delegating
    command list may set it in the callee. Reading only the caller's own lines
    attributes the draw to the wrong buffer — or to none — and both directions
    produce a confident, wrong verdict.
    """
    if depth > 6 or name in trail or name not in sections:
        return []
    out: list[str] = []
    for line in sections[name].lines:
        match = RUN_CALL.match(line)
        if match and match.group(1) in sections:
            out.extend(_expand_runs(match.group(1), sections, depth + 1, trail + (name,)))
            continue
        out.append(line)
    return out


# External material framework (RabbitFX) touch points. The call is written with
# every casing in the wild — "RabbitFX" and "RabbitFx" are the same framework, and
# matching one spelling misses about a fifth of the call sites.
RABBITFX_CALL = re.compile(r"run\s*=\s*Commandlist?\\RabbitF[Xx]\\SetTextures", re.I)
RABBITFX_SLOT = re.compile(
    r"^Resource\\RabbitF[Xx]\\(\w+)\s*=\s*(?:ref\s+)?(Resource\S+)", re.I)
RABBITFX_DEF = re.compile(r"^(?:CommandList)?RabbitF[Xx]\w*$", re.I)

# Which framework slot carries what, and what losing it costs. The framework does
# not invent these names: each slot *is* the property it carries (an empty
# placeholder section becomes ``Resource\<ns>\<Slot>`` under the framework's own
# ``namespace = ...``), and the caller fills it with a plain ref. Base maps are
# irreplaceable; the other channels may legitimately be abandoned.
SLOT_CLASS = {
    "diffuse": "baseline", "normal": "baseline", "light": "baseline",
    "glow": "extra", "fx": "extra", "rain": "extra", "discard": "extra",
}
# When no slot feeds the file, the declared filename often still names the role
# ("...-NormalMap.dds"). That is the author's own vocabulary, not a similarity
# guess, but it is reported as a separate, weaker source so it can be weighed.
_FILENAME_ROLE = re.compile(r"(diffuse|normal|light|glow|fx|rain|discard)\s*map", re.I)


def _slot_concept(name: str) -> str:
    """Reduce a slot name to the property it carries: ``NormalMap`` → ``normal``."""
    lowered = name.casefold()
    return lowered[:-3] if lowered.endswith("map") else lowered


def framework_inputs(documents, global_table: dict) -> dict:
    """Describe how a package uses an external material framework.

    Three separable facts, which conversions keep conflating:

    * Does it **call** the framework (``run = CommandList\\RabbitFX\\SetTextures``)?
      The definition is supplied by the framework installed in the game's mod
      directory and is, in practice, never shipped inside the source package. So
      the per-draw bindings that call produces cannot be read from the source.
    * Does it **define** it locally? Then those bindings are ordinary commands and
      must be read like any other command list.
    * Does it feed the framework through **slot indirection**
      (``Resource\\RabbitFX\\Diffuse = ref Resource-x``)? That part *is* resolvable
      without the framework: follow the reference to the local resource, read its
      ``filename``, take the ``t=<hash>`` identity.

    Never infer the first case from the third, and never assume a bundled copy of
    the framework describes the one the author used.
    """
    calls: list[str] = []
    slots: dict[str, dict] = {}
    definitions: list[str] = []
    for relative, sections in documents:
        for name, section in sections.items():
            if RABBITFX_DEF.match(name.strip()):
                definitions.append(f"{relative} :: {name}")
            for line in section.lines:
                if RABBITFX_CALL.search(line):
                    calls.append(f"{relative} :: {name}")
                    continue
                match = RABBITFX_SLOT.match(line)
                if match:
                    slot, target = match.group(1), match.group(2)
                    entry = global_table.get(target)
                    declared = entry["declared"] if entry else None
                    identity = IDENTITY.search(declared or "")
                    slots[slot.lower()] = {
                        "resource": target,
                        "exists": bool(entry and entry.get("exists")),
                        "declared": declared,
                        "identity": identity.group(1) if identity else None,
                        "declared_by": f"{relative} :: {name}",
                    }
    resolved = {key: value for key, value in slots.items() if value["identity"]}
    return {
        "calls": len(calls),
        "call_sites": calls[:6],
        "defined_locally": bool(definitions),
        "definitions": definitions[:4],
        "slots": slots,
        "slots_with_identity": len(resolved),
        # A call whose definition is not in the package cannot be read statically.
        "bindings_unknown": bool(calls) and not definitions,
    }


def index_overruns(package_root: Path, documents, scoped: dict[str, dict[str, dict]],
                   global_table: dict[str, dict]) -> tuple[list[dict], list[dict]]:
    """Flag draw ranges that exceed their index buffer.

    A CrossIB section is skipped on purpose. When a command list runs
    ``CustomShader_ExtractCB1`` / ``RecordBones`` / ``RedirectCB1`` the following
    draws read index data redirected from another buffer, so ``ib = ref X`` no
    longer bounds them. Comparing against X produces a spectacular false positive
    (a 2 178-index buffer "needing" 240 756 indices) on packages that convert fine.
    """
    problems, skipped = [], []
    seen_problem: dict[tuple[str, str], dict] = {}
    for relative, sections in documents:
        bucket = scoped.get(Path(relative).parent.as_posix(), {})
        for name, section in sections.items():
            if not name.startswith(("CommandList", "TextureOverride")):
                continue
            statements = _expand_runs(name, sections)
            cross_ib = any(CROSS_IB.search(line) for line in statements)
            # One command list may rebind `ib` several times and draw from each
            # buffer in turn. Every binding starts a new segment; comparing the
            # list's widest draw against only the last buffer it bound reports an
            # overrun that no draw actually makes.
            segments: list[tuple[str, list[tuple[int, int]]]] = []
            current: list[tuple[int, int]] | None = None
            for line in statements:
                if re.match(r"^ib\s*=\s*ref\s+", line, re.I):
                    match = RESOURCE_REF.search(line)
                    if match:
                        current = []
                        segments.append((match.group(1), current))
                    continue
                if current is None:
                    continue
                span = _draw_range(line)
                if span:
                    current.append(span)
            segments = [item for item in segments if item[1]]
            if not segments:
                continue
            if cross_ib:
                skipped.append({
                    "code": "CROSS_IB_SECTION_SKIPPED",
                    "section": f"{relative} :: {name}",
                    "buffer": segments[0][0],
                    "detail": "CrossIB redirection present; draw ranges do not bound against the "
                              "declared index buffer. Static truncation check is not applicable.",
                })
                continue
            for bound, spans in segments:
                entry = bucket.get(bound) or global_table.get(bound)
                if not entry or not entry.get("exists") or not entry.get("size"):
                    continue
                # Bound-by-`ib` is the authoritative signal; the section name is not.
                stride = entry.get("stride") or 4
                capacity = entry["size"] // stride
                needed = max(first + count for (count, first) in spans)
                if needed <= capacity:
                    continue
                key = (f"{relative} :: {name}", bound)
                if key in seen_problem and seen_problem[key]["required_indices"] >= needed:
                    continue
                record = {
                    "code": "INDEX_BUFFER_OVERRUN",
                    "section": f"{relative} :: {name}",
                    "buffer": bound,
                    "capacity_indices": capacity,
                    "required_indices": needed,
                    "missing_indices": needed - capacity,
                    "draws_in_segment": len(spans),
                    "detail": "Draw range exceeds the bound index buffer. Confirm the four "
                              "end-of-buffer conditions before treating this as recoverable; a "
                              "retry cannot restore absent triangles.",
                }
                seen_problem[key] = record
                problems.append(record)
    return problems, skipped


def state_scale(documents) -> dict:
    """Count appearance switches and their declared value ranges."""
    switches: list[dict] = []
    for relative, sections in documents:
        for name, section in sections.items():
            if not name.lower().startswith(("key", "preset")):
                continue
            values, variables = [], []
            for line in section.lines:
                assignment = re.match(r"^\s*(\$[\w.]+)\s*=\s*([-\d.,\s]+)$", line)
                if assignment:
                    variables.append(assignment.group(1))
                    values.extend(int(part) for part in re.findall(r"-?\d+", assignment.group(2)))
            switches.append({
                "section": f"{relative} :: {name}",
                "type": section.value("type") or "cycle",
                "variables": variables,
                "declared_values": sorted(set(values)),
            })
    variables = {var for item in switches for var in item["variables"]}
    return {"switch_sections": switches, "variable_count": len(variables)}


def load_documents(package_root: Path):
    """Effective sections, with the loader's include and duplicate rules applied.

    Falls back to the plain per-file reader when :mod:`bem_merge` is absent, so
    the probe still runs from a copy that has not got the companion module.
    """
    try:
        from bem_merge import merge as merge_sources
    except ImportError:
        return list(iter_active_inis(package_root)), None
    result = merge_sources(package_root)
    if not result.live:
        return list(iter_active_inis(package_root)), None
    return result.documents(), result


def triage(package: str | Path) -> dict:
    package_root = Path(package).resolve()
    if not package_root.is_dir():
        raise SystemExit(f"Not a directory: {package_root}")

    documents, resolution = load_documents(package_root)
    active = [relative for relative, _ in documents]
    family, entry_sections = find_entry_family(documents)
    features, _ = detect_features(documents)
    resources, global_table, missing = collect_resources(package_root, documents)
    framework = framework_inputs(documents, global_table)
    strong_refs, weak_refs = collect_references(documents)
    overruns, cross_ib_skipped = index_overruns(package_root, documents, resources, global_table)
    scale = state_scale(documents)

    if "custom_shader" in features:
        verdict = "manual_only"
        reason = "Author custom shader definitions/calls present: the program itself cannot be " \
                 "reproduced by parameter overrides. Manual adaptation only."
    elif "external_material_framework" in features:
        verdict = "manual_only"
        reason = "Depends on an external material framework (RabbitFX): native equivalence must be " \
                 "proven per draw, never assumed. Manual adaptation only."
    elif "shape_keys" in features:
        verdict = "requires_mapping"
        reason = "Shape keys present; BEM supports position morphs, but only with explicit " \
                 "parameter binding in the export project."
    elif family == "mixed":
        verdict = "manual_only"
        reason = "One entry mixes ComponentN and hash/LOD declarations; the family must be split first."
    elif family == "component-n" and not (
        {"per_draw_texture_slots", "merged_skeleton", "bone_indices_u16"} & features.keys()
    ):
        verdict = "standard_candidate"
        reason = "Standard ComponentN entry without per-draw texture slots or merged skeleton: " \
                 "the automated pipeline applies."
    elif family:
        verdict = "requires_mapping"
        reason = "Entry is parseable but needs verified skeleton/material mapping before any build."
    else:
        verdict = "unrecognized"
        reason = "No ComponentN or hash/LOD entry found; check for a nested package or a disabled INI."

    # Distinguish "the source is genuinely missing payloads" from "this checkout is
    # INI-only". The reference workspace keeps a cleaned_INI tree that has no
    # Buffer/Texture directories at all; running triage there reports hundreds of
    # false MISSING_PAYLOAD_FILE hits.
    declared_total = sum(len(bucket) for bucket in resources.values())
    truncated_copy = declared_total > 0 and len(missing) == declared_total

    anchored = identity_anchored(missing, documents)
    missing_identity = [
        entry for entry in missing
        if entry["resource"] in strong_refs and entry["resource"] in anchored
    ]
    missing_live = [
        entry for entry in missing
        if entry["resource"] in strong_refs and not _is_ui_channel(entry)
    ]
    missing_ui = [
        entry for entry in missing
        if entry["resource"] in strong_refs and _is_ui_channel(entry)
    ]
    missing_framework = [
        entry for entry in missing
        if entry["resource"] not in strong_refs and entry["resource"] in weak_refs
    ]
    missing_dead = [
        entry for entry in missing
        if entry["resource"] not in strong_refs and entry["resource"] not in weak_refs
    ]

    blockers: list[dict] = []
    if truncated_copy:
        blockers.append({
            "code": "SOURCE_LOOKS_TRUNCATED",
            "detail": f"All {declared_total} declared payloads are absent. This checkout holds INI "
                      f"files only; point triage at the untouched source tree, not at a cleaned "
                      f"INI copy.",
        })
    else:
        for entry in missing_live:
            if entry["resource"] in anchored:
                blockers.append({
                    "code": "MISSING_PAYLOAD_IDENTITY_ANCHORED",
                    "resource": entry["resource"],
                    "declared": entry["declared"],
                    "ini": entry["ini"],
                    "identity": IDENTITY.search(entry["declared"]).group(1),
                    "declared_by": anchored[entry["resource"]],
                    "used_by": strong_refs[entry["resource"]][:6],
                    "detail": "Absent texture whose filename names a game identity that a "
                              "TextureOverride in this package also declares. That makes it "
                              "recoverable *if* the identity resolves to a native texture, but it "
                              "is not evidence that one exists: packages that failed on exactly "
                              "this shape are on record. Resolve the identity before spending "
                              "effort.",
                })
                continue
            blockers.append({
                "code": "MISSING_REFERENCED_PAYLOAD",
                "resource": entry["resource"],
                "declared": entry["declared"],
                "ini": entry["ini"],
                "used_by": strong_refs[entry["resource"]][:6],
                "detail": "Absent payload that a draw-path statement actually consumes. Search "
                          "same-character peer packages for an identical path; if none carries it, "
                          "the package is blocked at the source.",
            })
    # Framework slots are named after the property they carry, so the slot a
    # missing file sits in decides whether the absence matters. Reverse-map the
    # declared resource back to its slot; a file nobody feeds is not a slot at all.
    slot_of = {value["resource"].casefold(): key
               for key, value in framework["slots"].items()}
    blockers.extend(overruns)

    framework_warnings: list[dict] = []
    hashes = declared_hashes(documents)
    for entry in missing_framework:
        slot = slot_of.get(entry["resource"].casefold())
        if slot:
            concept, source = _slot_concept(slot), "slot"
        else:
            named = _FILENAME_ROLE.search(entry["declared"] or "")
            concept, source = (named.group(1).casefold() if named else None), "filename"
        in_name = NAME_IDENTITY.match(entry["resource"])
        identity = in_name.group(1).casefold() if in_name else None
        owner = hashes.get(identity) if identity else None
        if owner:
            detail = (f"The name itself carries the identity {identity}, and [${owner}] declares that "
                      f"same hash: this is the game's own texture referenced by name, not an author "
                      f"asset. No donor file is needed — resolve the identity and expect no payload.")
        else:
            detail = ("Absent file referenced only through a framework namespace. The role decides "
                      "the weight: a base-colour, normal or light-map slot means the material lost "
                      "a base map (blocking even though the reference is indirect), while an extra "
                      "channel only matters if that channel is kept. A role read from the filename "
                      "is weaker than one read from a slot — state which you used. The verdict is "
                      "left to the converter.")
        framework_warnings.append({
            "code": "MISSING_FRAMEWORK_INPUT",
            "resource": entry["resource"],
            "declared": entry["declared"],
            "slot": slot,
            "role": concept,
            "role_source": source if concept else None,
            "role_class": SLOT_CLASS.get(concept or ""),
            "identity_in_name": identity,
            "identity_declared_by": owner,
            "used_by": weak_refs[entry["resource"]][:4],
            "detail": detail,
        })

    warnings = framework_warnings + [
        {
            "code": "MISSING_UI_PAYLOAD",
            "resource": entry["resource"],
            "declared": entry["declared"],
            "used_by": strong_refs[entry["resource"]][:4],
            "detail": "Absent HUD/overlay asset. The in-game panel loses a graphic; the mesh "
                      "conversion is unaffected.",
        }
        for entry in missing_ui
    ]
    if framework["bindings_unknown"]:
        warnings.append({
            "code": "FRAMEWORK_DEFINITION_ABSENT",
            "framework": "RabbitFX",
            "call_sites": framework["call_sites"],
            "detail": f"{framework['calls']} call(s) into the framework, and its command list is "
                      f"not defined in this package — it is installed once in the game's mod "
                      f"directory. Read the call against the framework's documented interface "
                      f"(slot-to-register table, delegated draw formula) rather than from the "
                      f"package alone; a copy found in another package is not evidence of the "
                      f"version this author used. Dump the parameter assignments next to the call: "
                      f"for a delegated draw they *are* the draw's first/count.",
        })
    if framework["slots"]:
        warnings.append({
            "code": "FRAMEWORK_SLOT_INDIRECTION",
            "framework": "RabbitFX",
            "slots": {key: {"resource": value["resource"], "identity": value["identity"]}
                      for key, value in framework["slots"].items()},
            "detail": "Framework inputs reach the material through slot references. Those are "
                      "resolvable without the framework — follow the ref, read the filename, take "
                      "its t=<hash> identity — and are separate from whether the framework's own "
                      "command list runs.",
        })
    blocked_by_source = bool(overruns) or (bool(missing_live) and not truncated_copy)

    return {
        "source": str(package_root),
        "section_resolution": {
            "mode": "loader" if resolution is not None else "per-file",
            "entry": getattr(resolution, "entry", None),
            "discarded_duplicate_sections": sum(
                1 for item in getattr(resolution, "ignored", [])
                if item.get("action") == "discard"),
            "scrubbed_banner_lines": len(getattr(resolution, "scrubbed", [])),
            "include_statements": len(getattr(resolution, "includes", [])),
            "notes": getattr(resolution, "notes", [])[:8],
        },
        "active_ini_count": len(active),
        "active_inis": active,
        "entry_family": family,
        "entry_sections": entry_sections[:24],
        "features": {key: value[:8] for key, value in features.items()},
        "verdict": verdict,
        "reason": reason,
        "framework_inputs": {
            "calls": framework["calls"],
            "defined_locally": framework["defined_locally"],
            "definitions": framework["definitions"],
            "bindings_unknown": framework["bindings_unknown"],
            "slot_count": len(framework["slots"]),
            "slots_with_identity": framework["slots_with_identity"],
        },
        "blocked_by_source": blocked_by_source,
        "blockers": blockers,
        "warnings": warnings,
        "resources": {
            "declared_with_payload": sum(
                1 for bucket in resources.values() for item in bucket.values() if item["exists"]
            ),
            "declared_missing": len(missing),
            "missing_and_used": len(missing_live),
            "missing_framework_only": len(missing_framework),
            "missing_but_unused": len(missing_dead),
            "missing_ui_only": len(missing_ui),
            "missing_identity_anchored": len(missing_identity),
        },
        "dead_declarations": [
            {"resource": entry["resource"], "declared": entry["declared"]}
            for entry in missing_dead[:40]
        ],
        "index_buffers": [
            {"resource": item["resource"], "size": item["size"], "stride": item.get("stride")}
            for item in global_table.values() if item.get("kind") == "index"
        ],
        "cross_ib_sections": cross_ib_skipped[:20],
        "state_scale": {
            "switch_section_count": len(scale["switch_sections"]),
            "switch_variable_count": scale["variable_count"],
            "switches": scale["switch_sections"][:24],
        },
        "note": "Triage never proves convertibility: it only rules out the mechanical blockers. "
                "Identity matching and appearance coverage still need review.",
    }


def render(report: dict) -> str:
    resources = report["resources"]
    resolution = report.get("section_resolution", {})
    resolved = resolution.get("discarded_duplicate_sections", 0) or \
        resolution.get("scrubbed_banner_lines", 0)
    lines = [
        f"source        : {report['source']}",
        f"active INIs   : {report['active_ini_count']}",
        "sections      : " + (
            f"{resolution['mode']} resolution, {resolved} disbelieved "
            f"({resolution.get('discarded_duplicate_sections', 0)} dead duplicates, "
            f"{resolution.get('scrubbed_banner_lines', 0)} banner lines, "
            f"{resolution.get('include_statements', 0)} include statements)"
            if resolution else "not resolved"),
        f"entry family  : {report['entry_family'] or 'NOT FOUND'}",
        f"verdict       : {report['verdict']}",
        f"why           : {report['reason']}",
        f"resources     : {resources['declared_with_payload']} present / "
        f"{resources['declared_missing']} absent "
        f"({resources['missing_and_used']} draw-path, "
        f"{resources['missing_framework_only']} framework-only, "
        f"{resources.get('missing_ui_only', 0)} UI-only, "
        f"{resources.get('missing_identity_anchored', 0)} identity-anchored, "
        f"{resources['missing_but_unused']} unused)",
        f"state switches: {report['state_scale']['switch_section_count']} sections / "
        f"{report['state_scale']['switch_variable_count']} variables",
    ]
    if report["features"]:
        lines.append("features      : " + ", ".join(sorted(report["features"])))
    if report["blockers"]:
        lines.append("")
        lines.append(f"BLOCKERS ({len(report['blockers'])})")
        for blocker in report["blockers"]:
            where = blocker.get("section") or blocker.get("resource") or "-"
            detail = blocker.get("declared") or blocker.get("detail", "")
            lines.append(f"  [{blocker['code']}] {where}")
            if detail:
                lines.append(f"      {str(detail)[:150]}")
            if blocker.get("missing_indices"):
                lines.append(
                    f"      needs {blocker['required_indices']} indices, buffer holds "
                    f"{blocker['capacity_indices']} (short {blocker['missing_indices']})"
                )
    else:
        lines.append("")
        lines.append("No mechanical blocker found. Next: identity matching, then appearance review.")
    if report["warnings"]:
        lines.append("")
        lines.append(f"WARNINGS ({len(report['warnings'])}): not blockers; verify before "
                     f"discarding the channel they belong to.")
        grouped: dict[str, list[dict]] = {}
        for warning in report["warnings"]:
            grouped.setdefault(warning.get("code", "?"), []).append(warning)
        for code, items in grouped.items():
            lines.append(f"  {code} x{len(items)}")
            for warning in items[:4]:
                if "resource" in warning:
                    role = ""
                    if warning.get("role"):
                        role = (f"  [{warning['role']} / {warning.get('role_class') or '?'}, "
                                f"from {warning['role_source']}]")
                    lines.append(f"    {warning['resource']} -> {warning.get('declared')}{role}")
                else:
                    lines.append(f"    {warning.get('detail', '')[:150]}")
            if len(items) > 4:
                lines.append(f"    ... {len(items) - 4} more")
        fw = [w for w in report["warnings"] if w.get("code") == "MISSING_FRAMEWORK_INPUT"]
        resolved = [w for w in fw if w.get("identity_declared_by")]
        if resolved:
            lines.append(f"  {len(resolved)} of them carry a game identity in the resource name and "
                         f"expect no payload: resolve the identity, do not hunt for a donor.")
        baseline = [w for w in fw
                    if w.get("role_class") == "baseline" and not w.get("identity_declared_by")]
        if baseline:
            lines.append(f"  {len(baseline)} sit in a base-map slot (diffuse/normal/light) with no "
                         f"identity: treat as blocking, not as a droppable channel.")
    if report["dead_declarations"]:
        lines.append("")
        lines.append(
            f"UNUSED DECLARATIONS ({len(report['dead_declarations'])} shown, harmless): "
            "no statement consumes these, so their absent files do not block conversion."
        )
        for entry in report["dead_declarations"][:6]:
            lines.append(f"  {entry['resource']} -> {entry['declared']}")
    return "\n".join(lines)


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(description="Triage a BEM source package before conversion.")
    parser.add_argument("package", nargs="?", help="source package directory")
    parser.add_argument("--root", help="scan every child directory of this root")
    parser.add_argument("--limit", type=int, default=0, help="cap packages when using --root")
    parser.add_argument("--json", action="store_true", help="emit JSON instead of text")
    parser.add_argument("--output", help="write the JSON report here")
    args = parser.parse_args(argv)

    if not args.package and not args.root:
        parser.error("provide a package directory or --root")

    reports = []
    if args.root:
        targets = [path for path in sorted(Path(args.root).iterdir()) if path.is_dir()]
        if args.limit:
            targets = targets[: args.limit]
        for target in targets:
            try:
                reports.append(triage(target))
            except SystemExit as exc:
                reports.append({"source": str(target), "error": str(exc)})
            except Exception as exc:  # keep batch scans going
                reports.append({"source": str(target), "error": f"{type(exc).__name__}: {exc}"})
    else:
        reports.append(triage(args.package))

    if args.json or args.output:
        payload = json.dumps(reports if len(reports) > 1 else reports[0], ensure_ascii=False, indent=2)
        if args.output:
            Path(args.output).write_text(payload + "\n", encoding="utf-8")
            print(f"wrote {args.output}")
        else:
            print(payload)
    else:
        for index, report in enumerate(reports):
            if index:
                print("\n" + "-" * 70 + "\n")
            if "error" in report:
                print(f"{report['source']}\n  ERROR {report['error']}")
            else:
                print(render(report))

    blocked = sum(1 for report in reports if report.get("blocked_by_source"))
    return 0 if not blocked else 1


if __name__ == "__main__":
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8")
    raise SystemExit(main())
