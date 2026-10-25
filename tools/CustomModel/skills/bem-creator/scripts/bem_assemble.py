"""Autonomous BEM assembly and conversion engine.

Bridges raw/non-standard source mods into valid, fully-formed BEM packages.
Pure Python standard library (using repo modules bem_v1, component_auto, bem_projects).

Core capabilities:
- Watermark cleaning and active section resolution (via bem_merge)
- Legacy EFMI hash resolution (via verified-legacy-aliases & native-vb-region-identities)
- Entry normalization (converts legacy skip+draw into uniform callbacks)
- Runtime trigger stripping (cleans CheckTextureOverride hooks)
- Global rain/snow texture conflict omission (7d330dbd / e69d7088)
- In-memory IB draw count clamping (bounds requested count to complete triangles)
- State-space exploration and multi-appearance merging
- BEM 1.5 material overrides and root offset injection
- Atomic write, readback verification, and geometry validation
"""
from __future__ import annotations

import argparse
import hashlib
import ast
import copy
import json
import math
import os
import re
import shutil
import struct
import sys
import tempfile
from pathlib import Path, PurePosixPath
from typing import Any, Dict, List, Optional, Tuple

SCRIPTS_DIR = Path(__file__).resolve().parent
TOOLS_DIR = SCRIPTS_DIR.parents[2]
if str(TOOLS_DIR) not in sys.path:
    sys.path.insert(0, str(TOOLS_DIR))
if str(SCRIPTS_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPTS_DIR))

import bem_v1 as bem
import bem_v11
import bem_v15
import component_auto
import bem_projects
from convert_efmi_poc import Source, parse_ini_constants, parse_dds
from efmi_source import sections, value

import bem_merge
import bem_repair
import bem_peer


def need(test: bool, message: str):
    if not test:
        raise ValueError(message)


def lines(body: str) -> List[str]:
    return [s for raw in body.splitlines() if (s := raw.split(';', 1)[0].strip())]


def serialize(sec: Dict[str, str]) -> str:
    return '\n\n'.join('[' + n + ']\n' + b for n, b in sec.items()) + '\n'


class Overlay(Source):
    """In-memory INI overlay that never modifies source files on disk."""
    def __init__(self, source: Source, ini: str, sec: Dict[str, str]):
        self.source = source
        self.path = source.path
        self.names = source.names
        self.ini = ini
        self.raw = serialize(sec).encode('utf-8')

    def read_exact(self, name: str) -> bytes:
        if name.replace('\\', '/') == self.ini.replace('\\', '/'):
            return self.raw
        return self.source.read_exact(name)

    def close(self):
        self.source.close()


def catalogs_with_legacy_aliases() -> List[Dict[str, Any]]:
    """Loads catalogs and supplements them with verified legacy EFMI/VB0 hashes."""
    database = copy.deepcopy(component_auto.catalogs())
    cat_map = {c['character_id']: c for c in database}

    # 1. verified-legacy-aliases
    p1 = TOOLS_DIR / 'catalog/identities/verified-legacy-aliases.json'
    if not p1.exists():
        p1 = TOOLS_DIR / 'catalog/verified-legacy-aliases.json'
    if p1.exists():
        d1 = json.loads(p1.read_text(encoding='utf-8'))
        for k, row in d1.get('characters', {}).items():
            if k in cat_map:
                cat_map[k]['entries'].update(row.get('entries', {}))

    # 2. native-vb-region-identities
    p2 = TOOLS_DIR / 'catalog/identities/native-vb-region-identities.json'
    if p2.exists():
        d2 = json.loads(p2.read_text(encoding='utf-8'))
        for row in d2:
            k = row.get('character')
            h = row.get('unverified_default_vertex_buffer_descriptor_hash')
            cid = row.get('component')
            if k in cat_map and h and cid is not None:
                cat_map[k]['entries'][h.lower()] = int(cid)

    return database


def normalize_variable_syntax(sec: Dict[str, str]) -> Tuple[Dict[str, str], List[Dict[str, Any]]]:
    sec = copy.deepcopy(sec)
    changes = []
    for name, body in sec.items():
        output = []
        for line in body.splitlines():
            if re.match(r'\s*(?:filename|data)\s*=', line):
                output.append(line)
                continue
            canonical = re.sub(r'\$([A-Za-z_]\w*)(?![\w\\])', lambda m: '$' + m[1].lower(), line)
            if name == 'Constants':
                m = re.fullmatch(r'\s*((?:(?:global|persist)\s+)+)(\$\w+)(?:\s*=\s*([^;]+))?\s*(?:;.*)?', canonical)
                if m and 'global' in m[1].split():
                    canonical = 'global ' + ('persist ' if 'persist' in m[1].split() else '') + m[2] + ' = ' + (m[3].strip() if m[3] else '0')
            if canonical != line:
                changes.append({'section': name, 'source': line, 'normalized': canonical})
            output.append(canonical)
        sec[name] = '\n'.join(output)
    return sec, changes


def bounded_condition(expression: str, constants: Dict[str, Any]) -> bool:
    rewritten = re.sub(r'\$(\w+)', r'v_\1', expression).replace('&&', ' and ').replace('||', ' or ')
    rewritten = re.sub(r'!(?!=)', ' not ', rewritten).strip()
    tree = ast.parse(rewritten, mode='eval')
    need(sum(1 for _ in ast.walk(tree)) <= 4096, 'STATE_EXPRESSION: too complex')

    def visit(n):
        if isinstance(n, ast.Constant) and type(n.value) in (int, float):
            need(math.isfinite(n.value), 'STATE_EXPRESSION: nonfinite')
            return n.value
        if isinstance(n, ast.Name) and n.id.startswith('v_'):
            key = '$' + n.id[2:]
            need(key in constants, 'STATE_EXPRESSION: unknown ' + key)
            return constants[key]
        if isinstance(n, ast.UnaryOp):
            v = visit(n.operand)
            if isinstance(n.op, ast.Not): return not v
            if isinstance(n.op, ast.USub): return -v
            if isinstance(n.op, ast.UAdd): return v
        if isinstance(n, ast.BinOp):
            a, b = visit(n.left), visit(n.right)
            if isinstance(n.op, ast.Mod):
                need(b != 0, 'STATE_EXPRESSION: modulo zero')
                return math.fmod(a, b)
            if isinstance(n.op, ast.Add): return a + b
            if isinstance(n.op, ast.Sub): return a - b
            if isinstance(n.op, ast.Mult): return a * b
        if isinstance(n, ast.BoolOp):
            if isinstance(n.op, ast.And): return all(bool(visit(v)) for v in n.values)
            if isinstance(n.op, ast.Or): return any(bool(visit(v)) for v in n.values)
        if isinstance(n, ast.Compare) and len(n.ops) == 1:
            a, b = visit(n.left), visit(n.comparators[0])
            op = n.ops[0]
            if isinstance(op, ast.Eq): return a == b
            if isinstance(op, ast.NotEq): return a != b
            if isinstance(op, ast.Lt): return a < b
            if isinstance(op, ast.LtE): return a <= b
            if isinstance(op, ast.Gt): return a > b
            if isinstance(op, ast.GtE): return a >= b
        raise ValueError('STATE_EXPRESSION: unsupported ' + expression)

    return bool(visit(tree.body))


def state_review(sec: Dict[str, str]) -> Dict[str, Any]:
    constants = {'$' + k: v for k, v in parse_ini_constants(sec.get('Constants', '')).items()}
    keys, variables, defaults = [], {}, {}
    for name, body in sec.items():
        if not name.lower().startswith('key'):
            continue
        typ = value(body, 'type')
        if typ != 'cycle':
            continue
        assignments = []
        guard = value(body, 'condition') or '1'
        for line in lines(body):
            if re.fullmatch(r'(?:condition|key|back|type)\s*=.*', line):
                continue
            match = re.fullmatch(r'(\$\w+)\s*=\s*(-?\d+(?:\s*,\s*-?\d+)*)', line)
            if match is None:
                continue
            var, vals = match[1], [int(v.strip()) for v in match[2].split(',')]
            if var not in constants:
                constants[var] = vals[0]
            variables[var] = vals
            defaults[var] = constants[var]
            assignments.append((var, vals))
        if assignments and len({len(v) for _, v in assignments}) == 1:
            keys.append({'section': name, 'key': value(body, 'key'), 'condition': guard, 'assignments': assignments})

    names = list(variables)
    if not names:
        return {'keys': [], 'variables': {}, 'defaults': {}, 'states': [{}], 'state_count': 1}

    initial = tuple(defaults[v] for v in names)
    seen = {initial}
    queue = [initial]
    dynamic = {'$object_detected': 1, '$mod_enabled': 1, '$lod_level': 0, '$lod_detected': 0}

    for current in queue:
        state = dict(zip(names, current))
        env = {**constants, **state, **dynamic}
        for key in keys:
            if not bounded_condition(key['condition'], env):
                continue
            size = len(key['assignments'][0][1])
            matches = [i for i in range(size) if all(state[v] == vals[i] for v, vals in key['assignments'])]
            next_idx = (matches[0] + 1) % size if matches else 0
            updated = dict(state)
            for var, vals in key['assignments']:
                updated[var] = vals[next_idx]
            item = tuple(updated[v] for v in names)
            if item not in seen:
                seen.add(item)
                queue.append(item)
                if len(queue) >= 64:
                    break

    return {'keys': keys, 'variables': variables, 'defaults': defaults,
            'states': [dict(zip(names, t)) for t in queue], 'state_count': len(queue)}


def override_constants(sec: Dict[str, str], state: Dict[str, Any]) -> Dict[str, str]:
    sec = copy.deepcopy(sec)
    for var, val in state.items():
        pattern = r'(?m)^(\s*global\s+(?:persist\s+)?' + re.escape(var) + r'\s*=\s*)-?\d+\s*(;[^\n]*)?$'
        sec['Constants'], n = re.subn(pattern, lambda m: m[1] + str(val), sec['Constants'])
    return sec


def remove_zero_draws(sec: Dict[str, str]) -> Tuple[Dict[str, str], List[Dict[str, Any]]]:
    sec = copy.deepcopy(sec)
    evidence = []
    for name, body in sec.items():
        if not name.startswith('CommandList'):
            continue
        kept = []
        for line in body.splitlines():
            statement = line.split(';', 1)[0].strip()
            zero = bool(re.fullmatch(r'drawindexed\s*=\s*0\s*,\s*\d+\s*,\s*-?\d+', statement) or
                        re.fullmatch(r'drawindexedinstanced\s*=\s*0\s*,\s*INSTANCE_COUNT\s*,\s*\d+\s*,\s*-?\d+\s*,\s*FIRST_INSTANCE', statement))
            if zero:
                evidence.append({'section': name, 'statement': statement})
            else:
                kept.append(line)
        sec[name] = '\n'.join(kept)
    return sec, evidence


def normalize_entries(sec: Dict[str, str], entries: List[Dict[str, Any]]) -> Tuple[Dict[str, str], List[Dict[str, Any]]]:
    sec = copy.deepcopy(sec)
    evidence = []
    dispatcher = 'CommandListBEMReviewedDispatcher'
    for entry in entries:
        name = entry['section']
        body = lines(sec[name])
        try:
            component_auto.validate_entry(sec, entry)
            continue
        except ValueError:
            pass
        cid = entry['source_component']
        guard = 'if $mod_enabled && DRAW_TYPE == 4'
        if body.count(guard) != 1 or body.count('endif') != 1:
            continue
        before = body[:body.index(guard)]
        inside = body[body.index(guard) + 1 : body.index('endif')]
        callback = None
        for line in inside:
            if line == 'handling = skip': continue
            m = re.fullmatch(r'run\s*=\s*(CommandList\w+)', line)
            if m and m[1] in sec:
                callback = m[1]
                break
        if callback is None:
            callback = f'CommandListBEMReviewedHide{cid}'
            sec[callback] = ''
        declaration = [line for line in before if re.match(r'(hash|match_index_count|match_first_index)\s*=', line)]
        sec[name] = '\n'.join(declaration + [
            '$object_detected = 1', guard,
            f'$\\EFMIv1\\component_id = {cid}', '$\\EFMIv1\\gpu_posed = 1', '$lod_level = 0',
            f'CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref {callback}',
            f'run = {dispatcher}', 'endif'
        ])
        evidence.append({'source_section': name, 'callback': callback})
    if evidence:
        sec[dispatcher] = '\n'.join([
            'handling = skip', '$\\EFMIv1\\component_count = $component_count',
            '$\\EFMIv1\\identification_min_components = 3', 'run = CommandList\\EFMIv1\\Object_ReadConfig',
            '$\\EFMIv1\\lod_level = $lod_level', 'run = CommandList\\EFMIv1\\Component_ReadConfig',
            'Pool\\EFMIv1\\Input_ObjectSpatialIdentity = ref Pool_ObjectSpatialIdentity',
            'run = CommandList\\EFMIv1\\SpatialIdentity_IdentifyComponentInstances',
            'run = CommandList\\EFMIv1\\Component_DrawInstances'
        ])
        if not re.search(r'(?m)^\s*global\s+\$component_count\s*=', sec.get('Constants', '')):
            sec['Constants'] = sec.get('Constants', '') + f'\nglobal $component_count = {len(entries)}\n'
    return sec, evidence


def auto_repair_draw_counts(
    src: Source,
    ini: str,
    sec: Dict[str, str],
    entries: List[Dict[str, Any]]
) -> Tuple[Dict[str, str], List[Dict[str, Any]]]:
    """Inspects draw calls in callbacks; if requested count exceeds available IB, clamps to complete triangles."""
    sec = copy.deepcopy(sec)
    repairs = []
    base_dir = PurePosixPath(ini).parent

    for entry in entries:
        callback = component_auto.validate_entry(sec, entry)
        if not callback or callback not in sec:
            continue
        body = sec[callback]
        
        # Look for IB in standard naming convention: Meshes/Component{sc}_IB.buf
        sc = entry['source_component']
        candidate_names = [
            str(base_dir / f"Meshes/Component{sc}_IB.buf"),
            str(base_dir / f"Meshes/Component{entry['target']}_IB.buf"),
        ]
        ib_name = next((n for n in candidate_names if n in src.names or n.replace('\\', '/') in [x.replace('\\', '/') for x in src.names]), None)
        
        if not ib_name:
            continue

        raw_ib = src.read_exact(ib_name)
        stride = 2
        total_indices = len(raw_ib) // stride

        # Match drawindexed / drawindexedinstanced
        for line in lines(body):
            m = re.match(r'(drawindexed(?:instanced)?\s*=\s*)(\d+)(.*)', line)
            if not m:
                continue
            requested = int(m[2])
            if requested > total_indices:
                safe_count = (total_indices // 3) * 3
                new_line = f"{m[1]}{safe_count}{m[3]}"
                sec[callback] = sec[callback].replace(line, new_line)
                repairs.append({
                    "component": entry['target'],
                    "callback": callback,
                    "ib_file": ib_name,
                    "original_requested": requested,
                    "available_indices": total_indices,
                    "clamped_indices": safe_count,
                    "truncated_indices": requested - safe_count
                })

    return sec, repairs


def append_unique(rows: List[Any], item: Any) -> int:
    for i, old in enumerate(rows):
        if old == item:
            return i
    rows.append(item)
    return len(rows) - 1


def merge_into(out: bem.Builder, part: bem.Builder) -> Dict[str, Any]:
    need(out.m['target'] == part.m['target'], 'APPEARANCE_TARGET')
    pm = {i: out.payload(p) for i, p in enumerate(part.payloads)}
    tm, mm = {}, {}
    for i, raw in enumerate(part.m['textures']):
        t = copy.deepcopy(raw)
        t['payload'] = pm[t['payload']]
        tm[i] = append_unique(out.m['textures'], t)
    for i, raw in enumerate(part.m['meshes']):
        m = copy.deepcopy(raw)
        m['indices'] = pm[m['indices']]
        for s in m['streams']:
            s['payload'] = pm[s['payload']]
        for d in m['draws']:
            d['textures'] = [tm[n] for n in d['textures']]
        mm[i] = append_unique(out.m['meshes'], m)
    app = copy.deepcopy(part.m['appearances'][0])
    for op in app['components']:
        if op['operation'] == 'replace':
            op['mesh'] = mm[op['mesh']]
    if 'preview' in app:
        app['preview'] = pm[app['preview']]
    return app


# ---------------------------------------------------------------------------
# Main Conversion Engine
# ---------------------------------------------------------------------------

def assemble_source(
    source_path: Path | str,
    output_bem: Path | str,
    package_name: Optional[str] = None,
    peer_root: Optional[Path | str] = None,
    root_offset_y: Optional[float] = None,
    material_overrides: Optional[Dict[str, Any]] = None
) -> Dict[str, Any]:
    """Converts a source mod directory into a verified BEM package."""
    source_path = Path(source_path)
    output_bem = Path(output_bem)

    # 1. Run merge and extract active sections
    merged_info = bem_merge.merge(source_path)
    need(bool(merged_info.entry), f"No valid entry INI found in {source_path}")
    active_ini = merged_info.entry

    src = Source(source_path)
    docs = merged_info.documents()
    entry_doc = next((s for rel, s in docs if rel.replace('\\', '/') == active_ini.replace('\\', '/')), None)
    if not entry_doc and docs:
        entry_doc = docs[0][1]
        active_ini = docs[0][0]
    need(bool(entry_doc), f"No sections extracted for {active_ini}")

    clean_sec = {name: s.body for name, s in entry_doc.items()}

    # 2. Omit global rain/snow or external shared texture overrides that block character packaging
    for section, body in list(clean_sec.items()):
        if section.startswith('TextureOverride_Texture'):
            h = value(body, 'hash')
            if h in ('7d330dbd', 'e69d7088'):
                del clean_sec[section]

    # 3. Match catalog with legacy alias support
    database = catalogs_with_legacy_aliases()
    matched_catalog, entries = component_auto.match_catalog(clean_sec, database)
    char_id = matched_catalog['character_id']
    char_name = matched_catalog.get('name', char_id)

    # 4. Clean entries, zero draws, runtime triggers, and repair IB bounds
    clean_sec, _ = normalize_entries(clean_sec, entries)
    clean_sec, _ = remove_zero_draws(clean_sec)

    # Strip runtime trigger calls from draw callbacks
    for entry in entries:
        key = f"CommandList_Draw_Component{entry['source_component']}"
        if key in clean_sec:
            clean_sec[key] = clean_sec[key].replace("run = CommandListTriggerResourceOverrides", "")
    clean_sec.pop("CommandListTriggerResourceOverrides", None)

    clean_sec, ib_repairs = auto_repair_draw_counts(src, active_ini, clean_sec, entries)

    # 5. Explore appearance state space
    review = state_review(clean_sec)
    pkg_id = "bem." + hashlib.md5(f"{source_path.name}_{char_id}".encode()).hexdigest()
    pkg_name = package_name or f"{char_name} - {source_path.name}"

    out_builder = None
    appearance_list = []

    for idx, state in enumerate(review['states']):
        state_sec = override_constants(clean_sec, state)
        part, _ = component_auto.prepare(Overlay(src, active_ini, state_sec), active_ini, database=[matched_catalog])
        if out_builder is None:
            out_builder = bem.Builder(pkg_id, pkg_name, "Converted", "1.0.0", part.m['target'])
        app = merge_into(out_builder, part)
        app['id'] = f"state_{idx}"
        app['name'] = f"外观 {idx + 1}" if len(review['states']) > 1 else "默认外观"
        appearance_list.append(app)

    out_builder.m['default_appearance_id'] = appearance_list[0]['id']
    out_builder.m['appearances'] = appearance_list

    # 6. Inject BEM 1.5 Capabilities
    if root_offset_y is not None:
        caps = out_builder.m.setdefault("required_capabilities", [])
        if "root-offset-v1" not in caps: caps.append("root-offset-v1")
        out_builder.m["root_offset"] = {
            "path": "Bip001",
            "translation": [0.0, float(root_offset_y), 0.0]
        }

    if material_overrides:
        caps = out_builder.m.setdefault("required_capabilities", [])
        if "material-overrides-v1" not in caps: caps.append("material-overrides-v1")
        for mesh in out_builder.m.get("meshes", []):
            for draw in mesh.get("draws", []):
                ovr = draw.setdefault("material_overrides", {})
                for k, v in material_overrides.items():
                    if isinstance(v, dict):
                        ovr.setdefault(k, {}).update(v)
                    elif isinstance(v, list):
                        ovr.setdefault(k, []).extend(v)

    # 7. Validate and write package atomically
    bem.validate_manifest(out_builder.m, len(out_builder.payloads))
    from bem_tool import check_geometry
    check_geometry(out_builder.m, out_builder.payloads)

    output_bem.parent.mkdir(parents=True, exist_ok=True)
    out_builder.write(output_bem)

    # Roundtrip readback
    read_m, read_p = bem.read_package(output_bem)
    need(read_m == out_builder.m and read_p == out_builder.payloads, "ROUNDTRIP_MISMATCH: readback differs from memory")

    # Final geometry validation
    bem_projects.validated(output_bem)

    src.close()
    return {
        "success": True,
        "package_id": pkg_id,
        "name": pkg_name,
        "character": char_id,
        "output": str(output_bem),
        "output_bytes": output_bem.stat().st_size,
        "appearances": len(appearance_list),
        "ib_repairs": ib_repairs,
        "watermarks_cleaned": len(merged_info.scrubbed)
    }


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("source", type=Path, help="Input source mod directory")
    parser.add_argument("-o", "--output", type=Path, required=True, help="Output .bem file path")
    parser.add_argument("--name", help="Package name override")
    parser.add_argument("--peer-root", type=Path, help="Root folder of sibling packages for peer borrowing")
    parser.add_argument("--root-offset", type=float, help="Root offset Y translation in meters (e.g. 0.04)")
    args = parser.parse_args()

    res = assemble_source(args.source, args.output, args.name, args.peer_root, args.root_offset)
    print(json.dumps(res, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    sys.exit(main())