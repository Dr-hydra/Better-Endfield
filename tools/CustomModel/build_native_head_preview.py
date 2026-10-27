"""Build a measured, offline head-preview prototype from NativeAssetReader output.

Keeps exact renderer/material references. This is preview data, never a BEM mod.
Requires Pillow only on the developer machine; astcenc is optional for Android.
"""
import argparse
import copy
import io
import json
from pathlib import Path
import struct
import subprocess
import zipfile

from parse_native_models import parse


def encode_json(value):
    return json.dumps(value, ensure_ascii=False, separators=(',', ':'), allow_nan=False).encode()


def read_payload(root, name):
    if Path(name).name != name or '/' in name or '\\' in name:
        raise ValueError('Payload must be an immediate child of the export directory')
    return (root / name).read_bytes()


def mips(texture, data, limit):
    if texture['format'] not in ('BC7', 'RGBA32'):
        raise ValueError('This measured prototype currently requires BC7 or RGBA32 source textures')
    w, h, offset, first = texture['width'], texture['height'], 0, None
    levels = []
    for level in range(texture['mips']):
        size = w*h*4 if texture['format']=='RGBA32' else ((w + 3) // 4) * ((h + 3) // 4) * 16
        if offset + size > len(data):
            raise ValueError('Truncated original texture mip chain')
        if max(w, h) <= limit:
            if first is None:
                first = level
            levels.append((w, h, data[offset:offset + size]))
        offset += size
        w, h = max(1, w // 2), max(1, h // 2)
    if offset != len(data) or not levels:
        raise ValueError('Unexpected mip extent or no suitable authored mip')
    return first, levels


def bc7_image(w, h, payload):
    from PIL import Image
    header = [124, 0x2100F, h, w, len(payload), 0, 1] + [0] * 11
    header += [32, 4, int.from_bytes(b'DX10', 'little'), 0, 0, 0, 0, 0]
    header += [0x1000, 0, 0, 0, 0]
    dds = b'DDS ' + struct.pack('<31I', *header) + struct.pack('<5I', 98, 3, 0, 1, 0)
    image = Image.open(io.BytesIO(dds + payload))
    return image.convert('RGBA')


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument('--graph', required=True, type=Path)
    ap.add_argument('--payloads', required=True, type=Path)
    ap.add_argument('--character', required=True)
    ap.add_argument('--output', required=True, type=Path)
    ap.add_argument('--astcenc', type=Path)
    args = ap.parse_args()
    if args.output.exists():
        raise ValueError('Output must be a new directory')
    raw = json.loads(args.graph.read_text(encoding='utf-8-sig'))
    database = parse(raw)
    objects = {o['id']: o for o in raw['objects']}
    roots = {args.character + '_uimodel', args.character + '_postmodel'}
    words = ('face', 'hair', 'iris', 'brow', 'eye', 'head', 'mouth', 'teeth')
    renderers = [r for r in database['renderers'] if r['resource_root'] in roots
                 and '/Mesh_all/lod0/' in r['path']
                 and any(w in r['name'].lower() for w in words)]
    if not renderers or any(r['errors'] for r in renderers):
        raise ValueError('Head renderers are absent or contain unresolved references')
    args.output.mkdir(parents=True)
    common, meshes, canonical, geometry_keys = {}, [], {}, []
    for renderer in renderers:
        mid = renderer['mesh_id']
        if mid in canonical:
            continue
        mesh = objects[mid]
        geometry = mesh.get('preview_geometry')
        if not geometry:
            raise ValueError('Required head geometry export is missing: ' + mesh['name'])
        arrays = tuple(read_payload(args.payloads, geometry[k]) for k in ('positions', 'uvs', 'indices'))
        if sum(s['index_count'] for s in mesh['submeshes']) * 4 != len(arrays[2]):
            raise ValueError('Exported triangle list does not match submesh ranges')
        # Exact byte comparison deduplicates world/UI payloads without computing hashes.
        geometry_key = (arrays, encode_json({k: mesh[k] for k in ('vertex_count', 'submeshes', 'bindposes', 'bone_name_hashes')}))
        if geometry_key in geometry_keys:
            canonical[mid] = geometry_keys.index(geometry_key)
            continue
        index = len(meshes)
        canonical[mid] = index
        geometry_keys.append(geometry_key)
        row = {k: mesh[k] for k in ('name', 'vertex_count', 'submeshes', 'bindposes', 'bone_name_hashes')}
        row['index'] = index
        row['payloads'] = {}
        for key, data in zip(('positions', 'uvs', 'indices'), arrays):
            filename = f'meshes/{index}.{key}.bin'
            common[filename] = data
            row['payloads'][key] = filename
        meshes.append(row)
    material_ids = sorted({m['id'] for r in renderers for m in r['materials']})
    full_materials = [objects[mid] for mid in material_ids]
    full_texture_ids = sorted({p['texture']['id'] for m in full_materials for p in m['textures'] if p['texture']['id']})
    base_texture_ids = sorted({p['texture']['id'] for m in full_materials for p in m['textures']
                               if p['property'] == '_BaseMap' and p['texture']['id']})
    transforms = {a['id']: a for a in raw['objects'] if a['type'] == 'Transform'}
    transform_ids = {i for r in renderers for i in r['transform_ids']}
    transform_ids.update(b['id'] for r in renderers for b in r['bones'])
    pending = list(transform_ids)
    while pending:
        parent = transforms[pending.pop()]['parent']['id']
        if parent and parent not in transform_ids:
            transform_ids.add(parent)
            pending.append(parent)
    skeleton = [{**transforms[i], 'name': objects[transforms[i]['game_object']['id']]['name']}
                for i in sorted(transform_ids)]
    source = args.graph.parent / 'source/extraction.json'
    snapshot = json.loads(source.read_text(encoding='utf-8')) if source.exists() else None
    records = []
    variants = [('original-full', 16384, False, True), ('pc-1024', 1024, False, False), ('pc-2048', 2048, False, False)]
    if args.astcenc:
        variants += [('android-1024', 1024, True, False), ('android-2048', 2048, True, False)]
    for name, limit, astc, full in variants:
        files = dict(common)
        texture_rows = []
        ids = full_texture_ids if full else base_texture_ids
        for index, tid in enumerate(ids):
            tex = objects[tid]
            original = read_payload(args.payloads, tex['raw_texture_file'])
            first, levels = mips(tex, original, limit)
            encoded = []
            for level, (w, h, data) in enumerate(levels):
                if astc:
                    folder = args.output / 'encoder-work' / name
                    folder.mkdir(parents=True, exist_ok=True)
                    input_image = folder / f'{index}-{level}.png'
                    output_image = input_image.with_suffix('.astc')
                    bc7_image(w, h, data).save(input_image)
                    result = subprocess.run([str(args.astcenc), '-cs' if tex.get('serialized_color_space')==1 else '-cl', str(input_image), str(output_image), '6x6', '-medium', '-j', '4'],
                                            capture_output=True, text=True)
                    if result.returncode:
                        raise ValueError('ASTC encoder failed: ' + result.stderr[-1000:])
                    data = output_image.read_bytes()
                    if data[:4] != b'\x13\xab\xa1\x5c' or data[4:7] != bytes([6, 6, 1]):
                        raise ValueError('Unexpected ASTC output format')
                    data = data[16:]
                    if len(data) != ((w + 5) // 6) * ((h + 5) // 6) * 16:
                        raise ValueError('Unexpected ASTC block extent')
                encoded.append(data)
            payload = f'textures/{index}.bin'
            files[payload] = b''.join(encoded)
            texture_rows.append({'id': tid, 'name': tex['name'], 'payload': payload,
                                 'sourceWidth': tex['width'], 'sourceHeight': tex['height'],
                                 'width': levels[0][0], 'height': levels[0][1], 'sourceMip': first,
                                 'mips': len(levels), 'format': 'ASTC_6x6' if astc else tex['format'],
                                 'sourceColorSpace': tex.get('serialized_color_space'),
                                 'srgb': tex.get('serialized_color_space') == 1 if 'serialized_color_space' in tex else None, 'bytes': len(files[payload])})
        materials = copy.deepcopy(full_materials)
        if not full:
            for m in materials:
                m['textures'] = [p for p in m['textures'] if p['property'] == '_BaseMap']
        pack = {'schema': 1, 'kind': 'native-head-preview-prototype', 'character': args.character,
                'scope': 'all-material-textures' if full else 'base-color-preview',
                'snapshot': {'manifestVersion': snapshot.get('manifest_version')} if snapshot else None,
                'meshes': meshes, 'textures': texture_rows, 'materials': materials, 'skeleton': skeleton,
                'renderers': [{**r, 'previewMesh': canonical[r['mesh_id']]} for r in renderers],
                'limitations': ['No game shader bytecode or animation clips.', 'Not a BEM replacement package.',
                                'Native preview data must only fill components whose effective rule is keep.',
                                'Base-color packs omit game-specific lighting, normal and effect textures.']}
        files['pack.json'] = encode_json(pack)
        archive = args.output / f'{args.character}-head-{name}.zip'
        with zipfile.ZipFile(archive, 'x', compression=zipfile.ZIP_DEFLATED, compresslevel=9) as z:
            for filename, data in sorted(files.items()):
                z.writestr(filename, data)
        with zipfile.ZipFile(archive) as z:
            if z.testzip() is not None:
                raise ValueError('Archive validation failed')
            for filename, data in files.items():
                if z.read(filename) != data:
                    raise ValueError('Pack round-trip verification failed')
        geometry_bytes = sum(len(v) for v in common.values())
        record = {'variant': name, 'archive': archive.name, 'packedBytes': archive.stat().st_size,
                  'unpackedBytes': sum(len(v) for v in files.values()), 'geometryBytes': geometry_bytes,
                  'textureBytes': sum(t['bytes'] for t in texture_rows), 'metadataBytes': len(files['pack.json']),
                  'meshes': len(meshes), 'renderers': len(renderers), 'textures': len(texture_rows)}
        records.append(record)
        print(json.dumps(record, ensure_ascii=False), flush=True)
    (args.output / 'size-report.json').write_bytes(encode_json(records))


if __name__ == '__main__':
    main()
