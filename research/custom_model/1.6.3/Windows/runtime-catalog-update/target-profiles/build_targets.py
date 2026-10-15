"""Build explicit Windows targets from this capture's observed native metadata.

Uses the existing BEM 1.4 target builder and its manifest validation. The
generated projects contain keep operations only; no BEM package is built.
"""
import json
import sys
from pathlib import Path

HERE = Path(__file__).resolve().parent
REPO = HERE.parents[5]
sys.path.insert(0, str(REPO / 'tools' / 'CustomModel'))

from build_bem14_target import create_profile


def main():
    capture = HERE.parent
    requests = [
        ('chr_0037_minghe', 'chr_0037_minghe', None),
        ('chr_0037_minghe02', 'chr_0037_minghe', None),
        ('chr_0037_minghe03', 'chr_0037_minghe', None),
        ('chr_0036_jsspsi', 'chr_0036_jsspsi',
         ['Mesh_all/lod0/S_actor_jsspsi_cloth_04_lod0']),
    ]
    for character, owner, paths in requests:
        database = capture / character / 'database.observed.json'
        native = json.loads(database.read_text(encoding='utf-8'))
        root = character + '_postmodel'
        prefix = character + ('-cloth04-world' if paths else '-world')
        selected = [r for r in native['renderers'] if r['resource_root'] == root
                    and (r['path'].split('/', 1)[1] in paths if paths
                         else '/lod0/' in r['path'])]
        if not selected or not all(r.get('runtime_layout') for r in selected):
            raise ValueError('Selected target lacks observed layouts: ' + character)
        resource = dict(id='world',
                        asset_path='assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/' + root + '.prefab',
                        platforms=['windows-x64'], lod=0)
        if paths:
            resource['renderer_paths'] = paths
        spec = dict(schema=1, kind='bem-resource-target-spec', id=owner,
                    profile_id=prefix + '-pc', revision='1.6.3-20261015',
                    snapshot=native['source']['snapshot']['manifest_version'],
                    target_kind='character', resources=[resource])
        spec_path = HERE / (prefix + '.spec.json')
        spec_path.write_text(json.dumps(spec, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
        result = create_profile(database, spec_path,
                                HERE / (prefix + '.profile.json'),
                                HERE / (prefix + '.project.json'))
        print(json.dumps(dict(target=prefix, components=result['components'],
                              observed_layouts=len(selected),
                              runtime_verified=False, conversion_ready=False,
                              render_verified=False), ensure_ascii=False))


if __name__ == '__main__':
    main()
