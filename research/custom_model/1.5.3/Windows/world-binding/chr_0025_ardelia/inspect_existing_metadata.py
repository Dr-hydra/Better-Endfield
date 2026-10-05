"""Only saved object metadata and BEM JSON headers; never decode payloads."""
import json
import struct
import sys
from pathlib import Path

sys.stdout.reconfigure(encoding='utf-8')
OUT = Path(__file__).resolve().parent
results = {}
for platform in ('pc', 'android'):
    data = json.loads((OUT / f'{platform}-graph.json').read_text(encoding='utf-8-sig'))
    gos = {o['id']: o for o in data['objects'] if o['type'] == 'GameObject'}
    refs = [(gos[o['game_object']['id']]['name'], o['mesh']) for o in data['objects']
            if o['type'] == 'SkinnedMeshRenderer' and 'fur_01_lod' in gos[o['game_object']['id']]['name']]
    results[platform] = refs
    print(platform, 'fur mesh refs', refs)

for label, path in [('catalog-source', Path('F:/zmd_bem/research/identities/roles/chr_0025_ardelia/native.json')),
                    ('saved-role-probe', Path('F:/zmd_bem/research/codex_20261001/full_audit/group5/role-probes/chr_0025_ardelia/native.json'))]:
    data = json.loads(path.read_text(encoding='utf-8-sig'))
    objects = {o['id']: o for o in data['objects']}
    matching = [{k: objects[ref['id']].get(k) for k in ('id', 'type', 'name', 'submeshes')}
                for _, ref in results['pc'] if ref.get('id') in objects]
    results[label] = matching
    print(label, 'saved fur Mesh names', [(o['name'], o['id']) for o in matching])

packages = []
catalog = json.loads(Path('tools/CustomModel/catalog/chr_0025_ardelia.json').read_text(encoding='utf-8-sig'))
for path in Path('F:/zmd_bem/成品/艾尔黛拉').glob('*.bem'):
    with path.open('rb') as stream:
        magic, major, minor, header, size, metadata, count, flags = struct.unpack('<8sHHIQQII', stream.read(40))
        assert major == 1 and header == 40 and size == path.stat().st_size and 0 < metadata <= 4 * 1024**2
        data = json.loads(stream.read(metadata).decode('utf-8'))
    target = data['target']
    fur = [c for c in target['components'] if 'fur' in c['mesh_name']]
    row = dict(path=str(path), name=data.get('name'), target_roots={k: target.get(k) for k in ('character_id', 'world_resource', 'ui_resource')},
               fur_components=fur, metadata=data)
    packages.append(row)
    print('BEM', path.name, row['target_roots'], 'fur names', [(c['id'], c['mesh_name']) for c in fur])
    print('metadata keys', list(data), 'texture sample', str(data.get('textures'))[:400])
results['packages'] = packages
checks = []
for package in packages:
    data = package['metadata']
    components = {c['id']: c for c in data['target']['components']}
    textures = data.get('textures', [])
    issues = []
    bindings = 0
    def material(binding, cid):
        global bindings
        bindings += 1
        component = components[cid]
        slot = binding['material_slot']
        if slot >= len(component['materials']) or component['materials'][slot] != binding['material_name']:
            issues.append(dict(kind='material-identity', component=cid, slot=slot))
            return
        pins = catalog['components'][str(cid)]['material_textures'][slot]
        for tid in binding.get('textures', []):
            if tid >= len(textures) or textures[tid]['original_name'] not in pins:
                issues.append(dict(kind='texture-pin', component=cid, slot=slot, texture=tid,
                    name=textures[tid]['original_name'] if tid < len(textures) else None))
    for mesh in data['meshes']:
        for binding in mesh.get('draws', []):
            material(binding, binding['material_component'])
    def visit(node):
        if isinstance(node, dict):
            if 'target' in node:
                def overrides(child):
                    if isinstance(child, dict):
                        for binding in child.get('material_overrides', []):
                            material(binding, node['target'])
                        for key, value in child.items():
                            if key != 'material_overrides': overrides(value)
                    elif isinstance(child, list):
                        for value in child: overrides(value)
                overrides(node)
            else:
                for value in node.values(): visit(value)
        elif isinstance(node, list):
            for value in node: visit(value)
    visit(data.get('component_rules', []))
    visit(data.get('appearances', []))
    for component in components.values():
        source = catalog['components'][str(component['id'])]
        for key in ('mesh_name', 'materials', 'bone_names', 'original_index_count'):
            if component[key] != source[key]:
                issues.append(dict(kind='target-contract', component=component['id'], field=key))
    row = dict(name=package['name'], material_bindings_checked=bindings, issues=issues)
    checks.append(row)
    print('saved catalog pin audit', row)
results['saved_catalog_contract_checks'] = checks
(OUT / 'existing-package-metadata.json').write_text(json.dumps(results, ensure_ascii=False, indent=2), encoding='utf-8')
