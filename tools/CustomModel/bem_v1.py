"""BEM 1.0 container and migration. See docs/BEM_V1_SPEC.md.

The manifest is data, never executable. Payload IDs are directory indices.
"""
from __future__ import annotations
import copy
import json
import re
import struct
import zlib
from pathlib import Path
import zstandard as zstd

MAGIC = b'BEM\x00PKG\x00'
HEADER = struct.Struct('<8sHHIQQII')
ENTRY = struct.Struct('<IIQQQ')
LIMIT = 512 * 1024 * 1024
ID = re.compile(r'^[A-Za-z0-9][A-Za-z0-9_.-]{0,95}$')
CAPABILITIES = ['native-materials', 'palette-u8', 'indices-u32', 'fixed-appearances']


def require(ok, message):
    if not ok:
        raise ValueError(message)


def identity(value):
    require(isinstance(value, str) and ID.fullmatch(value), f'Invalid stable ID: {value!r}')
    return value


def crc(text):
    return zlib.crc32(text.encode('utf-8'))


def load_json(path):
    return json.loads(Path(path).read_text(encoding='utf-8-sig'))


def atomic_write(path, data):
    import os
    import tempfile
    path = Path(path)
    path.parent.mkdir(parents=True, exist_ok=True)
    fd, tmp = tempfile.mkstemp(dir=path.parent, prefix=path.name + '.', suffix='.tmp')
    try:
        with os.fdopen(fd, 'wb') as f:
            f.write(data)
        os.replace(tmp, path)
    finally:
        if os.path.exists(tmp):
            os.unlink(tmp)


def validate_manifest(m, payload_count):
    # Match native numeric/string bounds before Python indexing (negative indexes
    # must never silently select the last donor or resource).
    def tree(value, depth=0):
        require(depth <= 48, 'Manifest nesting exceeds 48')
        if isinstance(value, dict):
            for v in value.values(): tree(v, depth+1)
        elif isinstance(value, list):
            for v in value: tree(v, depth+1)
        elif type(value) in (int, float):
            require(type(value) is int and 0 <= value <= 0xffffffff, 'Manifest integers must be UInt32')
        elif isinstance(value, str):
            require('\0' not in value, 'NUL in manifest string')
    tree(m)
    require(m['schema'] == 1, 'Unsupported manifest schema')
    for key in ('package_id', 'default_appearance_id'):
        identity(m[key])
    for key in ('name', 'author', 'version'):
        require(isinstance(m[key], str) and 0 < len(m[key].encode('utf-8')) <= 256, f'Invalid {key}')
    require(set(m['required_capabilities']) <= set(CAPABILITIES), 'Unsupported required capability')
    t = m['target']
    for key in ('character_id', 'profile_id', 'revision', 'world_resource', 'ui_resource'):
        identity(t[key])
    require(t['platform'] == 'windows-x64', 'BEM runtime currently supports windows-x64 only')
    require(t['world_resource'] != t['ui_resource'], 'Duplicate resource roots')
    require(0 < len(t['components']) <= 64, 'Expected 1..64 target components')
    for i, c in enumerate(t['components']):
        require(c['id'] == i and c['original_index_count'] > 0 and c['original_index_count'] % 3 == 0,
                'Target IDs must be contiguous and original counts triangular')
        require(isinstance(c['mesh_name'], str) and 0 < len(c['mesh_name']) <= 256, 'Invalid mesh identity')
        require(len(c['bone_names']) <= 65536 and len(c['materials']) <= 256, 'Donor table exceeds runtime limits')
    require(len({c['mesh_name'] for c in t['components']}) == len(t['components']), 'Duplicate mesh identity')
    def ref(n):
        require(type(n) is int and 0 <= n < payload_count, 'Payload reference outside directory')
    for mesh in m['meshes']:
        require(len(mesh['streams']) == 3, 'Three native streams required')
        for s in mesh['streams']:
            ref(s['payload'])
            require(0 < s['stride'] <= 64, 'Unsupported stride')
        ref(mesh['indices'])
        require(mesh['index_size'] in (2, 4), 'Unsupported index size')
        require(0 < mesh['vertex_count'] <= 1048576 and 0 < mesh['index_count'] <= 16777216,
                'Geometry exceeds runtime limits')
        require(0 < len(mesh['bones']) <= 256 and 0 < len(mesh['draws']) <= 256, 'Palette/draw limit exceeded')
        attrs = mesh['attributes']
        require(0 < len(attrs) <= 16, 'Invalid native vertex declaration')
        offsets = [0, 0, 0]
        seen = set()
        sizes = [4, 2, 1, 1, 2, 2, 1, 1, 2, 2, 4, 4]
        for a in attrs:
            require(len(a) == 5, 'Attribute requires semantic/format/dimension/stream/offset')
            semantic, fmt, dim, stream, off = a
            require(0 <= semantic <= 13 and semantic not in seen and 0 <= fmt < len(sizes)
                    and 1 <= dim <= 4 and 0 <= stream < 3 and off == offsets[stream], 'Invalid attribute')
            seen.add(semantic)
            offsets[stream] += sizes[fmt] * dim
        require(offsets == [s['stride'] for s in mesh['streams']], 'Attributes disagree with stream strides')
        require(attrs[-1][:4] == [13, 6, 4, 2], 'Skin indices must be UINT8x4 at end of stream 2')
        skin_attrs = [a for a in attrs if a[3] == 2]
        expected_skin = [[13,6,4,2,0]] if offsets[2] == 4 else [[12,4,4,2,0],[13,6,4,2,8]]
        require(skin_attrs == expected_skin, 'Unsupported native skin declaration')
        for bone in mesh['bones']:
            donor = t['components'][bone['component']]
            require(donor['bone_names'][bone['index']] == bone['name'], 'Bone identity differs from target')
        end = 0
        for d in mesh['draws']:
            require(d['start'] == end and d['count'] > 0 and d['count'] % 3 == 0, 'Draws must partition IB')
            end += d['count']
            require(t['components'][d['material_component']]['materials'][d['material_slot']] == d['material_name'],
                    'Material identity differs from target')
            require(len(set(d['textures'])) == len(d['textures']), 'Duplicate draw texture')
            for n in d['textures']:
                require(0 <= n < len(m['textures']), 'Missing texture binding')
        require(end == mesh['index_count'], 'Draws do not cover IB')
    for t in m['textures']:
        ref(t['payload'])
        require(t['format'] in (4, 10, 12, 25, 26, 27, 63), 'Unsupported texture format')
        require(t['format'] != 63 or t['srgb'] is False, 'R8 texture must be linear')
        require(type(t['srgb']) is bool, 'Texture srgb must be boolean')
        require(isinstance(t['original_name'], str) and 0 < len(t['original_name']) <= 256, 'Missing texture identity')
    appearances = m['appearances']
    require(0 < len(appearances) <= 64, 'Expected 1..64 appearances')
    require(len({identity(a['id']) for a in appearances}) == len(appearances), 'Duplicate appearance ID')
    require(m['default_appearance_id'] in [a['id'] for a in appearances], 'Default appearance missing')
    for a in appearances:
        require(isinstance(a['name'], str) and 0 < len(a['name']) <= 256, 'Invalid appearance name')
        require([c['target'] for c in a['components']] == list(range(len(m['target']['components']))),
                'Appearance must specify every target component in order')
        for c in a['components']:
            require(c['operation'] in ('keep', 'hide', 'replace'), 'Unknown component operation')
            if c['operation'] == 'replace':
                require(0 <= c['mesh'] < len(m['meshes']), 'Missing mesh')
        if 'preview' in a:
            ref(a['preview'])


def write_package(path, manifest, payloads):
    validate_manifest(manifest, len(payloads))
    manifest = copy.deepcopy(manifest)
    unique, mapping = [], []
    for data in payloads:
        index = next((i for i, old in enumerate(unique) if old == data), None)
        if index is None:
            index = len(unique); unique.append(data)
        mapping.append(index)
    for mesh in manifest['meshes']:
        mesh['indices'] = mapping[mesh['indices']]
        for stream in mesh['streams']:
            stream['payload'] = mapping[stream['payload']]
    for texture in manifest['textures']:
        texture['payload'] = mapping[texture['payload']]
    for appearance in manifest['appearances']:
        if 'preview' in appearance:
            appearance['preview'] = mapping[appearance['preview']]
    payloads = unique
    raw = json.dumps(manifest, ensure_ascii=False, separators=(',', ':'), allow_nan=False).encode('utf-8')
    require(len(raw) <= 4 * 1024 * 1024 and len(payloads) <= 4096, 'Manifest/directory too large')
    pos = HEADER.size + len(raw) + ENTRY.size * len(payloads)
    entries, blocks = [], []
    compressor = zstd.ZstdCompressor(level=9)
    for data in payloads:
        require(0 < len(data) <= LIMIT, 'Invalid payload length')
        packed = compressor.compress(data)
        codec = 1 if len(packed) < len(data) else 0
        block = packed if codec else data
        entries.append(ENTRY.pack(codec, 0, pos, len(block), len(data)))
        blocks.append(block)
        pos += len(block)
    require(pos <= 2 * 1024**3, 'Package exceeds 2 GiB')
    atomic_write(path, HEADER.pack(MAGIC, 1, 0, HEADER.size, pos, len(raw), len(entries), 0)
                 + raw + b''.join(entries) + b''.join(blocks))


def read_package(path, decode=True):
    with Path(path).open('rb') as f:
        size = f.seek(0, 2); f.seek(0)
        h = f.read(HEADER.size)
        require(len(h) == HEADER.size, 'Truncated BEM header')
        magic, major, minor, hs, fs, ms, count, flags = HEADER.unpack(h)
        require(magic == MAGIC and major == 1 and minor == 0 and hs == HEADER.size and not flags,
                'Unsupported BEM header/version')
        require(fs == size and fs <= 2 * 1024**3 and 0 < ms <= 4 * 1024**2 and count <= 4096,
                'Invalid BEM sizes')
        def no_duplicates(pairs):
            d = {}
            for k, v in pairs:
                require(k not in d, 'Duplicate JSON key')
                d[k] = v
            return d
        m = json.loads(f.read(ms).decode('utf-8'), object_pairs_hook=no_duplicates)
        table = f.read(count * ENTRY.size)
        require(len(table) == count * ENTRY.size, 'Truncated payload directory')
        entries = list(ENTRY.iter_unpack(table))
        end = HEADER.size + ms + len(table)
        for codec, reserved, off, stored, decoded in entries:
            require(codec in (0, 1) and not reserved and off == end and 0 < stored <= LIMIT
                    and 0 < decoded <= LIMIT and stored <= fs - off, 'Invalid payload directory')
            require(codec or stored == decoded, 'Raw payload size mismatch')
            end += stored
        require(end == fs, 'Trailing/missing package bytes')
        validate_manifest(m, count)
        payloads = []
        if decode:
            require(sum(e[4] for e in entries) <= 2 * 1024**3, 'Decoded package exceeds tool budget')
            for codec, _, off, stored, decoded in entries:
                f.seek(off); data = f.read(stored)
                if codec:
                    require(zstd.frame_content_size(data) == decoded, 'Zstd content size mismatch')
                    data = zstd.ZstdDecompressor().decompress(data, max_output_size=decoded, allow_extra_data=False)
                require(len(data) == decoded, 'Decoded length differs')
                payloads.append(data)
        return m, payloads


class Builder:
    def __init__(self, package_id, name, author, version, target):
        self.m = dict(schema=1, package_id=identity(package_id), name=name, author=author, version=version,
                      required_capabilities=CAPABILITIES, target=target, default_appearance_id='',
                      appearances=[], meshes=[], textures=[])
        self.payloads = []

    def payload(self, data):
        # Exact byte equality; no artifact hashes or collision-based identity.
        for i, old in enumerate(self.payloads):
            if old == data:
                return i
        self.payloads.append(bytes(data))
        return len(self.payloads) - 1

    def add_legacy(self, raw, profile, appearance_id, name):
        """Migrate verified v25 or fully described v24; no layout/material guessing."""
        pos = 0
        def take(n):
            nonlocal pos
            require(n >= 0 and n <= len(raw) - pos, 'Truncated legacy package')
            b = raw[pos:pos+n]; pos += n
            return b
        magic, version, nc, nt, _, _ = struct.unpack('<8s5I', take(28))
        require((version, magic) in ((24, b'BEMPC24\0'), (25, b'BEMPC25\0')), 'Expected BEMPC24/25')
        require(nc == len(self.m['target']['components']) and nt <= 32, 'Legacy component/texture count differs')
        operations, pending = [], []
        texture_base = len(self.m['textures'])
        for expected_id in range(nc):
            cid, orig, vc, ic, maxbone, sc, s0, s1, s2, iz, nb, flags, nd = struct.unpack('<13I', take(52))
            native = profile['components'][str(cid)]
            require(cid == expected_id and orig == native['original_index_count'], 'Legacy target mismatch')
            operation = 'hide' if flags & 1 else 'keep' if flags & 2 else 'replace'
            op = dict(target=cid, operation=operation)
            if flags & 2:
                operations.append(op); continue
            require(sc == 3 and [s0,s1,s2] == native['strides'], 'Missing/mismatched verified native strides')
            streams = [dict(stride=s, payload=self.payload(take(vc*s))) for s in (s0,s1,s2)]
            indices = self.payload(take(ic*iz))
            attrs, offsets = [], [0,0,0]
            sizes = [4,2,1,1,2,2,1,1,2,2,4,4]
            for a in native['attributes']:
                sem, fmt, dim, stream = a
                attrs.append([*a, offsets[stream]])
                offsets[stream] += sizes[fmt]*dim
            bones, draws = [], []
            if version == 25:
                layout, = struct.unpack('<I', take(4))
                require(layout == zlib.crc32(b''.join(struct.pack('<4i', *a) for a in native['attributes'])),
                        'Legacy declaration differs from supplied profile')
                for _ in range(nb):
                    donor, index, pin = struct.unpack('<3I', take(12))
                    bn = profile['components'][str(donor)]['bone_names'][index]
                    require(crc(bn) == pin, 'Legacy bone name differs')
                    bones.append(dict(component=donor, index=index, name=bn))
                for _ in range(nd):
                    start, count, donor, slot, pin, mask = struct.unpack('<6I', take(24))
                    mn = profile['components'][str(donor)]['materials'][slot]
                    require(crc(mn) == pin, 'Legacy material name differs')
                    draws.append(dict(start=start, count=count, material_component=donor, material_slot=slot,
                                      material_name=mn, textures=[texture_base+t for t in range(nt) if mask & (1<<t)]))
            else:
                mapping = native.get('v24_draws')
                require(mapping, f'C{cid}: v24 migration needs explicit v24_draws material/texture mapping')
                bones = [dict(component=cid, index=i, name=native['bone_names'][i]) for i in range(maxbone+1)]
                draws = copy.deepcopy(mapping)
                for d in draws:
                    d['material_name'] = profile['components'][str(d['material_component'])]['materials'][d['material_slot']]
                    d['textures'] = [texture_base+t for t in d['textures']]
            mesh = dict(vertex_count=vc, index_count=ic, index_size=iz, streams=streams, indices=indices,
                        attributes=attrs, bones=bones, draws=draws)
            if operation == 'replace':
                op['mesh'] = len(self.m['meshes']); self.m['meshes'].append(mesh)
            operations.append(op)
        # Names are supplied by reviewed material rules; CRC alone is not a v1 identity.
        names = set(profile.get('texture_names', []))
        for entry in profile.get('entries', {}).values():
            for rule in entry.get('material_rules', []):
                names.update(t['original_texture'] for t in rule.get('textures', []))
        for _ in range(nt):
            w,h,mips,size,fmt,srgb,sfmt,ssrgb,mask,pin,nl,kind = struct.unpack('<4I4iIi2I', take(48))
            take(nl)
            matches = [n for n in names if crc(n) == (pin & 0xffffffff)]
            require(kind == 2 and len(matches) == 1, 'Migration requires unambiguous original texture name')
            self.m['textures'].append(dict(width=w,height=h,mips=mips,format=fmt,srgb=bool(srgb),
                                           original_name=matches[0],payload=self.payload(take(size))))
        require(pos == len(raw), 'Legacy package has trailing bytes')
        self.m['appearances'].append(dict(id=identity(appearance_id), name=name, description='', components=operations))
        if not self.m['default_appearance_id']:
            self.m['default_appearance_id'] = appearance_id

    def write(self, path):
        write_package(path, self.m, self.payloads)


def target_from_profile(profile, character_id, world, ui, profile_id, revision):
    require(profile.get('verified') is True, 'A verified native profile is required')
    components = []
    for key, c in sorted(profile['components'].items(), key=lambda kv: int(kv[0])):
        components.append(dict(id=int(key), mesh_name=c['mesh_name'], original_index_count=c['original_index_count'],
                               bone_names=c['bone_names'], materials=c['materials']))
    return dict(character_id=character_id, platform='windows-x64', profile_id=profile_id, revision=revision,
                snapshot=profile.get('source_snapshot', {}).get('manifest_version', ''),
                world_resource=world, ui_resource=ui, components=components)
