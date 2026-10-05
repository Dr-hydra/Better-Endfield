"""Read existing object identities/probe rows only; do not extract or alter assets."""
import collections
import json
import re
import sys
from pathlib import Path

sys.stdout.reconfigure(encoding='utf-8')
ROOT = Path(__file__).resolve().parents[2]
OUT = Path(__file__).resolve().parent
ROLE_ROOT = Path('F:/zmd_bem/research/identities/roles')
catalogs = {}
for path in (ROOT / 'tools/CustomModel/catalog').glob('*.json'):
    data = json.loads(path.read_text(encoding='utf-8-sig'))
    if data.get('kind') == 'bem-character-catalog':
        catalogs[data['character_id']] = data

def scope(root, path):
    relative = path[len(root)+1:]
    lod = re.fullmatch(r'Mesh_all/(lod\d)/[^/]+', relative)
    if lod:
        return lod[1]
    if 'shadowProxy' in relative or relative.startswith('Shadow_Proxy/'):
        return 'shadow-proxy'
    return 'other'

rows = []
sources = []
coverage = collections.defaultdict(collections.Counter)
errors = []
for path in sorted(ROLE_ROOT.glob('*/native.json')):
    data = json.loads(path.read_text(encoding='utf-8-sig'))
    objects = {o['id']: o for o in data['objects']}
    transforms = {o['game_object'].get('id'): o for o in objects.values() if o['type'] == 'Transform'}
    roots = {}
    for obj in objects.values():
        if obj['type'] != 'GameObject':
            continue
        match = re.fullmatch(r'(chr_\d{4}_[a-z0-9]+)_(postmodel|uimodel)', obj['name'])
        if not match:
            continue
        cid, route = match.groups()
        wanted = (f'assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/{obj["name"]}.prefab'
                  if route == 'postmodel' else f'assets/beyond/dynamicassets/gameplay/prefabs/uimodels/{obj["name"]}.prefab')
        if wanted in obj.get('asset_paths', []):
            roots[obj['id']] = (cid, obj['name'])
    def object_path(go):
        transform = transforms.get(go['id'])
        chain = []
        seen = set()
        found = None
        while transform:
            if transform['id'] in seen:
                raise ValueError('cyclic Transform references')
            seen.add(transform['id'])
            owner = objects[transform['game_object']['id']]
            chain.append(owner['name'])
            if owner['id'] in roots:
                found = roots[owner['id']]
                break
            parent = transform['parent'].get('id')
            if parent and parent not in objects:
                raise ValueError('unresolved parent reference')
            transform = objects.get(parent)
        return found, '/'.join(reversed(chain))
    resolved = 0
    source_issues = []
    for renderer in objects.values():
        if renderer['type'] != 'SkinnedMeshRenderer':
            continue
        try:
            go = objects[renderer['game_object']['id']]
            found, fullpath = object_path(go)
            if not found:
                continue
            cid, root = found
            region = scope(root, fullpath)
            coverage[cid]['renderers'] += 1
            coverage[cid][region] += 1
            mesh_id = renderer['mesh'].get('id')
            mesh = objects.get(mesh_id)
            if not mesh or mesh['type'] != 'Mesh':
                coverage[cid]['unresolved_mesh'] += 1
                source_issues.append(dict(path=fullpath, reason='unresolved Mesh object'))
                continue
            resolved += 1
            row = dict(character_id=cid, root=root, path=fullpath, renderer_name=go['name'], mesh_name=mesh['name'],
                mesh_id=mesh_id, renderer_id=renderer['id'], scope=region, source=str(path), evidence='offline-reference-resolved')
            row['mismatch'] = row['renderer_name'] != row['mesh_name']
            component = next(((key, c) for key, c in catalogs.get(cid, {}).get('components', {}).items()
                              if c['mesh_name'] == row['mesh_name']), None)
            row['catalog_component'] = component[0] if component and region == 'lod0' else None
            rows.append(row)
            if row['mismatch']:
                coverage[cid]['mismatches'] += 1
                coverage[cid]['mismatch_'+region] += 1
        except (KeyError, ValueError) as error:
            source_issues.append(dict(renderer_id=renderer['id'], reason=str(error)))
    sources.append(dict(path=str(path), manifest_version=data.get('snapshot', {}).get('manifest_version'),
        bundle_platforms=sorted({match[1] for bundle in data.get('bundles', [])
                                 if (match := re.search(r'Bundles[/\\](Windows|Android)[/\\]', bundle))}),
        roots=[dict(character_id=c, root=r) for c, r in roots.values()], resolved_renderers=resolved,
        backend_issues=len(data.get('backend_errors', [])), issues=source_issues))
    errors.extend(source_issues)

runtime = []
runtime_source = ROOT / 'artifacts/android-refactor/android-source-metadata.jsonl'
for line in runtime_source.read_text(encoding='utf-8-sig').splitlines():
    record = json.loads(line)
    for row in record.get('renderers', []):
        root = row.get('resource_root', record.get('resource_root'))
        match = re.fullmatch(r'(chr_\d{4}_[a-z0-9]+)_(postmodel|uimodel)', root or '')
        if not match or not row.get('complete') or not row.get('path') or not row.get('mesh_name'):
            continue
        # Probe records BuildTransformPath(renderer) and ObjectName(sharedMesh).
        # Its path leaf is the observed renderer GameObject name.
        name = row['path'].rsplit('/', 1)[-1]
        runtime.append(dict(character_id=match[1], root=root, path=row['path'], renderer_name=name,
            mesh_name=row['mesh_name'], mismatch=name != row['mesh_name'], scope=scope(root, row['path']),
            run=record.get('run'), source=str(runtime_source), evidence='runtime-observed-path-and-mesh-name'))

raw_row_count = len(rows)
unique = {}
for row in rows:
    key = tuple(row[k] for k in ('character_id', 'root', 'path', 'renderer_id', 'mesh_id', 'renderer_name', 'mesh_name'))
    if key not in unique:
        unique[key] = dict(row, source_files=[])
    unique[key]['source_files'].append(row['source'])
rows = list(unique.values())
coverage = collections.defaultdict(collections.Counter)
identity_versions = collections.defaultdict(set)
for row in rows:
    cid = row['character_id']
    coverage[cid]['renderers'] += 1
    coverage[cid][row['scope']] += 1
    identity_versions[(cid, row['root'], row['path'])].add((row['renderer_id'], row['mesh_id'], row['mesh_name']))
    if row['mismatch']:
        coverage[cid]['mismatches'] += 1
        coverage[cid]['mismatch_'+row['scope']] += 1
conflicts = [dict(character_id=k[0], root=k[1], path=k[2], identities=sorted(v))
             for k,v in identity_versions.items() if len(v)>1]
groups = collections.defaultdict(list)
for row in rows:
    if row['mismatch']:
        groups[(row['character_id'], row['scope'], row['renderer_name'], row['mesh_name'])].append(row)
mismatch_groups = [dict(character_id=key[0], scope=key[1], renderer_name=key[2], mesh_name=key[3],
    roots=sorted({r['root'] for r in value}), paths=sorted({r['path'] for r in value}),
    mesh_ids=sorted({r['mesh_id'] for r in value}), catalog_component=value[0]['catalog_component'])
    for key, value in sorted(groups.items())]
missing_catalog_roles = sorted(set(catalogs)-set(coverage))
summary = dict(offline_sources=len(sources), offline_characters=len(coverage), catalog_characters=len(catalogs),
    root_observations=sum(len(s['roots']) for s in sources), roots=len({r['root'] for r in rows}),
    raw_resolved_rows=raw_row_count, dependency_duplicate_rows=raw_row_count-len(rows), resolved_renderer_rows=len(rows),
    identity_conflicts=len(conflicts),
    unresolved_issues=len(errors), lod0_rows=sum(r['scope']=='lod0' for r in rows),
    lod0_mismatch_rows=sum(r['scope']=='lod0' and r['mismatch'] for r in rows),
    lod0_mismatch_characters=sorted({r['character_id'] for r in rows if r['scope']=='lod0' and r['mismatch']}),
    runtime_rows=len(runtime), runtime_characters=sorted({r['character_id'] for r in runtime}),
    missing_catalog_roles=missing_catalog_roles)
result = dict(summary=summary, coverage={c:dict(v) for c,v in sorted(coverage.items())},
    sources=sources, conflicts=conflicts, mismatches=mismatch_groups, runtime=runtime, renderer_rows=rows)
(OUT/'existing-renderer-mesh-name-audit.json').write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
print('summary', json.dumps(summary, ensure_ascii=False))
print('all non-shadow mismatch groups', json.dumps([r for r in mismatch_groups if r['scope']!='shadow-proxy'], ensure_ascii=False))
print('wolfgd coverage', dict(coverage['chr_0006_wolfgd']))
print('all mismatch scope counts', dict(collections.Counter(r['scope'] for r in rows if r['mismatch'])))
print('runtime mismatch scope counts', dict(collections.Counter(r['scope'] for r in runtime if r['mismatch'])))
print('source bundle platforms', dict(collections.Counter(tuple(s['bundle_platforms']) for s in sources)))
