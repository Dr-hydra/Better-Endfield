"""Inspect hash-named EFMI LOD exports; never execute their INI/HLSL.

This is an inventory, not a lowering pass. Draw conditions, shader state and
merged skeletons must be resolved before these sources can become BEM payloads.
"""
from pathlib import PurePosixPath
import re
import struct

ENTRY = re.compile(r"TextureOverride_(?:EntryPoint_)?LOD(\d+)\.([0-9a-fA-F]{8})_(\d+)_(\d+)$")


def binding_paths(program):
    """Keep branch-local bindings; preserve alternatives after a conditional.

    No expression or command is executed. Callers must still reject conversion
    until pass conditions and command-list side effects have native mappings.
    """
    states, stack = {(None, None, None, None)}, []  # vb0, vb1, vb2, ib
    for line in program:
        if line.startswith("if "):
            stack.append([states.copy(), set(), False])
        elif line.startswith("elif ") or line == "else":
            if not stack:
                raise ValueError("unmatched conditional branch")
            stack[-1][1].update(states)
            states = stack[-1][0].copy()
            stack[-1][2] |= line == "else"
        elif line == "endif":
            if not stack:
                raise ValueError("unmatched endif")
            initial, branches, has_else = stack.pop()
            states |= branches | (set() if has_else else initial)
        elif match := re.fullmatch(r"(vb[012]|ib)\s*=\s*(\S+)", line):
            slot = 3 if match[1] == "ib" else int(match[1][-1])
            states = {state[:slot] + (match[2],) + state[slot + 1:] for state in states}
        elif re.match(r"drawindexed(?:instanced)?\s*=", line):
            yield line, states.copy()
        if len(states) > 64:
            raise ValueError("too many unresolved buffer-binding paths")
    if stack:
        raise ValueError("unclosed conditional")


def inspect_lod_source(src, ini_name, sec, *, geometry_only=False):
    from efmi_source import value
    from convert_efmi_poc import parse_dds

    errors, components, textures = [], [], []
    names = {n.replace("\\", "/"): n for n in src.names}
    root = PurePosixPath(ini_name.replace("\\", "/")).parent

    def read_resource(resource):
        body = sec.get(resource, "")
        filename = value(body, "filename")
        if not filename:
            raise ValueError(f"{resource}: missing resource or filename")
        path = PurePosixPath(filename.replace("\\", "/"))
        if path.is_absolute() or ".." in path.parts or ":" in str(path):
            raise ValueError(f"{resource}: source-relative filename required")
        name = names.get(str(root / path))
        if name is None:
            raise ValueError(f"{resource}: missing file {filename}")
        return name, src.read_exact(name), body

    for section, body in sec.items():
        match = ENTRY.fullmatch(section)
        if not match:
            continue
        lod, draw_hash, original_count, first_index = match.groups()
        entry = {"section": section, "lod": int(lod), "draw_hash": draw_hash,
                 "original_index_count": int(original_count), "first_index": int(first_index),
                 "streams": []}
        components.append(entry)
        for key, expected in (("hash", draw_hash), ("match_index_count", original_count),
                              ("match_first_index", first_index)):
            actual = value(body, key)
            if actual is None or actual.lower() != expected.lower():
                errors.append(f"{section}: {key} differs from entry identity")
        callback = re.search(r"Callback_Component_DrawCustom\s*=\s*ref\s+(\S+)", body)
        draw_name = callback[1] if callback else section
        draw_body = sec.get(draw_name)
        if draw_body is None:
            errors.append(f"{section}: missing draw callback {draw_name}")
            continue
        entry["draw_section"] = draw_name
        entry["draw_program"] = [line.split(";", 1)[0].strip()
                                 for line in draw_body.splitlines()
                                 if line.split(";", 1)[0].strip()]
        component_id = re.search(r"\\component_id\s*=\s*(\d+)", body)
        if component_id:
            entry["merged_component_id"] = int(component_id[1])
        try:
            vertices = []
            for slot in range(3):
                resource = value(draw_body, f"vb{slot}")
                name, data, declaration = read_resource(resource)
                stride = int(value(declaration, "stride") or 0)
                if stride <= 0 or len(data) % stride:
                    raise ValueError(f"vb{slot}: invalid stride or buffer length")
                vertices.append(len(data) // stride)
                entry["streams"].append({"stream": slot, "file": name, "stride": stride,
                                         "vertices": vertices[-1]})
            if not vertices[0] or len(set(vertices)) != 1:
                raise ValueError("vertex counts disagree or are zero")
            _, data, declaration = read_resource(value(draw_body, "ib"))
            fmt = (value(declaration, "format") or "").removeprefix("DXGI_FORMAT_")
            code = {"R16_UINT": "H", "R32_UINT": "I"}.get(fmt)
            if code is None or len(data) % struct.calcsize(code):
                raise ValueError("unsupported index format or invalid index buffer length")
            indices = [i[0] for i in struct.iter_unpack("<" + code, data)]
            entry.update(index_format=fmt, index_count=len(indices), max_index=max(indices, default=-1))
            if not indices or max(indices) >= vertices[0]:
                raise ValueError("index exceeds vertex count or index buffer is empty")
            draws = []
            initial_bindings = tuple(value(draw_body, slot) for slot in ("vb0", "vb1", "vb2", "ib"))
            cached = {initial_bindings: (indices, vertices[0])}
            for line, paths in binding_paths(entry["draw_program"]):
                draw = re.match(r"(drawindexedinstanced|drawindexed)\s*=\s*(.*)", line)
                args = [x.strip() for x in draw[2].split(",")]
                try:
                    if draw[1] == "drawindexedinstanced" and len(args) == 5:
                        count, start, base = int(args[0]), int(args[2]), int(args[3])
                    elif draw[1] == "drawindexed" and len(args) == 3:
                        count, start, base = map(int, args)
                    else:
                        raise ValueError()
                except ValueError:
                    raise ValueError(f"unresolved draw: {line}") from None
                for bindings in paths:
                    if bindings not in cached:
                        sizes = []
                        for resource in bindings[:3]:
                            _, raw, decl = read_resource(resource)
                            stride = int(value(decl, "stride") or 0)
                            if stride <= 0 or len(raw) % stride:
                                raise ValueError("rebound VB has invalid stride/length")
                            sizes.append(len(raw) // stride)
                        if not sizes[0] or len(set(sizes)) != 1:
                            raise ValueError("rebound vertex counts disagree or are zero")
                        _, raw, decl = read_resource(bindings[3])
                        rebound_fmt = (value(decl, "format") or "").removeprefix("DXGI_FORMAT_")
                        code = {"R16_UINT": "H", "R32_UINT": "I"}.get(rebound_fmt)
                        if code is None or len(raw) % struct.calcsize(code):
                            raise ValueError("rebound IB has invalid format/length")
                        rebound_indices = [i[0] for i in struct.iter_unpack("<" + code, raw)]
                        cached[bindings] = rebound_indices, sizes[0]
                    bound_indices, bound_vertices = cached[bindings]
                    if count < 0 or start < 0 or start + count > len(bound_indices) or count % 3:
                        raise ValueError(f"draw outside IB or not triangles: {line}")
                    selected = bound_indices[start:start + count]
                    if selected and (min(selected) + base < 0 or max(selected) + base >= bound_vertices):
                        raise ValueError(f"draw base vertex outside VB: {line}")
                draws.append({"count": count, "start": start, "base_vertex": base,
                              "buffer_binding_paths": sorted(paths, key=str)})
            if not draws:
                raise ValueError("no explicit draws found")
            entry["draws"] = draws
            entry["cross_buffer_draws"] = any(
                tuple(bindings) != initial_bindings for d in draws for bindings in d["buffer_binding_paths"])
        except ValueError as exc:
            errors.append(f"{section}: {exc}")

    for name in src.names:
        if name.lower().endswith(".dds"):
            try:
                width, height, mips, fmt, srgb, data = parse_dds(name, src.read_exact(name))
                textures.append({"file": name, "width": width, "height": height,
                                 "mips": mips, "format": fmt, "srgb": srgb})
            except ValueError as exc:
                errors.append(str(exc))
    # Check every local file reference, including shader sources. Reading them
    # does not execute commands, includes, or compute shaders from a mod.
    for resource, body in sec.items():
        if value(body, "filename"):
            try:
                read_resource(resource)
            except ValueError as exc:
                errors.append(str(exc))
    text = "\n".join(line.split(";", 1)[0] for body in sec.values() for line in body.splitlines())
    if not components:
        errors.append("No hash-named LOD entry points found")
    dependencies = sorted(set(re.findall(r"^\s*run\s*=\s*(\S+)", text, re.M)))
    defaults = {m[1]: int(m[2]) for m in re.finditer(
        r"^\s*global\s+persist\s+(\$\w+)\s*=\s*(-?\d+)\s*$", sec.get("Constants", ""), re.M)}
    skeleton_program = {name: body.strip() for name, body in sec.items()
                        if name == "CommandListInitializeMergedSkeleton" or name.startswith("ResourceID_")}
    blockers = ["hash_lod_lowering", "verified_native_mesh_and_bone_mapping", "draw_state_lowering"]
    if "MergedSkeleton" in text:
        blockers.append("merged_skeleton_remap")
    if re.search(r"ElementFormat\(BLENDINDICES,\s*0\)\s*=\s*R16G16B16A16_UINT", text):
        blockers.append("uint16_blend_indices")
    if re.search(r"^\s*run\s*=\s*CustomShader", text, re.M):
        blockers.append("custom_shader_semantics")
    if any(c.get("cross_buffer_draws") for c in components):
        blockers.append("cross_buffer_draw_mapping")
    if any(c["lod"] == 0 and c.get("max_index", 0) > 65535 for c in components):
        blockers.append("uint32_indices_or_mesh_partition")
    if not geometry_only and textures:
        blockers.append("per_draw_native_material_mapping")
    return {"ini": ini_name, "source_format": "efmi_hash_lod", "character_id": None,
            "components": components, "textures": len(textures), "texture_inventory": textures,
            "persistent_defaults": defaults, "command_dependencies": dependencies,
            "skeleton_program": skeleton_program,
            "lod_counts": {str(lod): sum(c["lod"] == lod for c in components)
                           for lod in sorted({c["lod"] for c in components})},
            "structural_errors": errors, "blockers": blockers,
            "errors": errors + ["Unsupported conversion requirement: " + b for b in blockers],
            "warnings": ["All explicit draw branches inventoried; default visibility and shader state are not evaluated"],
            "conversion_ready": False, "geometry_only": geometry_only, "lod_scope": "inventory of all declared LODs",
            "validation_scope": "buffer/explicit draw structure only; no native mesh, bone or material equivalence claim"}
