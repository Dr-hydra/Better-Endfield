"""Build a scoped, offline-checked weapon replacement from the real Nait3D source.

The source's global rain override is intentionally excluded and reported.
Windows LOD1 is a separate donor probe, never mislabeled as Android evidence.
"""
from __future__ import annotations

import io
import json
from pathlib import Path
import shutil
import struct
import subprocess
import sys

HERE = Path(__file__).resolve().parent
REPO = HERE.parents[5]
sys.path.insert(0, str(REPO / 'tools/CustomModel'))
import bem_v1 as bem
from bem_tool import check_geometry
from build_bem14_target import build_profile
from convert_efmi_poc import parse_dds
from import_efmi_identities import crc32c, FORMATS
from parse_native_models import parse
from inventory import read_text, sections


def write_json(path, value):
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2), encoding='utf-8')


def identity(texture):
    native = texture['resource_identity']
    dxgi, _ = FORMATS[native['format'], native['color_space']]
    first = native['levels'][0]
    descriptor = [first['width'], first['height'], len(native['levels']), 1, dxgi, 1, 0, 0, 8, 0, 0]
    token = '%08x' % crc32c(int(first['legacy_crc32c'], 16), struct.pack('<11I', *descriptor))
    return token, dict(policy='efmi-dx11-region-texture0-fullchain-v1', descriptor=descriptor,
                       native_texture_id=texture['id'], native_texture_name=texture['name'],
                       direct_runtime_binding_observed=False)


def main():
    graph_path = HERE / 'native-misc-lance-pistol.json'
    graph = json.loads(graph_path.read_text(encoding='utf-8-sig'))
    database = parse(graph)
    objects = {row['id']: row for row in graph['objects']}
    index = json.loads((HERE / 'identity-index-misc-lance-pistol.json').read_text(encoding='utf-8'))
    anchor = next(row for row in index['source_matches'] if row['name'] == 'S_wpn_lance_0006_01_lod0')
    source = Path(next(row['ini'] for row in anchor['source_refs'] if 'CohesiveTraction_GaeBolg' in row['ini']))
    text = read_text(source)
    rows = {row['name']: dict(row['assignments']) for row in sections(text)}

    def resource(name):
        row = rows[name]
        path = (source.parent / row['filename'].replace('\\', '/')).resolve()
        if not path.is_relative_to(source.parent.resolve()):
            raise ValueError('Source resource escapes selected package')
        return path, path.read_bytes()

    buffers = [resource('Resource_Component0_VB0'), resource('Resource_Component0_VB1')]
    _, indices = resource('Resource_Component0_IB')
    assert rows['Resource_Component0_VB0']['stride'] == '40'
    assert rows['Resource_Component0_VB1']['stride'] == '8'
    assert rows['Resource_Component0_IB']['format'] == 'DXGI_FORMAT_R16_UINT'
    count = len(indices) // 2
    vertices = len(buffers[0][1]) // 40
    assert len(indices) == count * 2 and count == 59928
    assert len(buffers[0][1]) == vertices * 40 and len(buffers[1][1]) == vertices * 8
    assert 'drawindexedinstanced = 59928, INSTANCE_COUNT, 0, 0, FIRST_INSTANCE' in text
    source_textures = {}
    for name, values in rows.items():
        if name.startswith('TextureOverride_Texture'):
            reference = values['this'].removeprefix('ref ').strip()
            source_textures[values['hash'].lower()] = (reference, *resource(reference))
    assert set(source_textures) == {'70b367d7', '7135f6c0', '7d330dbd', 'ae53cc49'}
    material = next(row for row in graph['objects'] if row['type'] == 'Material' and row['name'] == 'M_wpn_lance_0006_01')
    mapped = []
    for prop in material['textures']:
        texture = objects.get(prop['texture'].get('id'))
        if not texture:
            continue
        token, evidence = identity(texture)
        if token not in source_textures:
            continue
        reference, path, raw = source_textures[token]
        w, h, mips, fmt, srgb, payload = parse_dds(reference, raw)
        mapped.append(dict(token=token, property=prop['property'], evidence=evidence, path=str(path),
                           texture=dict(width=w, height=h, mips=mips, format=fmt, srgb=srgb, original_name=texture['name']),
                           data=payload))
    assert {row['property'] for row in mapped} == {'_BaseMap', '_BumpMap', '_MetallicGlossMap'}

    # Direct pixel evidence: the source deliberately flattens a global rain map.
    # It is not a weapon material property, so it is not silently attached here.
    from PIL import Image
    rain = source_textures['7d330dbd']
    rain_image = Image.open(io.BytesIO(rain[2])).convert('RGBA')
    rain_extrema = rain_image.getextrema()
    assert rain_extrema == ((127, 127), (126, 126), (0, 0), (32, 32))
    outputs = []
    for lod in (0, 1):
        spec = dict(schema=1, kind='bem-resource-target-spec', target_kind='weapon', id='wpn_lance_0006',
                    profile_id='research.nait3d.gae_bolg.win.lod' + str(lod), revision='1',
                    snapshot=graph['snapshot']['manifest_version'], resources=[dict(id='weapon',
                    asset_path='assets/beyond/dynamicassets/gameplay/prefabs/weapons/wpn_lance_0006.prefab',
                    platforms=['windows-x64'], lod=lod)])
        profile, manifest = build_profile(database, spec)
        assert len(manifest['target']['components']) == 1
        component = manifest['target']['components'][0]
        assert component['renderer_kind'] == 'static' and not component['bone_names']
        mesh = objects[profile['evidence'][0]['mesh_id']]
        token = mesh['resource_identity']['data_crc32c']
        native_indices = mesh['resource_identity']['index_count']
        assert any(row.get('hash', '').lower() == token and int(row.get('match_index_count', -1)) == native_indices for row in rows.values())
        builder = bem.Builder('research.nait3d.gae_bolg.weapon.win.lod' + str(lod),
                              'Nait3D Gae Bolg weapon-only · Windows LOD' + str(lod), 'Nait3D / BEM research adaptation', '0.1', profile['target'])
        builder.m = manifest
        manifest.update(package_id='research.nait3d.gae_bolg.weapon.win.lod' + str(lod),
                        name='Nait3D Gae Bolg weapon-only · Windows LOD' + str(lod),
                        author='Nait3D / BEM research adaptation', version='0.1')
        textures = []
        for row in mapped:
            texture = dict(row['texture'], payload=builder.payload(row['data']))
            if row['property'] == '_BumpMap':
                # Original material's BC5 normal slot samples XY. The source
                # replaces that same DX11 texture with BC7 without changing shader.
                texture.update(semantic='normal', normal_encoding='xy-unorm')
                assert not texture['srgb']
            textures.append(texture)
        manifest['textures'] = textures
        manifest['meshes'] = [dict(renderer_kind='static', vertex_count=vertices, index_size=2, bones=[],
                                  streams=[dict(stride=stride, payload=builder.payload(raw)) for stride, (_, raw) in zip((40, 8), buffers)],
                                  attributes=[[0, 0, 3, 0, 0], [1, 0, 3, 0, 12], [2, 0, 4, 0, 24], [4, 0, 2, 1, 0]],
                                  draws=[dict(indices=builder.payload(indices), count=count, material_component=0, material_slot=0,
                                              material_name=component['materials'][0], textures=list(range(len(textures))))])]
        manifest['component_rules'] = [dict(target=0, candidates=[dict(operation='replace', mesh=0)])]
        geometry = check_geometry(manifest, builder.payloads, 4)
        destination = HERE / 'output' / ('Nait3D-GaeBolg-weapon-only-Windows-LOD' + str(lod) + '.bem')
        destination.parent.mkdir(parents=True, exist_ok=True)
        builder.write(destination)
        packed, payloads = bem.read_package(destination)
        check_geometry(packed, payloads, 4)
        assert packed['target'] == profile['target'] and packed['meshes'][0]['renderer_kind'] == 'static'
        log = destination.with_suffix('.native.txt')
        validator = REPO / 'build/bem14/native/modules/custom_model/Release/BetterEndfield.BemValidate.exe'
        with log.open('w', encoding='utf-8') as output:
            subprocess.run([str(validator), str(destination)], stdout=output, stderr=subprocess.STDOUT, check=True)
        sample = REPO / 'build/bem14/samples' / ('probes' if lod else '') / destination.name
        sample.parent.mkdir(parents=True, exist_ok=True)
        shutil.copyfile(destination, sample)
        write_json(HERE / ('lance-lod' + str(lod) + '.profile.json'), profile)
        write_json(HERE / ('lance-lod' + str(lod) + '.spec.json'), spec)
        outputs.append(dict(file=str(destination), sample=str(sample), platform='windows-x64', lod=lod,
                            purpose='offline-only donor probe' if lod else 'Windows weapon replacement research candidate',
                            runtime_lod_supported=lod == 0,
                            resource='wpn_lance_0006', renderer_path=component['renderer_path'],
                            native_index_hash=token, native_index_count=native_indices,
                            replacement_vertices=vertices, replacement_indices=count, texture_count=len(textures),
                            python_validated=True, native_validated=True, geometry=geometry))
    summary = dict(schema=1, source=str(source), source_files_unchanged=True,
                   scope='Global shared wpn_lance_0006 prefab: affects every game user of this weapon resource; not character-exclusive.',
                   artifact_hashes_computed=False, snapshot=graph['snapshot']['manifest_version'], outputs=outputs,
                   texture_mapping=[{k: v for k, v in row.items() if k != 'data'} for row in mapped],
                   omitted_overrides=[dict(source_hash='7d330dbd', original_name='T_actor_common_rain_01_M',
                      reason='Global rain shader resource, not a serialized weapon material slot; applying it requires a global override contract.',
                      source_decoded_rgba=[127, 126, 0, 32], no_op=False),
                      dict(source_hash='1265a39c', reason='Legacy additional entry hash has no current native match; exact current LOD0/1 hashes are both covered.')],
                   runtime_verified=False, render_verified=False, android_ready=False,
                   limitations=['Scoped geometry and three native material textures only; global rain flattening is intentionally not applied.',
                                'LOD1 output is an offline-only Windows donor probe; current Windows runtime rejects it. It is not an Android package.',
                                'Offline validation does not establish in-game replacement, shared weapon instancing, shader rendering or rollback behavior.'])
    write_json(HERE / 'summary.json', summary)
    print(json.dumps(dict(outputs=outputs, global_rain_applied=False, android_ready=False), ensure_ascii=False))


if __name__ == '__main__':
    main()
