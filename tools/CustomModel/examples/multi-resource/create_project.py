"""Create synthetic BEM 1.4 authoring/native-reader fixtures, never playable assets."""
from __future__ import annotations

import argparse
import copy
import json
from pathlib import Path
import struct
import sys

sys.path.insert(0, str(Path(__file__).resolve().parents[2]))
import bem_v1 as bem
import bem_v13


def fixture():
    target = dict(kind='character', id='chr_synthetic', platform='windows-x64', profile_id='synthetic-1.4',
                  revision='1', snapshot='synthetic-not-game-evidence', resources=[
        dict(id='body', name='body', asset_path='assets/synthetic/body.prefab', platforms=['windows-x64', 'android-arm64'], lod=0),
        dict(id='ultimate', name='ultimate', asset_path='assets/synthetic/ultimate.prefab', platforms=['windows-x64'], lod=0),
        dict(id='weapon', name='weapon', asset_path='assets/synthetic/weapon.prefab', platforms=['windows-x64', 'android-arm64'], lod=1)],
        components=[dict(id=i, resource=rid, renderer_kind=kind, renderer_path='Mesh/' + name, mesh_name=name,
                         original_index_count=3, bone_names=['root'] if kind == 'skinned' else [], materials=['material'])
                    for i, (rid, kind, name) in enumerate([('body', 'skinned', 'shared_mesh'),
                                                          ('ultimate', 'skinned', 'shared_mesh'),
                                                          ('weapon', 'static', 'blade')])])
    b = bem.Builder('synthetic.multi-resource', 'Synthetic multi-resource test', 'BEM tests', '1', target)
    m = b.m
    m.pop('appearances'); m.pop('default_appearance_id')
    m['required_capabilities'] = ['native-materials', 'palette-u8', 'indices-u32', 'composable-options',
                                  'multi-resource-targets', 'static-meshes', *bem_v13.CAPABILITIES]
    m['option_groups'] = [dict(id='detail', name='Detail', default='show',
                               choices=[dict(id='show', name='Show'), dict(id='hide', name='Hide')])]
    m['parameters'] = [dict(id='length', name='Length', min=0, max=1000, neutral=0, default=0, step=1)]
    m['mesh_deformations'] = []
    positions = b.payload(struct.pack('<9f', 0, 0, 0, 1, 0, 0, 0, 1, 0))
    uv = b.payload(struct.pack('<6f', 0, 0, 1, 0, 0, 1))
    skin = b.payload(bytes(12))
    indices = b.payload(struct.pack('<3H', 0, 1, 2))
    for i, component in enumerate(target['components']):
        skinned = component['renderer_kind'] == 'skinned'
        mesh = dict(renderer_kind=component['renderer_kind'], vertex_count=3, index_size=2,
                    streams=[dict(stride=12, payload=positions), dict(stride=8, payload=uv)],
                    attributes=[[0, 0, 3, 0, 0], [4, 0, 2, 1, 0]],
                    bones=[dict(component=i, index=0, name='root')] if skinned else [],
                    draws=[dict(indices=indices, count=3, material_component=i, material_slot=0,
                                material_name='material', textures=[])])
        if skinned:
            mesh['streams'].append(dict(stride=4, payload=skin))
            mesh['attributes'].append([13, 6, 4, 2, 0])
        m['meshes'].append(mesh)
        delta = b.payload(bem_v13.encode_deltas([[1, (i + 1) / 4, 0, 0]], 3))
        m['mesh_deformations'].append(dict(mesh=i, parameter='length', frames=[
            dict(value=0, neutral=True), dict(value=1000, payload=delta, count=1, encoding=bem_v13.ENCODING)]))
    m['component_rules'] = [dict(target=i, candidates=[dict(operation='replace', mesh=i)]) for i in range(3)]
    m['component_rules'][2]['candidates'] = [dict(operation='replace', mesh=2, when={'eq': ['detail', 'show']}),
                                           dict(operation='hide', when={'eq': ['detail', 'hide']})]
    return b


def create(root):
    """Write a portable pack project and return its export-task path."""
    import bem_tasks
    root = Path(root); root.mkdir(parents=True, exist_ok=True)
    b = fixture(); files = []
    (root / 'payloads').mkdir(exist_ok=True)
    for index, raw in enumerate(b.payloads):
        name = f'payloads/{index:04d}.bin'
        (root / name).write_bytes(raw); files.append(name)
    project = root / 'project.json'
    project.write_text(json.dumps(dict(manifest=b.m, payload_files=files), ensure_ascii=False, indent=2), encoding='utf-8')
    task = root / 'export.bemproj.json'
    bem_tasks.new_project(project, task, mode='pack', export_output=root / 'dist/synthetic.bem')
    return task


def native_fixtures(root):
    """Valid and deliberately invalid packages for cross-language reader tests."""
    root = Path(root); root.mkdir(parents=True, exist_ok=True)
    b = fixture(); valid = root / 'multi-resource-valid.bem'; b.write(valid)
    raw = valid.read_bytes()
    m, _ = bem.read_package(valid, decode=False)
    # Mutate the serialized manifest without using the validating author writer.
    bad = copy.deepcopy(m); bad['meshes'][1]['bones'][0]['component'] = 0
    _rewrite_manifest(raw, bad, root / 'cross-resource-donor-invalid.bem')
    bad = copy.deepcopy(m); bad['target']['components'][0]['renderer_path'] = 'Mesh_all/lod1/shared_mesh'
    _rewrite_manifest(raw, bad, root / 'resource-lod-mismatch-invalid.bem')
    old = bytearray(raw); struct.pack_into('<H', old, 10, 3)
    (root / 'downgraded-header-invalid.bem').write_bytes(old)
    return valid


def _rewrite_manifest(raw, manifest, output):
    header = list(bem.HEADER.unpack_from(raw))
    old_size, count = header[5:7]
    encoded = json.dumps(manifest, ensure_ascii=False, separators=(',', ':')).encode('utf-8')
    shift = len(encoded) - old_size
    entries = []
    for index in range(count):
        entry = list(bem.ENTRY.unpack_from(raw, bem.HEADER.size + old_size + index * bem.ENTRY.size))
        entry[2] += shift; entries.append(bem.ENTRY.pack(*entry))
    header[4] += shift; header[5] = len(encoded)
    data = raw[bem.HEADER.size + old_size + count * bem.ENTRY.size:]
    output.write_bytes(bem.HEADER.pack(*header) + encoded + b''.join(entries) + data)


if __name__ == '__main__':
    p = argparse.ArgumentParser(description=__doc__); p.add_argument('output', type=Path)
    p.add_argument('--native-fixtures', action='store_true')
    args = p.parse_args()
    print(native_fixtures(args.output) if args.native_fixtures else create(args.output))
