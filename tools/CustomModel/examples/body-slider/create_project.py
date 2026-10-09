"""Create a tiny author workflow fixture (Python standard library only).

This triangle is a format/creator test, not a game character or playable mod.
Run: python create_project.py --output <new-directory>
Then: BetterEndfieldNext.BemConverter.exe build <new-directory>/export.bemproj.json
"""
import argparse
import json
import struct
from pathlib import Path


def create(root):
    root = Path(root)
    if root.exists() and any(root.iterdir()): raise ValueError('Choose a new empty output directory: ' + str(root))
    root.mkdir(parents=True, exist_ok=True)
    payload = root / 'payloads'; payload.mkdir(exist_ok=True)
    streams = [b''.join(struct.pack('<4f', *p, 1.0) for p in [(0, 0, 0), (1, 0, 0), (0, 1, 0)]),
               b''.join(struct.pack('<2f4B', 0, 0, 127, 127, 127, 127) for _ in range(3)),
               b''.join(struct.pack('<4H4B', 65535, 0, 0, 0, 0, 0, 0, 0) for _ in range(3)),
               struct.pack('<3H', 0, 1, 2)]
    for i, raw in enumerate(streams): (payload / f'{i}.bin').write_bytes(raw)
    m = dict(schema=1, package_id='example.body-slider', name='BEM 1.3 position test', author='BEM Tools', version='1',
             required_capabilities=['native-materials', 'palette-u8', 'indices-u32', 'fixed-appearances'],
             target=dict(character_id='chr_test', platform='windows-x64', profile_id='example', revision='1', snapshot='synthetic',
                         world_resource='world', ui_resource='ui', components=[dict(id=0, mesh_name='body_lod0',
                         original_index_count=3, bone_names=['Root'], materials=['body'])]),
             meshes=[dict(vertex_count=3, index_count=3, index_size=2,
                          streams=[dict(stride=s, payload=i) for i, s in enumerate([16, 12, 12])], indices=3,
                          attributes=[[0, 0, 3, 0, 0], [1, 0, 1, 0, 12], [4, 0, 2, 1, 0], [6, 3, 4, 1, 8], [12, 4, 4, 2, 0], [13, 6, 4, 2, 8]],
                          bones=[dict(component=0, index=0, name='Root')], draws=[dict(start=0, count=3, material_component=0,
                          material_slot=0, material_name='body', textures=[])])], textures=[], default_appearance_id='default',
             appearances=[dict(id='default', name='Triangle', components=[dict(target=0, operation='replace', mesh=0)])])
    files = { 'project.json': dict(manifest=m, payload_files=[f'payloads/{i}.bin' for i in range(4)]),
              'target-positions.json': [[0, 0, 0], [1.5, 0, 0], [0, 1.5, 0]],
              'body-morphs.json': dict(schema=1, kind='bem-position-morphs',
                 parameters=[dict(id='body', name='Body size', min=0, max=1000, neutral=0, default=0, step=1)],
                 mesh_deformations=[dict(mesh=0, parameter='body', frames=[dict(value=0, neutral=True),
                                                            dict(value=1000, target_positions='target-positions.json')])]),
              'export.bemproj.json': dict(schema=1, kind='bem-export-task', mode='pack', source='project.json',
                 deformations='body-morphs.json', output='dist/synthetic.bem', report='reports/build.json',
                 package=dict(id=m['package_id'], name=m['name'], author=m['author'], version=m['version'])) }
    for name, data in files.items():
        path = root / name
        if path.exists(): raise ValueError('Choose a new output directory: ' + str(path))
        path.write_text(json.dumps(data, indent=2), encoding='utf-8')
    return root / 'export.bemproj.json'


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__); parser.add_argument('--output', type=Path, required=True)
    print(create(parser.parse_args().output))
