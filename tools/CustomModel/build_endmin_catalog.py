"""Rebuild the reviewed Endmin catalog from local evidence, never from Mod filenames.

Maintainer-only bootstrap for the first catalog. Future full-role export uses
the same catalog schema; this script is not run by players or during conversion.
"""
import argparse
import json
import hashlib
import re
from pathlib import Path

from prepare_native_profile import paired_resource

# Resource identities recorded in CUSTOM_MODEL_RUNTIME_VALIDATION_ENDMIN_20260910.
IDENTITIES = [('638e0992', 'hair_01'), ('4eabed4f', 'face_01'),
    ('5825df15', 'cloth_03'), ('827a29bc', 'cloth_04'), ('586dadd6', 'eyebrow_01'),
    ('fa04a7b6', 'body_01'), ('b1f947ec', 'cloth_05'), ('57f73db4', 'iris_01'),
    ('ca67e19e', 'eyeshadow_01'), ('9cd919fa', 'cloth_01'), ('316571b5', 'cloth_02')]


def build(database, material_evidence, log):
    world, ui = 'chr_0003_endminf_postmodel', 'chr_0003_endminf_uimodel'
    components, entries = {}, {}
    for cid, (identity, part) in enumerate(IDENTITIES):
        name = f'S_actor_endminf_{part}_lod0'
        def renderer(root):
            rows = [r for r in database['renderers'] if r['resource_root'] == root
                    and '/lod0/' in r['path'] and r['mesh_name'] == name]
            if len(rows) != 1: raise ValueError(f'{name}: ambiguous native renderer')
            return rows[0]
        native, other = renderer(world), renderer(ui)
        errors = paired_resource(native, other, database['meshes'])
        if errors: raise ValueError(f'{name}: {errors}')
        # Historical direct observation includes complete bone and material names.
        blocks = re.findall(r'mesh name="'+re.escape(name)+r'".*?(?=renderer\[|=== CustomModel|\Z)', log, re.S)
        bones = [b['name'] for b in native['bones']]
        materials = [m['name'] for m in native['materials']]
        if not any(re.findall(r'bone\[\d+\] name="([^"]+)"', b) == bones
                   and re.findall(r'material\[\d+\] name="([^"]+)"', b) == materials for b in blocks):
            raise ValueError(f'{name}: full runtime bone/material evidence missing')
        mesh = database['meshes'][native['mesh_id']]
        if len(mesh['submeshes']) != 1: raise ValueError('Only reviewed single-submesh renderers')
        attrs, streams, strides = [], [{'semantics': []} for _ in range(3)], [0, 0, 0]
        # This restricted translation is backed by C9's full descriptor capture
        # and all eleven successful raw-stream uploads. It is not a generic HG
        # dimension-bit decoder and must not approve unknown channel patterns.
        rules = {(0, 0, 0, 3): ('POSITION', 'R32G32B32_FLOAT', 12, 3),
                 (1, 0, 0, 49): ('ENCODEDDATA', 'R32_UINT', 4, 1),
                 (4, 1, 0, 2): ('TEXCOORD', 'R32G32_FLOAT', 8, 2),
                 (6, 1, 3, 36): ('COLOR', 'R8G8B8A8_SNORM', 4, 4),
                 (12, 2, 4, 4): ('BLENDWEIGHTS', 'R16G16B16A16_UNORM', 8, 4),
                 (13, 2, 6, 4): ('BLENDINDICES', 'R8G8B8A8_UINT', 4, 4),
                 (13, 2, 6, 20): ('BLENDINDICES', 'R8G8B8A8_UINT', 4, 4)}
        for a in mesh['serialized_layout']['channels']:
            if not a['dimension_raw']: continue
            semantic, stream, fmt, raw = (a[k] for k in ('attribute', 'stream', 'format', 'dimension_raw'))
            label, encoding, size, dim = rules[(semantic, stream, fmt, raw)]
            if a['offset'] != strides[stream]: raise ValueError('Unexpected source padding')
            attrs.append([semantic, fmt, dim, stream]); strides[stream] += size
            streams[stream]['semantics'].append(dict(name=label, index=0, format=encoding, stride=size))
        capture = rf'C{cid} built: vtx=\d+ idx=\d+ strides={"/".join(map(str, strides))} attributes={len(attrs)} uploaded=1'
        layout_verified = bool(re.search(capture, log))
        components[str(cid)] = dict(mesh_name=name, original_index_count=native['original_index_count'],
            bone_names=bones, materials=materials, strides=strides, attributes=attrs,
            source_streams=streams, layout_verified=layout_verified,
            material_textures=[[t['name'] for t in m['textures'] if t['name']] for m in native['materials']],
            evidence=dict(world_renderer=native['id'], ui_renderer=other['id'], mesh_id=native['mesh_id'],
                bones_materials='direct-runtime-names-match-offline', layout='reviewed-channel-rule-and-historical-raw-upload',
                world_ui='same-mesh-bones-materials'))
        if part == 'eyeshadow_01':
            # Early EFMI exports call this opaque four-byte packed channel
            # UNKNOWN/UINT8x4. Historical C8 raw uploads preserved those bytes.
            components[str(cid)]['source_stream_aliases'] = {'0': [{'semantics': [
                streams[0]['semantics'][0], dict(name='UNKNOWN',index=0,format='R8G8B8A8_UINT',stride=4)]}]}
        entries[identity] = cid
    if 'C9 source declaration matches: Normal=Float32x1' not in log:
        raise ValueError('Missing full runtime declaration anchor')
    return dict(schema=1, kind='bem-character-catalog', character_id='chr_0003_endminf', name='女管理员',
        platform='windows-x64', profile_id='endminf-pc', revision='20260919', verified=True,
        world_resource=world, ui_resource=ui,
        source_snapshot={'manifest_version': database['source']['snapshot']['manifest_version']},
        evidence=['CUSTOM_MODEL_RUNTIME_VALIDATION_ENDMIN_20260910.md', 'CUSTOM_MODEL_POC22_RAW_STREAM.md',
                  'CUSTOM_MODEL_RESOURCE_AB_VALIDATION_20260916.md'],
        components=components, entries=entries,
        textures={t['source']: {'name': t['target_texture'], 'evidence': t['evidence']} for t in material_evidence['textures']},
        preserved_globals={t['source']: {'name': t['target_texture'], 'property': t['shader_global']} for t in material_evidence['preserved_global_sources']})


if __name__ == '__main__':
    p = argparse.ArgumentParser(description=__doc__)
    for flag in ('database', 'materials', 'log', 'output', 'native-textures'): p.add_argument('--'+flag, type=Path, required=True)
    a = p.parse_args()
    catalog = build(json.loads(a.database.read_text(encoding='utf-8-sig')),
                    json.loads(a.materials.read_text(encoding='utf-8-sig')), a.log.read_text(encoding='utf-8-sig'))
    textures=json.loads((a.native_textures/'textures.json').read_text(encoding='utf-8-sig'))
    material_evidence=json.loads(a.materials.read_text(encoding='utf-8-sig'))
    for row in material_evidence['preserved_global_sources']:
        t=next(t for t in textures if t['name']==row['target_texture'])
        if t['format']!='BC7': raise ValueError('Unknown global texture encoding')
        offset=0;w=t['width'];h=t['height']
        for _ in range(row['native_mip']):
            offset+=((w+3)//4)*((h+3)//4)*16;w=max(w//2,1);h=max(h//2,1)
        size=((w+3)//4)*((h+3)//4)*16
        data=(a.native_textures/(t['name']+'.tex')).read_bytes()[offset:offset+size]
        if len(data)!=size: raise ValueError('Missing native reference mip')
        # Compatibility fingerprint of these two input resources only; no
        # artifact hashing or native game texture bytes in the distribution.
        catalog['preserved_globals'][row['source']].update(width=w,height=h,mips=1,format=25,
            source_payload_sha256=hashlib.sha256(data).hexdigest())
    a.output.parent.mkdir(parents=True, exist_ok=True)
    a.output.write_text(json.dumps(catalog, ensure_ascii=False, indent=2), encoding='utf-8')
    print(f'Catalog: {len(catalog["components"])} components, {len(catalog["textures"])} texture identities')
