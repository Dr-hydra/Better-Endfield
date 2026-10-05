import json
import sys
from pathlib import Path
sys.stdout.reconfigure(encoding='utf-8')
REPO = Path(__file__).resolve().parents[2]
OUT = Path(sys.argv[1]).resolve() if len(sys.argv) > 1 else Path(__file__).resolve().parent
report = {}
for platform in ('pc', 'android'):
    data = json.loads((OUT / f'{platform}-graph.json').read_text(encoding='utf-8-sig'))
    objects = {o['id']: o for o in data['objects']}
    transforms = {o['game_object']['id']: o for o in objects.values() if o['type'] == 'Transform'}
    def path(go):
        names = []
        t = transforms[go]
        while t:
            names.append(objects[t['game_object']['id']]['name'])
            t = objects.get(t['parent'].get('id'))
        return '/'.join(reversed(names))
    renderers = []
    for o in objects.values():
        if o['type'] != 'SkinnedMeshRenderer':
            continue
        go = objects[o['game_object']['id']]
        renderers.append(dict(name=go['name'], path=path(go['id']), mesh=o['mesh'],
            bones=[dict(name=objects[b['id']]['name'], path=path(objects[b['id']]['game_object']['id']))
                   for b in o['bones'] if b.get('id') in objects],
            materials=[objects.get(m.get('id'), {}).get('name') for m in o['materials']]))
    roots = {o['name']: dict(asset_paths=o['asset_paths'], renderers=[r for r in renderers if r['path'].startswith(o['name']+'/')])
             for o in objects.values() if o['type'] == 'GameObject' and o['asset_paths'] and o['name'].endswith(('_postmodel', '_uimodel'))}
    report[platform] = roots
    for root, details in roots.items():
        rows = details['renderers']
        print(platform, root, 'renderers', len(rows))
        lod = 'lod0' if root.endswith('_uimodel') or platform == 'pc' else 'lod1'
        names = [r['name'] for r in rows if f'/Mesh_all/{lod}/' in r['path']]
        print('components', names)
        if root.endswith('_postmodel'):
            catalog=json.loads((REPO/'tools/CustomModel/catalog'/f'{root.removesuffix("_postmodel")}.json').read_text(encoding='utf-8-sig'))
            def renderer_name(mesh):
                if root == 'chr_0025_ardelia_postmodel' and mesh == 'S_actor_ardelia_fur_01_lod0_20':
                    return 'S_actor_ardelia_fur_01_lod0'
                return mesh
            expected=[renderer_name(c['mesh_name'])[:-1]+'1' for c in catalog['components'].values()]
            print('missing world LOD1', [name for name in expected if name not in [r['name'] for r in rows if '/Mesh_all/lod1/' in r['path']]])
            print('LOD1 materials', [(r['name'],r['materials']) for r in rows if '/Mesh_all/lod1/' in r['path']])
            ui=next((v for k,v in roots.items() if k==root.removesuffix('_postmodel')+'_uimodel'),None)
            if ui:
                world_paths={b['path'].split('/',1)[1] for r in rows for b in r['bones']}
                missing=sorted({b['path'].split('/',1)[1] for r in ui['renderers'] for b in r['bones'] if b['path'].split('/',1)[1] not in world_paths})
                print('UI bone paths missing from world palettes',missing[:15], 'total',len(missing))
(OUT/'prefab-comparison.json').write_text(json.dumps(report,ensure_ascii=False,indent=2),encoding='utf-8')
