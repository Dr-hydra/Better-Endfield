"""Bounded, static EFMI hash/LOD lowering to BEMPC25.

Only verified profile data supplies native identities/layouts. Source commands
are parsed as data; no Python eval, shader execution or arbitrary command runs.
"""
import ast
import io
import re
import struct
import zlib
from pathlib import PurePosixPath

from convert_efmi_poc import FILE_HEADER, COMPONENT_HEADER, TEXTURE_ENTRY, parse_dds
from efmi_lod_source import ENTRY
from efmi_source import sections, value


def crc(text):
    return zlib.crc32(text.encode("utf-8")) & 0xffffffff


def layout_crc(native):
    attrs = native["attributes"]
    if not attrs or len(attrs)>14 or [a[0] for a in attrs] != sorted({a[0] for a in attrs}):
        raise ValueError("native attributes must have unique sorted IDs")
    sizes = (4, 2, 1, 1, 2, 2, 1, 1, 2, 2, 4, 4)
    strides = [0, 0, 0]
    for attribute, fmt, dim, stream in attrs:
        if not 0<=attribute<14 or not 0<=fmt<len(sizes) or not 1<=dim<=4 or not 0<=stream<3:
            raise ValueError("invalid native vertex descriptor")
        strides[stream] += sizes[fmt]*dim
    if strides != native["strides"]:
        raise ValueError("native descriptor/stride mismatch")
    expected = [[13, 6, 4, 2]] if strides[2]==4 else [[12, 4, 4, 2], [13, 6, 4, 2]]
    if [a for a in attrs if a[3]==2] != expected:
        raise ValueError("unsupported native skin declaration")
    return zlib.crc32(b"".join(struct.pack("<4i", *a) for a in attrs)) & 0xffffffff


def condition(expression, constants):
    expression = re.sub(r"\$(\w+)", r"v_\1", expression)
    expression = expression.replace("&&", " and ").replace("||", " or ")
    expression = re.sub(r"!(?!=)", " not ", expression).strip()
    def visit(node):
        if isinstance(node, ast.Constant) and type(node.value) is int:
            return node.value
        if isinstance(node, ast.Name) and node.id.startswith("v_"):
            name = "$" + node.id[2:]
            if name in constants:
                return constants[name]
        if isinstance(node, ast.UnaryOp):
            if isinstance(node.op, ast.Not): return not visit(node.operand)
            if isinstance(node.op, ast.USub): return -visit(node.operand)
        if isinstance(node, ast.BoolOp):
            items = [bool(visit(v)) for v in node.values]
            return all(items) if isinstance(node.op, ast.And) else any(items)
        if isinstance(node, ast.Compare) and len(node.ops) == 1:
            a, b = visit(node.left), visit(node.comparators[0])
            op = node.ops[0]
            if isinstance(op, ast.Eq): return a == b
            if isinstance(op, ast.NotEq): return a != b
            if isinstance(op, ast.Lt): return a < b
            if isinstance(op, ast.LtE): return a <= b
            if isinstance(op, ast.Gt): return a > b
            if isinstance(op, ast.GtE): return a >= b
        raise ValueError(f"unsupported/default-unresolved condition: {expression}")
    try:
        return bool(visit(ast.parse(expression, mode="eval").body))
    except SyntaxError as exc:
        raise ValueError(f"invalid condition: {expression}") from exc


def selected_draws(sec, name, *, static_constants=None):
    constants = {m[1]: int(m[2]) for m in re.finditer(
        r"^\s*global\s+persist\s+(\$\w+)\s*=\s*(-?\d+)\s*$", sec.get("Constants", ""), re.M)}
    if static_constants is not None:
        constants.update(static_constants)
    buffers, textures, draws = {}, {}, []
    def walk(command, ancestors=()):
        if command in ancestors or len(ancestors) >= 16:
            raise ValueError("recursive command list")
        if command not in sec:
            raise ValueError(f"missing command list: {command}")
        stack, active = [], True
        for original in sec[command].splitlines():
            line = original.split(";", 1)[0].strip()
            if not line: continue
            if line.startswith("if "):
                chosen = condition(line[3:], constants)
                stack.append([active, chosen]); active = active and chosen
                continue
            if line.startswith("elif ") or line.startswith("else if ") or line == "else":
                if not stack: raise ValueError("unmatched branch")
                parent, taken = stack[-1]
                chosen = True if line == "else" else condition(line.split(" ", 1)[1].removeprefix("if "), constants)
                active = parent and not taken and chosen
                stack[-1][1] |= chosen
                continue
            if line == "endif":
                if not stack: raise ValueError("unmatched endif")
                active = stack.pop()[0]; continue
            if not active: continue
            if m := re.fullmatch(r"(vb[0-3]|ib)\s*=\s*(?:ref\s+)?(Resource\S+)", line):
                buffers[m[1]] = m[2]; continue
            # Some EFMI exports spell the optional fourth vertex slot as an
            # explicit alias (`vb3=vb0`).  It is safe only when vb0 has
            # already been bound; normalize that alias to the same resource
            # and let the downstream layout checks prove the rest.
            if re.fullmatch(r"vb3\s*=\s*vb0", line, re.I):
                if "vb0" not in buffers:
                    raise ValueError("unsupported draw statement: vb3=vb0 before vb0 binding")
                buffers["vb3"] = buffers["vb0"]
                continue
            if m := re.fullmatch(r"(ps-t\d+|Resource\\RabbitFX\\\w+)\s*=\s*(?:ref\s+)?(Resource\S+)", line, re.I):
                textures[m[1].lower()] = m[2]; continue
            if m := re.fullmatch(r"run\s*=\s*(\S+)", line):
                if m[1].lower() in ("commandlist\\efmiv1\\overridetextures", "commandlist\\rabbitfx\\settextures"):
                    continue  # Explicit profile maps every resulting texture state.
                if m[1].startswith("CommandList") and "\\" not in m[1]:
                    walk(m[1], ancestors + (command,)); continue
                raise ValueError(f"unsupported draw command: {line}")
            if m := re.fullmatch(r"drawindexedinstanced\s*=\s*(\d+)\s*,\s*INSTANCE_COUNT\s*,\s*(\d+)\s*,\s*(-?\d+)\s*,\s*FIRST_INSTANCE", line):
                draws.append({"count": int(m[1]), "start": int(m[2]), "base": int(m[3]),
                              "buffers": dict(buffers), "bindings": dict(textures)})
                continue
            if m := re.fullmatch(r"drawindexed\s*=\s*(\d+)\s*,\s*(\d+)\s*,\s*(-?\d+)", line):
                draws.append({"count": int(m[1]), "start": int(m[2]), "base": int(m[3]),
                              "buffers": dict(buffers), "bindings": dict(textures)})
                continue
            raise ValueError(f"unsupported draw statement: {line}")
        if stack: raise ValueError("unclosed draw conditional")
    walk(name)
    return draws


def skeleton_ranges(sec):
    """Read the EFMIv1 explicit group ranges, never infer bone semantics."""
    current, groups = None, {}
    for line in sec.get("CommandListInitializeMergedSkeleton", "").splitlines():
        if m := re.fullmatch(r"\s*\$component_id\s*=\s*(\d+)\s*", line):
            current = int(m[1])
        if m := re.fullmatch(r"\s*\$Pool_MergedSkeleton_Component_VertexGroup(Offsets|Counts)\[\$component_id\]\s*=\s*(\d+)\s*", line):
            if current is None: raise ValueError("skeleton range without component ID")
            group = groups.setdefault(current, {})
            if m[1] in group: raise ValueError("duplicate skeleton range")
            group[m[1]] = int(m[2])
    result, used = {}, set()
    for key, group in groups.items():
        if set(group) != {"Offsets", "Counts"} or group["Counts"] <= 0:
            raise ValueError("incomplete skeleton range")
        start, count = group["Offsets"], group["Counts"]
        if start + count > 65536: raise ValueError("skeleton range exceeds uint16")
        slots = set(range(start, start + count))
        if slots & used: raise ValueError("overlapping merged skeleton ranges")
        used |= slots
        result[key] = (start, count)
    return result


def texture_overrides(sec):
    """Global texture replacements are separate from per-draw ps-t assignments."""
    result = []
    for name, body in sec.items():
        if not name.lower().startswith('textureoverride') or ENTRY.fullmatch(name):
            continue
        lines = [line.split(';', 1)[0].strip() for line in body.splitlines()]
        if not any(re.match(r'this\s*=', line, re.I) for line in lines):
            raise ValueError(f'unsupported non-draw TextureOverride: {name}')
        fields = {}
        for line in filter(None, lines):
            match = re.fullmatch(r'(hash|match_priority|this)\s*=\s*(.+)', line, re.I)
            if not match or match[1].lower() in fields:
                raise ValueError(f'unsupported texture override statement: {name}: {line}')
            fields[match[1].lower()] = match[2]
        identity = fields.get('hash', '').lower()
        resource = fields.get('this', '').removeprefix('ref ')
        if not re.fullmatch('[0-9a-f]{8}', identity) or resource not in sec:
            raise ValueError(f'invalid texture override identity/resource: {name}')
        if any(row['hash'] == identity for row in result):
            raise ValueError(f'ambiguous texture override hash: {identity}')
        result.append({'section': name, 'hash': identity, 'resource': resource})
    return result


def convert(src, ini, profile, *, max_output_bytes=512 * 1024 * 1024):
    if not 512 * 1024 * 1024 <= max_output_bytes <= 1024 * 1024 * 1024:
        raise ValueError("Unsupported transient BEMPC25 carrier limit")
    if profile.get("schema") != 2 or not profile.get("evidence") or profile.get("verified") is not True:
        raise ValueError("requires verified schema=2 native profile with evidence")
    natives = {int(k): v for k, v in profile["components"].items()}
    if not natives or len(natives) > 64 or set(natives) != set(range(len(natives))):
        raise ValueError("native component IDs must be contiguous, 0..63")
    for native in natives.values():
        if not native.get("mesh_name") or not native.get("evidence"):
            raise ValueError("native component identity/evidence missing")
        count = native["original_index_count"]
        if type(count) is not int or not 0 < count <= 0xffffffff or count % 3:
            raise ValueError("invalid native original index count")
        if any(type(s) is not int or not 0 < s <= 64 for s in native["strides"]):
            raise ValueError("invalid native stream stride")
        layout_crc(native)
    sec = sections(src.read_exact(ini).decode("utf-8-sig"))
    for override in texture_overrides(sec):
        mapped = profile.get('texture_overrides', {}).get(override['hash'], {})
        if mapped.get('resource') != override['resource'] or not mapped.get('evidence'):
            raise ValueError(f"unmapped global texture override: {override['hash']}")
        if mapped.get('disposition') == 'preserve_native':
            if not mapped.get('reason'):
                raise ValueError('preserved global texture override requires a reason')
        elif mapped.get('disposition') == 'material_rules':
            textures = [t for e in profile['entries'].values() for r in e['material_rules']
                        for t in r.get('textures', [])]
            if not mapped.get('original_texture') or not any(
                t['resource'] == override['resource'] and t['original_texture'] == mapped['original_texture']
                for t in textures):
                raise ValueError('global texture override is not delivered by material rules')
        else:
            raise ValueError('global texture override requires an explicit disposition')
    root = PurePosixPath(ini).parent
    def resource(name):
        decl = sec.get(name, {})
        filename = value(decl, "filename") if decl else None
        if not filename: raise ValueError(f"resource has no filename: {name}")
        path = PurePosixPath(filename.replace("\\", "/"))
        if path.is_absolute() or ".." in path.parts or ":" in str(path):
            raise ValueError("invalid source-relative resource path")
        exact = str(root / path)
        if exact not in src.names: raise ValueError(f"missing {exact}")
        return src.read_exact(exact), decl, exact

    ranges = skeleton_ranges(sec)
    palette_sources = {}
    for key, mapping in profile.get("skeleton_groups", {}).items():
        group = int(key)
        if group not in ranges: raise ValueError("profile group absent from source")
        start, count = ranges[group]
        component = int(mapping["component"])
        local = mapping["indices"]
        if component not in natives or len(local) != count or not mapping.get("evidence"):
            raise ValueError("invalid verified skeleton group mapping")
        for i, index in enumerate(local):
            names = natives[component]["bone_names"]
            if not 0 <= index < len(names) or not names[index]: raise ValueError("missing bone identity")
            palette_sources[start + i] = (component, index, crc(names[index]))

    outputs, texture_entries, texture_keys = {}, [], {}
    for section, body in sec.items():
        entry = ENTRY.fullmatch(section)
        if not entry or int(entry[1]) != 0: continue
        if entry[2] not in profile["entries"]: raise ValueError(f"unmapped LOD0 entry: {entry[2]}")
        mapping = profile["entries"][entry[2]]
        cid = int(mapping["component"]); native = natives[cid]
        entry_palette = palette_sources
        local = mapping.get('local_bones')
        if local is not None:
            if ranges or profile.get('skeleton_groups'):
                raise ValueError('local bone mapping cannot be combined with merged skeleton groups')
            donor = local.get('component')
            indices = local.get('indices')
            if type(donor) is not int or donor not in natives or not local.get('evidence') or not isinstance(indices, list) or not indices:
                raise ValueError('invalid local bone mapping')
            names = natives[donor]['bone_names']
            if len(indices) > 65536 or any(type(i) is not int or not 0 <= i < len(names) or not names[i] for i in indices):
                raise ValueError('invalid local bone identity')
            entry_palette = {i: (donor, index, crc(names[index])) for i, index in enumerate(indices)}
        if cid in outputs: raise ValueError("multiple entry points for one native component")
        if int(entry[3]) != native["original_index_count"] or value(body, "match_index_count") != entry[3]:
            raise ValueError("source identity/index count mismatch")
        if value(body, "hash") != entry[2] or value(body, "match_first_index") != entry[4]:
            raise ValueError("source entry identity mismatch")
        callback = re.search(r"Callback_Component_DrawCustom\s*=\s*ref\s+(\S+)", body)
        if not callback: raise ValueError("conversion requires an explicit draw callback; custom GPU entry unsupported")
        draws = selected_draws(sec, callback[1])
        if not draws: raise ValueError("selected draw set is empty; explicit hide mapping required")
        streams = [bytearray(), bytearray(), bytearray()]
        ib, draw_table, bones, bone_ids, vertex_ids = [], [], [], {}, {}
        strides = native["strides"]
        if len(strides) != 3 or strides[2] not in (4, 12): raise ValueError("unsupported native skin layout")
        if mapping.get("skin") not in ("unorm16x4_uint16x4", "unorm16x4_uint8x4", "rigid_uint8x4"):
            raise ValueError("explicit input skin declaration required")
        for draw in draws:
            rules = [r for r in mapping["material_rules"] if r["bindings"] == draw["bindings"]]
            if len(rules) != 1: raise ValueError(f"C{cid}: unresolved material state {draw['bindings']}")
            rule = rules[0]
            if not rule.get("evidence"): raise ValueError("material rule requires evidence")
            preserved = rule.get("preserve_resources", {})
            if any(not evidence for evidence in preserved.values()):
                raise ValueError("preserved resource requires evidence")
            handled = {t["resource"] for t in rule.get("textures", [])} | set(preserved)
            if set(draw["bindings"].values()) - handled:
                raise ValueError("material rule silently drops an explicit texture binding")
            donor, slot = int(rule["component"]), int(rule["slot"])
            if donor not in natives or not 0 <= slot < min(256, len(natives[donor]["materials"])):
                raise ValueError("invalid donor material slot")
            material_name = natives[donor]["materials"][slot]
            if not material_name: raise ValueError("missing donor material identity")
            repeated_slots = rule.get('repeat_material_slots', [])
            if repeated_slots and len(draws) != 1:
                raise ValueError('repeated material slots require one source draw to preserve draw order')
            if (len(set(repeated_slots)) != len(repeated_slots) or slot in repeated_slots or
                    any(type(s) is not int or not 0 <= s < min(256, len(natives[donor]['materials']))
                        or not natives[donor]['materials'][s] for s in repeated_slots)):
                raise ValueError('invalid repeated material slot')
            texture_mask = 0
            for tex in rule.get("textures", []):
                key = (tex["resource"], tex["original_texture"])
                if key not in texture_keys:
                    raw, _, filename = resource(tex["resource"])
                    width, height, mips, fmt, srgb, data = parse_dds(filename, raw)
                    texture_keys[key] = len(texture_entries)
                    texture_entries.append({"width": width, "height": height, "mips": mips, "format": fmt,
                                            "srgb": srgb, "data": data, "pin": crc(tex["original_texture"]), "mask": 0})
                t = texture_keys[key]
                if t >= 32 or cid >= 32: raise ValueError("texture binding limit exceeded")
                texture_entries[t]["mask"] |= 1 << cid
                texture_mask |= 1 << t
            bufs = [resource(draw["buffers"][f"vb{i}"]) for i in range(3)]
            input_strides = [int(value(d, "stride")) for _, d, _ in bufs]
            expected_skin = {"unorm16x4_uint16x4": 16, "unorm16x4_uint8x4": 12, "rigid_uint8x4": 4}[mapping["skin"]]
            if input_strides != mapping["input_strides"] or input_strides[:2] != strides[:2] or input_strides[2] != expected_skin:
                raise ValueError("input/native vertex declarations require an unsupported adaptation")
            sizes = [len(raw) // stride for (raw, _, _), stride in zip(bufs, input_strides)]
            if len(set(sizes)) != 1 or any(len(raw) % stride for (raw, _, _), stride in zip(bufs, input_strides)):
                raise ValueError("vertex buffer length mismatch")
            raw_ib, decl, _ = resource(draw["buffers"]["ib"])
            code = {"R16_UINT": "H", "R32_UINT": "I"}.get((value(decl, "format") or "").removeprefix("DXGI_FORMAT_"))
            if code is None or len(raw_ib) % struct.calcsize(code): raise ValueError("invalid IB")
            all_indices = [v[0] for v in struct.iter_unpack("<" + code, raw_ib)]
            start, count = draw["start"], draw["count"]
            if not count or count % 3 or start + count > len(all_indices): raise ValueError("invalid draw range")
            output_start = len(ib)
            for original in all_indices[start:start + count]:
                original += draw["base"]
                if not 0 <= original < sizes[0]: raise ValueError("draw vertex out of range")
                key = (tuple(draw["buffers"][f"vb{i}"] for i in range(3)), original)
                if key not in vertex_ids:
                    out_index = len(vertex_ids)
                    if out_index > 65535 and profile.get('allow_uint32_indices') is not True:
                        raise ValueError("selected geometry exceeds 16-bit index limit")
                    if out_index >= 1 << 20: raise ValueError('selected geometry exceeds native vertex limit')
                    vertex_ids[key] = out_index
                    for s in range(2):
                        stride = input_strides[s]
                        streams[s].extend(bufs[s][0][original * stride:(original + 1) * stride])
                    skin = bufs[2][0][original * expected_skin:(original + 1) * expected_skin]
                    weights = (65535, 0, 0, 0) if expected_skin == 4 else struct.unpack_from("<4H", skin)
                    source_bones = struct.unpack_from("<4H" if expected_skin == 16 else "<4B", skin, 8 if expected_skin != 4 else 0)
                    if abs(sum(weights) - 65535) > 655: raise ValueError("invalid skin weight sum")
                    mapped = []
                    for weight, source_index in zip(weights, source_bones):
                        if not weight: mapped.append(0); continue
                        if source_index not in entry_palette: raise ValueError(f"unmapped source bone {source_index}")
                        identity = entry_palette[source_index]
                        if identity not in bone_ids:
                            if len(bones) == 256: raise ValueError("selected mesh needs more than 256 native bone slots")
                            bone_ids[identity] = len(bones); bones.append(identity)
                        mapped.append(bone_ids[identity])
                    if strides[2] == 4:
                        if sum(w > 0 for w in weights) != 1 or weights[0] != 65535:
                            raise ValueError("weighted mesh cannot use rigid native layout")
                        streams[2].extend(bytes(mapped))
                    else:
                        streams[2].extend(struct.pack("<4H4B", *weights, *mapped))
                ib.append(vertex_ids[key])
            draw_table.append((output_start, count, donor, slot, crc(material_name), texture_mask))
            # Unity can render one submesh with multiple ordered material slots.
            # BEM uses one slot per draw: repeat the index range, share vertices/bones.
            for extra_slot in repeated_slots:
                extra_start = len(ib)
                ib.extend(ib[output_start:output_start + count])
                draw_table.append((extra_start, count, donor, extra_slot,
                                   crc(natives[donor]['materials'][extra_slot]), texture_mask))
            if len(draw_table)>256:
                raise ValueError("selected mesh exceeds 256 material draws")
        outputs[cid] = (streams, ib, bones, draw_table, len(vertex_ids))
    if not outputs: raise ValueError("no supported LOD0 entries")
    for override in texture_overrides(sec):
        mapped = profile['texture_overrides'][override['hash']]
        if mapped['disposition'] == 'material_rules' and (
                override['resource'], mapped['original_texture']) not in texture_keys:
            raise ValueError('global texture override is only mapped in an unselected material state')
    output = io.BytesIO()
    output.write(FILE_HEADER.pack(b"BEMPC25\0", 25, len(natives), len(texture_entries), 0, 0))
    summary = []
    for cid, native in sorted(natives.items()):
        if cid not in outputs:
            if native.get("unmodified") is not True: raise ValueError(f"missing explicit unmodified disposition for C{cid}")
            output.write(COMPONENT_HEADER.pack(cid, native["original_index_count"], 0, 0, 0, 3, 0, 0, 0, 2, 0, 2, 0))
            continue
        streams, ib, bones, draws, vertices = outputs[cid]
        strides = native["strides"]
        index_size = 4 if vertices > 65536 else 2
        output.write(COMPONENT_HEADER.pack(cid, native["original_index_count"], vertices, len(ib), len(bones)-1,
                                           3, *strides, index_size, len(bones), 0, len(draws)))
        for stream in streams: output.write(stream)
        output.write(struct.pack(f"<{len(ib)}{'I' if index_size == 4 else 'H'}", *ib))
        output.write(struct.pack("<I", layout_crc(native)))
        for bone in bones: output.write(struct.pack("<3I", *bone))
        for draw in draws: output.write(struct.pack("<6I", *draw))
        summary.append({"component": cid, "vertices": vertices, "indices": len(ib), "bones": len(bones), "draws": len(draws)})
    for index, tex in enumerate(texture_entries):
        name = f"texture{index}".encode()
        # TEXTURE_ENTRY writes the name-pin bits as UInt32; native code reads
        # the same bits through int32_t and casts back for name comparisons.
        pin = tex["pin"]
        output.write(TEXTURE_ENTRY.pack(tex["width"], tex["height"], tex["mips"], len(tex["data"]), tex["format"],
                                       tex["srgb"], tex["format"], tex["srgb"], tex["mask"], pin, len(name), 2))
        output.write(name); output.write(tex["data"])
    if len(output.getbuffer()) > max_output_bytes: raise ValueError("BEM package too large")
    return output.getvalue(), {"version": 25, "components": summary, "textures": len(texture_entries)}
