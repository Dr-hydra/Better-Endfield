"""Read official EFMI shape buffers through explicit creator bindings.

No GUI, INI expression, shader, animation or command list is evaluated. A binding
declares the source component, exported vertex correspondence and shape key ID.
"""
import math
import re
import struct
from pathlib import Path, PurePosixPath

import bem_v1 as bem
from convert_efmi_poc import Source
from efmi_source import sections, value


def has_shape_data(src):
    for name in src.names:
        if not name.lower().endswith('.ini') or any(p.lower().startswith('disabled') or
                p.lower() in ('backup', 'backups') for p in PurePosixPath(name).parts): continue
        text = src.read_exact(name).decode('utf-8-sig')
        text = '\n'.join(line.split(';', 1)[0] for line in text.splitlines())
        if re.search(r'ShapeKey(?:BatchConfigs|VertexIds|VertexOffsets)|run\s*=\s*\S*ShapeKeys?', text, re.I):
            return True
    return False


def require_shape_binding(source, spec_path):
    src = Source(Path(source))
    try: shapes = has_shape_data(src)
    finally: src.close()
    bem.require(not shapes or spec_path is not None,
                'SHAPE_BINDING_REQUIRED: 源包含形态数据，请在现有导出工程的 deformations 中显式绑定；不能丢弃滑条后导出静态模型。')
    if shapes:
        spec_path = Path(spec_path).resolve()
        spec = bem.load_json(spec_path)
        bindings = [f['efmi'] for c in spec.get('mesh_deformations', []) for f in c['frames'] if 'efmi' in f]
        bem.require(any((spec_path.parent / b['source']).resolve() == Path(source).resolve() for b in bindings),
                    'SHAPE_BINDING_REQUIRED: 形态配置未显式绑定此源 Mod 的官方 ShapeKey buffers。')


def decode_official(config, ids, offsets, shape_key, vertex_count, vertex_map=None):
    bem.require(type(shape_key) is int and shape_key >= 0, 'Invalid EFMI shape key ID')
    bem.require(len(config) >= (8 + 132) * 4 and (len(config) // 4 - 8) % 132 == 0 and len(config) % 4 == 0,
                'Invalid EFMI shape batch config size')
    words = [v[0] for v in struct.iter_unpack('<I', config)]
    scales = [v[0] for v in struct.iter_unpack('<f', config[:32])]
    bem.require(all(math.isfinite(v) for v in scales), 'Non-finite EFMI quantization metadata')
    batch_count = (len(words) - 8) // 132
    bem.require(batch_count <= 64 and shape_key < batch_count * 127 and len(ids) % 4 == 0,
                'EFMI shape key is outside the declared batches')
    source_ids = [v[0] for v in struct.iter_unpack('<I', ids)]
    bem.require(len(offsets) == len(source_ids) * 6 and len(source_ids) <= 16777216,
                'EFMI shape offset/vertex buffer sizes disagree')
    expected = 0
    for b in range(batch_count):
        start = 8 + b * 132
        base, table = words[start], words[start + 4:start + 132]
        bem.require(base == expected and table[0] == 0 and table == sorted(table) and
                    table[-1] <= len(source_ids) - base, 'Invalid EFMI shape batch offsets')
        expected += table[-1]
    bem.require(expected == len(source_ids), 'Unreferenced EFMI shape records')
    batch, local = divmod(shape_key, 127); start = 8 + batch * 132
    base = words[start]; table = words[start + 4:start + 132]
    first, end = base + table[local], base + table[local + 1]
    records, seen = [], set()
    for i in range(first, end):
        vid = source_ids[i]
        delta = struct.unpack_from('<3e', offsets, i * 6)
        bem.require(vid not in seen and all(math.isfinite(v) for v in delta), 'Invalid EFMI sparse shape record')
        seen.add(vid); records.append((vid, *delta))
    if vertex_map is None:
        bem.require(all(r[0] < vertex_count for r in records), 'EFMI shape vertex outside exported mesh')
        return records
    bem.require(isinstance(vertex_map, list) and len(vertex_map) == vertex_count and
                all(type(i) is int and 0 <= i <= 0xffffffff for i in vertex_map),
                'EFMI vertex_map must give one source vertex ID for every exported mesh vertex')
    lookup = {r[0]: r[1:] for r in records}
    bem.require(set(lookup) <= set(vertex_map), 'EFMI shape has vertices missing from explicit vertex_map')
    return [(target, *lookup[source]) for target, source in enumerate(vertex_map) if source in lookup]


def decode_binding(binding, root, vertex_count):
    bem.require(isinstance(binding, dict) and set(binding) <=
                {'source', 'ini', 'component', 'shape_key', 'vertex_order', 'vertex_map'} and
                binding.get('vertex_order') == 'exported',
                'EFMI binding must explicitly declare vertex_order=exported')
    component = binding['component']
    bem.require(type(component) is int and 0 <= component <= 65535, 'Invalid EFMI source component')
    source_path = (Path(root) / binding['source']).resolve()
    src = Source(source_path)
    try:
        ini = src.find(binding['ini'])
        bem.require(ini is not None, 'Missing bound EFMI INI')
        sec = sections(src.read_exact(ini).decode('utf-8-sig'))
        raws, names = [], []
        formats = [('ShapeKeyBatchConfigs', {'r32g32b32a32_uint'}, 16),
                   ('ShapeKeyVertexIds', {'r32_uint'}, 4),
                   ('ShapeKeyVertexOffsets', {'r16_float'}, 2)]
        for suffix, allowed, stride in formats:
            section = 'Resource_Component' + str(component) + '_' + suffix
            bem.require(section in sec, 'Missing official EFMI resource: ' + section)
            body = sec[section]; filename = value(body, 'filename')
            fmt = (value(body, 'format') or '').lower().removeprefix('dxgi_format_')
            bem.require(filename and fmt in allowed and value(body, 'stride') in (None, str(stride)),
                        'Unsupported EFMI shape resource declaration: ' + section)
            name = str(PurePosixPath(ini).parent / filename.replace('\\', '/'))
            raws.append(src.read_exact(name)); names.append(name)
        vertex_map = binding.get('vertex_map')
        if isinstance(vertex_map, str): vertex_map = bem.load_json(Path(root) / vertex_map)
        records = decode_official(*raws, binding['shape_key'], vertex_count, vertex_map)
        return records, dict(source=str(source_path), ini=ini, component=component,
                             shape_key=binding['shape_key'], resources=names,
                             binding_mode='explicit-author-map', records=len(records))
    finally: src.close()
