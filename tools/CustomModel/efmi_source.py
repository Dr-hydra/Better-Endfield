"""Read EFMI declarations without executing INI command lists.

Profiles describe original game meshes, never inferred replacement layouts.
The inspector can report incomplete exports; conversion remains fail-closed.
"""
from __future__ import annotations

import argparse
import json
from pathlib import Path
import re
import struct


def sections(text: str) -> dict[str, str]:
    text = text.replace("\r\n", "\n").replace("\r", "")
    parts = re.split(r"^\[([^\]\n]+)\][ \t]*$", text, flags=re.M)
    result = {}
    for name, body in zip(parts[1::2], parts[2::2]):
        if name in result:
            raise ValueError(f"duplicate INI section: {name}")
        result[name] = body
    return result


def value(body: str, key: str) -> str | None:
    found = re.search(r"^\s*" + re.escape(key) + r"\s*=\s*([^\n;]+)", body, re.M)
    return found[1].strip() if found else None


def conversion_inputs(src, profile: dict | None) -> tuple[dict, dict[int, int]]:
    meta_name = src.find("Meshes/Components.buf")
    meta = json.loads(src.read_exact(meta_name).decode("utf-8-sig")) if meta_name else {}
    counts = {}
    if profile:
        if profile.get("schema") != 1 or not profile.get("character_id"):
            raise ValueError("source profile requires schema=1 and character_id")
        for key, component in profile["components"].items():
            index = int(key)
            counts[index] = int(component["original_index_count"])
            for stream, declaration in enumerate(component["streams"]):
                meta_key = f"Component{index}_VB{stream}"
                if meta_key not in meta:
                    meta[meta_key] = declaration
    return meta, counts


def adapt_stream(raw: bytes, rule: dict, target_stride: int) -> bytes:
    """Explicit profile rule, never inferred just from buffer length."""
    if (rule.get("operation") != "retain_uv0" or rule.get("input_stride") != 12
            or target_stride != 8 or not rule.get("evidence")):
        raise ValueError("unsupported or undocumented stream adaptation")
    if len(raw) % 12:
        raise ValueError("UV adaptation input is not divisible by 12")
    return b"".join(raw[i:i+8] for i in range(0, len(raw), 12))


def analyze_source(src, ini_name: str, profile: dict | None = None, *, geometry_only=False, material_profile=None) -> dict:
    from convert_efmi_poc import parse_original_index_counts, parse_dds

    sec = sections(src.read_exact(ini_name).decode("utf-8-sig"))
    text = "\n".join(f"[{k}]\n{v}" for k, v in sec.items())
    errors, warnings, components = [], [], []
    if profile and profile.get("requires_geometry_only") and not geometry_only:
        errors.append("This source profile requires --geometry-only until native material mapping is verified")
    if profile and profile.get("requires_material_profile") and not geometry_only:
        if material_profile is None:
            errors.append("This source profile requires --material-profile or --geometry-only")
        else:
            from efmi_materials import collect_mapped_textures
            collect_mapped_textures(src, material_profile, profile["character_id"])
            warnings.append("Explicit native material approximation; RabbitFX visual equivalence is not established")
    counts = parse_original_index_counts(text)
    meta, profile_counts = conversion_inputs(src, profile)
    if not meta:
        errors.append("Missing Meshes/Components.buf; provide a verified --source-profile")
    # Both generations have an unsuffixed base entry; suffixed entries are
    # reported independently, not silently treated as native LOD numbers.
    ids = sorted({int(m[1]) for name in sec
                  if (m := re.fullmatch(r"TextureOverride_(?:EntryPoint_)?Component(\d+)", name))})
    if not ids:
        errors.append("No base component entries found")
    for cid in ids:
        entry = {"component": cid, "original_index_count": counts.get(cid), "streams": []}
        source = profile.get("components", {}).get(str(cid)) if profile else None
        if source:
            entry["source_mesh"] = source["mesh_name"]
            entry["source_bones"] = source["bone_count"]
            if cid in counts and counts[cid] != profile_counts[cid]:
                errors.append(f"C{cid}: original index count differs from source profile")
            entry["original_index_count"] = profile_counts[cid]
        if entry["original_index_count"] is None:
            errors.append(f"C{cid}: missing original index count; source mapping required")
        files = [src.find(f"Meshes/Component{cid}_VB{s}.buf") for s in range(3)]
        if not any(files):
            entry["geometry"] = False
            components.append(entry)
            continue
        entry["geometry"] = True
        vertices = []
        for stream, filename in enumerate(files):
            label = f"C{cid}/VB{stream}"
            decl = meta.get(f"Component{cid}_VB{stream}")
            declared_stride = sum(s["stride"] for s in decl["semantics"]) if decl else 0
            body = sec.get(f"Resource_Component{cid}_VB{stream}", "")
            stride_text = value(body, "stride")
            stride = int(stride_text) if stride_text else declared_stride
            if not filename or stride <= 0:
                errors.append(f"{label}: missing stream or stride")
                continue
            raw = src.read_exact(filename)
            if len(raw) % stride:
                errors.append(f"{label}: size is not divisible by stride {stride}")
            vertices.append(len(raw) // stride)
            item = {"stream": stream, "stride": stride, "vertices": vertices[-1]}
            rule = source.get("stream_adaptations", {}).get(str(stream)) if source else None
            if rule:
                target = source["streams"][stream]["semantics"]
                if stream != 1 or len(target) != 1 or target[0]["name"] != "TEXCOORD" or target[0]["format"] != "R32G32_FLOAT":
                    raise ValueError(f"{label}: UV adaptation requires UV-only target")
                if stride != rule["input_stride"]:
                    raise ValueError(f"{label}: adaptation input stride mismatch")
                adapted = adapt_stream(raw, rule, declared_stride)
                item["adaptation"] = rule
                item["output_bytes"] = len(adapted)
                stride = declared_stride
                warnings.append(f"{label}: explicit UV-only adaptation; runtime visual validation pending")
            if declared_stride and stride != declared_stride:
                errors.append(f"{label}: mod stride {stride} != source/declaration stride {declared_stride}")
            if source:
                expected = sum(s["stride"] for s in source["streams"][stream]["semantics"])
                item["source_stride"] = expected
                if expected != stride and expected != declared_stride:
                    errors.append(f"{label}: mod stride {stride} != source stride {expected}")
            if not decl:
                errors.append(f"{label}: missing attribute semantics")
            if stream == 2 and decl and declared_stride == stride:
                offset = 0
                for semantic in decl["semantics"]:
                    if semantic["name"].startswith("BLENDINDICES"):
                        if semantic["format"] != "R8G8B8A8_UINT" or semantic["stride"] != 4:
                            errors.append(f"{label}: unsupported bone index encoding")
                            break
                        slots = [b for i in range(vertices[-1]) for b in raw[i*stride+offset:i*stride+offset+4]]
                        item["max_bone"] = max(slots, default=0)
                        if source and item["max_bone"] >= source["bone_count"]:
                            errors.append(f"{label}: bone slot {item['max_bone']} exceeds source palette")
                        break
                    offset += semantic["stride"]
                else:
                    errors.append(f"{label}: missing BLENDINDICES")
            entry["streams"].append(item)
        if len(set(vertices)) > 1:
            errors.append(f"C{cid}: inconsistent vertex counts {vertices}")
        ib_name = src.find(f"Meshes/Component{cid}_IB.buf")
        if not ib_name:
            errors.append(f"C{cid}: missing index buffer")
        else:
            ib = src.read_exact(ib_name)
            fmt = value(sec.get(f"Resource_Component{cid}_IB", ""), "format")
            if fmt not in (None, "DXGI_FORMAT_R16_UINT", "R16_UINT"):
                errors.append(f"C{cid}: unsupported index format {fmt}")
            if len(ib) % 6:
                errors.append(f"C{cid}: index buffer is not a uint16 triangle list")
            else:
                indices = struct.unpack(f"<{len(ib)//2}H", ib)
                entry["indices"] = len(indices)
                if indices and vertices and max(indices) >= vertices[0]:
                    errors.append(f"C{cid}: index exceeds vertex count")
                draws = re.findall(r"^\s*drawindexed(?:instanced)?\s*=\s*(.+)",
                                   sec.get(f"CommandList_Draw_Component{cid}", ""), re.M)
                for draw in draws:
                    args = [a.strip() for a in draw.split(",")]
                    if args[0].isdigit():
                        start = args[2] if len(args) == 5 else args[1] if len(args) == 3 else "?"
                        if not start.isdigit() or int(start) + int(args[0]) > len(indices):
                            correction = source.get("draw_correction", {}) if source else {}
                            if (len(draws) == 1 and start == "0" and correction.get("declared_count") == int(args[0])
                                    and correction.get("actual_count") == len(indices) and correction.get("evidence")):
                                warnings.append(f"C{cid}: use complete IB ({len(indices)} indices); explicit correction from {args[0]}")
                            else:
                                errors.append(f"C{cid}: draw {draw} exceeds or cannot resolve IB range ({len(indices)} indices)")
        lod_name = src.find(f"Meshes/Component{cid}_VB2_LOD.buf")
        if lod_name and files[2]:
            entry["alternate_skin_stream"] = {
                "file": lod_name,
                "differs": src.read_exact(lod_name) != src.read_exact(files[2]),
            }
        components.append(entry)
    defined = set(sec)
    # Known alternate LOD entries are not part of the base/LOD0 import.
    base_text = "\n".join(body for name, body in sec.items()
                           if not re.fullmatch(r"TextureOverride_(?:EntryPoint_)?Component\d+_LOD\d*", name))
    refs = set(re.findall(r"=\s*ref\s+(Resource\w+)", base_text))
    all_refs = set(re.findall(r"=\s*ref\s+(Resource\w+)", text))
    for resource in sorted((all_refs - defined) - refs):
        warnings.append(f"Unselected alternate LOD references undefined {resource}; excluded from LOD0 import")
    for resource in sorted(refs - defined):
        (warnings if geometry_only else errors).append(f"Undefined local resource: {resource}")
    external = sorted(set(re.findall(r"^\s*run\s*=\s*(CommandList\\[^\n;]+)", text, re.M)))
    if external:
        warnings.append("External command lists require native material mapping: " + ", ".join(external))
    texture_count = 0
    untagged_textures = 0
    for name in src.names:
        if name.lower().endswith(".dds"):
            parse_dds(name, src.read_exact(name))
            texture_count += 1
            if re.search(r" t=[0-9a-fA-F]+\.dds$", name):
                untagged_textures += 1
    if untagged_textures:
        warnings.append(f"{untagged_textures} textures lack original-format tags; verify native material slots/pins before deployment")
    for body in sec.values():
        filename = value(body, "filename")
        if filename and filename.replace("\\", "/") not in {
                n.replace("\\", "/").removeprefix(ini_name.rsplit("/", 1)[0] + "/")
                if "/" in ini_name else n.replace("\\", "/") for n in src.names}:
            errors.append(f"Missing referenced file: {filename}")
    return {"ini": ini_name, "character_id": profile.get("character_id") if profile else None,
            "components": components, "textures": texture_count,
            "errors": errors, "warnings": warnings, "conversion_ready": not errors,
            "lod_scope": "base entries / LOD0", "geometry_only": geometry_only,
            "validation_scope": "structural import only; not runtime bone semantics or material-effect validation"}


def main() -> int:
    from convert_efmi_poc import Source
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("source", type=Path)
    parser.add_argument("--ini")
    parser.add_argument("--source-profile", type=Path)
    parser.add_argument("--material-profile", type=Path)
    parser.add_argument("--geometry-only", action="store_true")
    parser.add_argument("--report", type=Path, required=True)
    args = parser.parse_args()
    src = Source(args.source)
    try:
        name = args.ini or src.find("mod.ini")
        if name not in src.names:
            raise ValueError("selected mod.ini is missing")
        profile = json.loads(args.source_profile.read_text(encoding="utf-8-sig")) if args.source_profile else None
        material_profile = json.loads(args.material_profile.read_text(encoding="utf-8-sig")) if args.material_profile else None
        report = analyze_source(src, name, profile, geometry_only=args.geometry_only, material_profile=material_profile)
    finally:
        src.close()
    args.report.parent.mkdir(parents=True, exist_ok=True)
    args.report.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    print(f"components={len(report['components'])} textures={report['textures']} errors={len(report['errors'])}")
    for error in report["errors"]:
        print(error)
    return 0 if report["conversion_ready"] else 2


if __name__ == "__main__":
    raise SystemExit(main())
