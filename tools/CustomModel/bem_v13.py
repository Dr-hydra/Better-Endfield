"""BEM 1.3 author supplied position morphs; no source program is executed."""
from __future__ import annotations

import bisect
import copy
import json
import math
import struct
from pathlib import Path

import bem_v1 as bem
import bem_v11 as options

CAPABILITIES = ('body-parameters', 'mesh-position-deltas')
ENCODING = 'sparse-position-f32'
RECORD = struct.Struct('<I3f')
MAX_PARAMETERS, MAX_CHANNELS, MAX_FRAMES = 64, 4096, 64


def used(m):
    return bool(m.get('parameters') or m.get('mesh_deformations') or
                set(m['required_capabilities']) & set(CAPABILITIES))


def position_attribute(mesh):
    attrs = [a for a in mesh['attributes'] if a[0] == 0]
    bem.require(len(attrs) == 1 and attrs[0][1:3] == [0, 3],
                'Morph positions require Float32 XYZ position attribute')
    return attrs[0]


def validate_manifest(m, payload_count, minor=None):
    bem.require(minor in (None, 3, 4), 'Body parameters require BEM 1.3 or newer')
    params, channels = m.get('parameters', []), m.get('mesh_deformations', [])
    bem.require(isinstance(params, list) and len(params) <= MAX_PARAMETERS, 'Invalid parameters')
    bem.require(isinstance(channels, list) and len(channels) <= MAX_CHANNELS, 'Invalid mesh deformations')
    caps = m['required_capabilities']
    bem.require(not params or 'body-parameters' in caps, 'Body parameter capability mismatch')
    bem.require(not channels or 'mesh-position-deltas' in caps, 'Position delta capability mismatch')
    groups = {g['id']: {c['id'] for c in g['choices']} for g in m['option_groups']}
    by_id = {}
    for p in params:
        bem.require(isinstance(p, dict) and set(p) <=
                    {'id', 'name', 'min', 'max', 'neutral', 'default', 'step', 'available_when'},
                    'Invalid body parameter fields')
        pid = bem.identity(p['id'])
        bem.require(pid not in by_id, 'Duplicate body parameter')
        options._name(p['name'], 'body parameter name')
        bem.require(all(type(p[k]) is int for k in ('min', 'max', 'neutral', 'default', 'step')),
                    'Body parameter ticks must be integers')
        bem.require(0 <= p['min'] < p['max'] <= 1000 and 1 <= p['step'] <= 1000,
                    'Invalid body parameter range/step')
        bem.require(all(p['min'] <= p[k] <= p['max'] and (p[k] - p['min']) % p['step'] == 0
                        for k in ('max', 'neutral', 'default')), 'Unaligned body parameter ticks')
        options._condition(p.get('available_when', True), groups)
        by_id[pid] = p
    pairs = set()
    for channel in channels:
        bem.require(isinstance(channel, dict) and set(channel) == {'mesh', 'parameter', 'frames'},
                    'Invalid mesh deformation fields')
        mid, pid = channel['mesh'], channel['parameter']
        bem.require(type(mid) is int and 0 <= mid < len(m['meshes']) and pid in by_id,
                    'Unknown morph mesh/parameter')
        bem.require((mid, pid) not in pairs, 'Duplicate mesh parameter channel')
        pairs.add((mid, pid))
        mesh, p, frames = m['meshes'][mid], by_id[pid], channel['frames']
        position_attribute(mesh)
        bem.require(isinstance(frames, list) and 2 <= len(frames) <= MAX_FRAMES, 'Invalid morph frames')
        values, neutral_count = [], 0
        for f in frames:
            bem.require(isinstance(f, dict) and type(f.get('value')) is int and
                        p['min'] <= f['value'] <= p['max'] and
                        (f['value'] - p['min']) % p['step'] == 0, 'Invalid morph frame tick')
            values.append(f['value'])
            if 'neutral' in f:
                bem.require(set(f) == {'value', 'neutral'} and f['neutral'] is True and
                            f['value'] == p['neutral'], 'Invalid neutral frame')
                neutral_count += 1
            else:
                bem.require(set(f) == {'value', 'payload', 'count', 'encoding'} and
                            f['encoding'] == ENCODING and type(f['payload']) is int and
                            0 <= f['payload'] < payload_count and type(f['count']) is int and
                            0 <= f['count'] <= mesh['vertex_count'] and f['value'] != p['neutral'],
                            'Invalid position delta frame')
        bem.require(values == sorted(set(values)) and values[0] == p['min'] and
                    values[-1] == p['max'] and neutral_count == 1,
                    'Morph frames must cover min/max and contain one neutral frame')
    options.validate_manifest(m, payload_count, 4 if minor == 4 else 3)


def encode_deltas(records, vertex_count):
    normalized = []
    for record in records:
        bem.require(isinstance(record, (tuple, list)) and len(record) == 4 and
                    type(record[0]) is int and 0 <= record[0] < vertex_count,
                    'Invalid sparse delta vertex')
        bem.require(all(type(v) in (int, float) and math.isfinite(v) for v in record[1:]),
                    'Non-finite position delta')
        try:
            normalized.append(RECORD.unpack(RECORD.pack(*record)))
        except (OverflowError, struct.error) as exc:
            raise ValueError('Position delta outside Float32 range') from exc
    normalized.sort(key=lambda r: r[0])
    bem.require(len({r[0] for r in normalized}) == len(normalized), 'Duplicate sparse delta vertex')
    header = json.dumps(dict(count=len(normalized), encoding=ENCODING), separators=(',', ':')).encode()
    return struct.pack('<I', len(header)) + header + b''.join(RECORD.pack(*r) for r in normalized)


def decode_deltas(raw, count, vertex_count):
    bem.require(len(raw) >= 4, 'Truncated position delta header')
    size = struct.unpack_from('<I', raw)[0]
    bem.require(0 < size <= 4096 and size <= len(raw) - 4, 'Invalid position delta header length')
    def pairs(items):
        result = {}
        for k, v in items:
            bem.require(k not in result, 'Duplicate position delta header key')
            result[k] = v
        return result
    h = json.loads(raw[4:4 + size].decode('utf-8'), object_pairs_hook=pairs)
    bem.require(isinstance(h, dict) and set(h) == {'count', 'encoding'} and
                type(h['count']) is int and h['count'] == count and h['encoding'] == ENCODING and
                len(raw) == 4 + size + count * RECORD.size, 'Position delta payload descriptor mismatch')
    result, seen = [], set()
    for r in RECORD.iter_unpack(raw[4 + size:]):
        bem.require(r[0] < vertex_count and r[0] not in seen and all(math.isfinite(v) for v in r[1:]),
                    'Invalid/non-finite/duplicate position delta')
        seen.add(r[0]); result.append(r)
    return result


def resolve_parameters(m, saved=None, effective=None):
    saved = saved or {}
    pids = {p['id'] for p in m.get('parameters', [])}
    bem.require(set(saved) <= pids, 'Unknown body parameter')
    effective = effective if effective is not None else options.selection_plan(m)['effective']
    result = {}
    for p in m.get('parameters', []):
        tick = saved.get(p['id'], p['default'])
        bem.require(type(tick) is int and p['min'] <= tick <= p['max'] and
                    (tick - p['min']) % p['step'] == 0, 'Invalid body parameter tick')
        result[p['id']] = tick if options.evaluate(p.get('available_when', True), effective) else p['neutral']
    return result


def bracket(frames, value):
    values = [f['value'] for f in frames]
    right = bisect.bisect_left(values, value)
    if right < len(frames) and values[right] == value:
        return [(frames[right], 1.0)]
    left = right - 1
    weight = (value - values[left]) / (values[right] - values[left])
    return [(frames[left], 1.0 - weight), (frames[right], weight)]


def selected_deformations(m, plan, parameters=None):
    values = resolve_parameters(m, parameters, plan['effective'])
    meshes = {c['mesh'] for c in plan['components'] if c['operation'] == 'replace'}
    selected = []
    for channel in m.get('mesh_deformations', []):
        if channel['mesh'] in meshes:
            selected.append((channel, bracket(channel['frames'], values[channel['parameter']])))
    return values, selected


def morph_streams(m, payloads, mesh_id, parameters=None, option_values=None):
    """CPU reference: update XYZ only; retain base TBN, UVs, weights and indices."""
    plan = options.selection_plan(m, option_values)
    _, selected = selected_deformations(m, plan, parameters)
    mesh = m['meshes'][mesh_id]
    result = [bytearray(payloads[s['payload']]) for s in mesh['streams']]
    attr = position_attribute(mesh)
    stream, offset = attr[3], attr[4]
    stride = mesh['streams'][stream]['stride']
    delta = {}
    for channel, frames in selected:
        if channel['mesh'] != mesh_id: continue
        for frame, weight in frames:
            if frame.get('neutral'): continue
            for vid, x, y, z in decode_deltas(payloads[frame['payload']], frame['count'], mesh['vertex_count']):
                old = delta.setdefault(vid, [0.0, 0.0, 0.0])
                for axis, value in enumerate((x, y, z)): old[axis] += value * weight
    for vid, add in delta.items():
        start = vid * stride + offset
        base = struct.unpack_from('<3f', result[stream], start)
        values = tuple(a + b for a, b in zip(base, add))
        bem.require(all(math.isfinite(v) and abs(v) <= 3.4028234663852886e38 for v in values),
                    'Morphed position is outside finite Float32 range')
        struct.pack_into('<3f', result[stream], start, *values)
    return [bytes(s) for s in result]


def check_geometry(m, payloads, minor=None):
    summary = options.check_geometry(m, payloads, minor)
    for channel in m.get('mesh_deformations', []):
        mesh = m['meshes'][channel['mesh']]
        for f in channel['frames']:
            if not f.get('neutral'):
                decode_deltas(payloads[f['payload']], f['count'], mesh['vertex_count'])
    # Base positions must be finite even when every currently selected frame is neutral.
    for mid in {c['mesh'] for c in m.get('mesh_deformations', [])}:
        mesh = m['meshes'][mid]
        a = position_attribute(mesh); s = mesh['streams'][a[3]]
        bem.require(all(math.isfinite(v) for i in range(mesh['vertex_count'])
                        for v in struct.unpack_from('<3f', payloads[s['payload']], i * s['stride'] + a[4])),
                    'Non-finite base morph position')
    summary['required_minor'] = max(3, summary['required_minor'])
    summary['parameters'] = len(m.get('parameters', []))
    summary['mesh_deformations'] = len(m.get('mesh_deformations', []))
    return summary


def _read_positions(path, vertex_count):
    path = Path(path)
    if path.suffix.lower() == '.json':
        points = bem.load_json(path)
        bem.require(isinstance(points, list) and len(points) == vertex_count and
                    all(isinstance(p, list) and len(p) == 3 and all(type(v) in (int, float)
                        and math.isfinite(v) for v in p) for p in points), 'Invalid target positions')
        return points
    raw = path.read_bytes()
    bem.require(len(raw) == vertex_count * 12, 'Target positions must contain Float32 XYZ per exported vertex')
    points = list(struct.iter_unpack('<3f', raw))
    bem.require(all(math.isfinite(v) for p in points for v in p), 'Non-finite target positions')
    return points


def author_input_paths(spec_path):
    """Protect all explicitly named author inputs from output/report collisions."""
    spec_path = Path(spec_path).resolve(); root = spec_path.parent
    spec = bem.load_json(spec_path); result = {spec_path}
    for channel in spec.get('mesh_deformations', []):
        for frame in channel['frames']:
            for key in ('deltas', 'target_positions'):
                if isinstance(frame.get(key), str): result.add((root / frame[key]).resolve())
            if 'efmi' in frame:
                b = frame['efmi']; source = (root / b['source']).resolve(); result.add(source)
                if source.is_dir(): result.update(p.resolve() for p in source.rglob('*') if p.is_file())
                if isinstance(b.get('vertex_map'), str): result.add((root / b['vertex_map']).resolve())
    return result


def apply_author_spec(manifest, payloads, spec_path):
    """Existing export task extension; positions, sparse deltas or official EFMI buffers."""
    if not spec_path: return manifest, payloads, None
    spec_path = Path(spec_path).resolve(); spec = bem.load_json(spec_path)
    bem.require(spec.get('schema') == 1 and spec.get('kind') == 'bem-position-morphs',
                'Invalid deformation author data')
    bem.require(set(spec) <= {'schema', 'kind', 'parameters', 'mesh_deformations'}, 'Unknown deformation author fields')
    m, payloads = copy.deepcopy(manifest), list(payloads)
    bem.require(not m.get('parameters') and not m.get('mesh_deformations'),
                'Package already has shape parameters; edit its existing manifest instead')
    m['parameters'] = copy.deepcopy(spec['parameters']); m['mesh_deformations'] = []
    for cap in CAPABILITIES:
        if cap not in m['required_capabilities']: m['required_capabilities'].append(cap)
    from bem_export import prepare_export
    m, payloads = prepare_export(m, payloads)
    evidence = []
    for authored in spec['mesh_deformations']:
        mid = authored['mesh']
        bem.require(type(mid) is int and 0 <= mid < len(m['meshes']), 'Unknown authored morph mesh')
        mesh = m['meshes'][mid]; vc = mesh['vertex_count']; attr = position_attribute(mesh)
        stream = mesh['streams'][attr[3]]
        base = [struct.unpack_from('<3f', payloads[stream['payload']], i * stream['stride'] + attr[4]) for i in range(vc)]
        channel = dict(mesh=mid, parameter=authored['parameter'], frames=[])
        for frame in authored['frames']:
            if frame.get('neutral'):
                bem.require(set(frame) == {'value', 'neutral'}, 'Neutral author frame has extra inputs')
                channel['frames'].append(copy.deepcopy(frame)); continue
            keys = set(frame) - {'value'}
            bem.require(len(keys) == 1 and keys <= {'deltas', 'target_positions', 'efmi'},
                        'Author frame needs exactly one delta/target/EFMI input')
            if 'target_positions' in frame:
                target = _read_positions(spec_path.parent / frame['target_positions'], vc)
                records = [(i, *(t[k] - b[k] for k in range(3))) for i, (t, b) in enumerate(zip(target, base)) if tuple(t) != tuple(b)]
            elif 'deltas' in frame:
                value = frame['deltas']
                records = bem.load_json(spec_path.parent / value) if isinstance(value, str) else value
            else:
                from efmi_shapes import decode_binding
                records, ev = decode_binding(frame['efmi'], spec_path.parent, vc)
                evidence.append(dict(mesh=mid, parameter=authored['parameter'], value=frame['value'], **ev))
            raw = encode_deltas(records, vc)
            try: pid = payloads.index(raw)
            except ValueError: pid = len(payloads); payloads.append(raw)
            count = len(decode_deltas(raw, len(records), vc))
            channel['frames'].append(dict(value=frame['value'], payload=pid, count=count, encoding=ENCODING))
        m['mesh_deformations'].append(channel)
    bem.validate_manifest(m, len(payloads)); check_geometry(m, payloads)
    return m, payloads, dict(kind='position-morphs', spec=str(spec_path), parameters=len(m['parameters']),
                            channels=len(m['mesh_deformations']), efmi_bindings=evidence,
                            normals='retained-base', render_verified=False)
