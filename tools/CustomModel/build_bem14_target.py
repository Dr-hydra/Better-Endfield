"""Create an explicitly unverified BEM 1.4 author target from native metadata.

The resource specification chooses exact asset paths and LOD branches. This
does not infer runtime layouts, donor equivalence or source-Mod mappings.
"""
from __future__ import annotations

import argparse
import json
from pathlib import Path, PurePosixPath

import bem_v1 as bem
import bem_v14
from parse_native_models import parse


def build_profile(database, spec):
    if database.get('kind') != 'native_character_metadata':
        database = parse(database)
    bem.require(spec.get('schema') == 1 and spec.get('kind') == 'bem-resource-target-spec', 'Invalid target specification')
    source = database.get('source', {})
    snapshot = source.get('snapshot') or {}
    observed_version = snapshot.get('manifest_version')
    bem.require(not observed_version or spec['snapshot'] == observed_version, 'Target snapshot differs from native metadata')
    platform_names = {'windows': 'windows-x64', 'android': 'android-arm64'}
    observed_platforms = {platform_names[parts[1]] for record in snapshot.get('bundles', [])
                          if len(parts := record['path'].lower().split('/')) > 1 and
                          parts[0] == 'bundles' and parts[1] in platform_names}
    target = {k: spec[k] for k in ('id', 'profile_id', 'revision', 'snapshot')}
    target.update(kind=spec['target_kind'], platform='windows-x64', resources=[], components=[])
    evidence = []
    for request in spec['resources']:
        bem.require(not observed_platforms or set(request['platforms']) <= observed_platforms,
                    'Target platforms lack source snapshot evidence')
        asset = request['asset_path']
        root = PurePosixPath(asset).stem
        resource = dict(id=request['id'], name=root, asset_path=asset, platforms=request['platforms'], lod=request['lod'])
        target['resources'].append(resource)
        rows = [row for row in database['renderers'] if asset in row.get('resource_asset_paths', [])]
        bem.require(rows, 'No renderer with exact prefab asset identity: ' + asset)
        prefix = root + '/'
        selected = []
        explicit = request.get('renderer_paths')
        if explicit is not None:
            bem.require(isinstance(explicit, list) and explicit and len(explicit) == len(set(explicit)),
                        'Invalid explicit renderer paths')
        for row in rows:
            bem.require(row['resource_root'] == root and (row['path'] == root or row['path'].startswith(prefix)),
                        'Prefab asset/root identity mismatch')
            path = row['path'][len(prefix):] if row['path'] != root else ''
            # Only an exact named hierarchy branch is selected automatically;
            # unusual prefabs require the author's explicit receiver list.
            include = path in explicit if explicit is not None else f"lod{request['lod']}" in path.split('/')
            if include: selected.append((path, row))
        bem.require(selected, 'No exact LOD branch; provide renderer_paths for ' + asset)
        if explicit is not None:
            bem.require({path for path, _ in selected} == set(explicit), 'Explicit renderer path missing from prefab')
        for path, row in sorted(selected, key=lambda item: item[0]):
            bem.require(row['offline_references_complete'], 'Incomplete offline references: ' + row['path'])
            component = dict(id=len(target['components']), resource=resource['id'], renderer_kind=row['renderer_kind'],
                             renderer_path=path, mesh_name=row['mesh_name'], original_index_count=row['original_index_count'],
                             bone_names=[bone['name'] for bone in row['bones']],
                             materials=[material['name'] for material in row['materials']])
            target['components'].append(component)
            evidence.append(dict(component=component['id'], renderer_id=row['id'], mesh_id=row['mesh_id'],
                                 resource=resource['id'], renderer_path=path,
                                 runtime_layout_observed=row.get('runtime_layout') is not None))
    capabilities = ['native-materials', 'composable-options', 'multi-resource-targets']
    if any(c['renderer_kind'] == 'static' for c in target['components']): capabilities.append('static-meshes')
    # A keep-only manifest exercises the complete target contract without
    # inventing replacement buffers or claiming a playable conversion.
    manifest = dict(schema=1, package_id=target['profile_id'], name='Unverified target scaffold', author='BEM target tool',
                    version=target['revision'], required_capabilities=capabilities, target=target, meshes=[], textures=[],
                    option_groups=[], component_rules=[dict(target=c['id'], candidates=[dict(operation='keep')])
                                                       for c in target['components']])
    bem.validate_manifest(manifest, 0)
    return dict(schema=1, kind='bem-target-profile', bem_format='1.4', target=target,
                required_capabilities=capabilities, evidence=evidence,
                source_snapshot={k: (source.get('snapshot') or {}).get(k) for k in ('manifest_version', 'perforce_cl')},
                backend_warning_count=len(source.get('backend_errors') or []),
                runtime_verified=False, conversion_ready=False, render_verified=False,
                limitations=['Offline identities only; replacement stream layouts, source mappings and runtime lifecycle remain unverified.']), manifest


def create_profile(graph, spec, output, project=None):
    graph, spec, output = Path(graph), Path(spec), Path(output)
    project = Path(project) if project else None
    inputs = {graph.resolve(), spec.resolve()}
    outputs = [output] + ([project] if project else [])
    bem.require(len({p.resolve() for p in outputs}) == len(outputs) and
                all(p.resolve() not in inputs for p in outputs), 'Output must not overwrite inputs or another output')
    profile, manifest = build_profile(bem.load_json(graph), bem.load_json(spec))
    bem.atomic_write(output, json.dumps(profile, ensure_ascii=False, indent=2).encode('utf-8'))
    if project:
        bem.atomic_write(project, json.dumps(dict(manifest=manifest, payload_files=[]), ensure_ascii=False, indent=2).encode('utf-8'))
    return dict(profile=str(output), project=str(project) if project else None, format_version='1.4',
                resources=len(profile['target']['resources']), components=len(profile['target']['components']),
                runtime_verified=False, conversion_ready=False)


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('graph', type=Path, help='Raw NativeAssetReader graph or parsed native metadata')
    parser.add_argument('--spec', required=True, type=Path)
    parser.add_argument('--output', required=True, type=Path, help='Draft target profile JSON')
    parser.add_argument('--project', type=Path, help='Optional editable project with keep-only component rules')
    args = parser.parse_args(argv)
    print(json.dumps(create_profile(args.graph, args.spec, args.output, args.project)))
    return 0


if __name__ == '__main__': raise SystemExit(main())
