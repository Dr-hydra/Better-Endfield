"""BEM 1.4 explicit resource targets and static mesh contracts.

Resource membership is an identity boundary. Payload bytes may be shared across
resources, but each descriptor's donor references belong to one resource.
"""
from __future__ import annotations

from pathlib import PurePosixPath
import re

import bem_v1 as bem

CAPABILITIES = ('multi-resource-targets', 'static-meshes')
PLATFORMS = ('windows-x64', 'android-arm64')
KINDS = ('skinned', 'static')
MAX_RESOURCES = 32


def used(m):
    target = m.get('target', {})
    return bool(set(m.get('required_capabilities', [])) & set(CAPABILITIES) or
                any(k in target for k in ('kind', 'id', 'resources')) or
                any(any(k in c for k in ('resource', 'renderer_kind', 'renderer_path'))
                    for c in target.get('components', [])) or
                any('renderer_kind' in mesh for mesh in m.get('meshes', [])))


def _path(value, label, *, empty=False):
    bem.require(isinstance(value, str) and len(value.encode('utf-8')) <= 1024 and
                (bool(value) or empty) and '\0' not in value and '\\' not in value and ':' not in value and
                (not value or all(part not in ('', '.', '..') for part in value.split('/'))),
                'Invalid ' + label)
    return value


def validate_target(m):
    target = m['target']
    bem.require(target.get('kind') in ('character', 'weapon'), 'Invalid target kind')
    bem.require(not any(k in target for k in ('character_id', 'world_resource', 'ui_resource')),
                'BEM 1.4 target must use explicit resources')
    for key in ('id', 'profile_id', 'revision'):
        bem.identity(target[key])
    bem.require(target['platform'] == 'windows-x64', 'Invalid target platform')
    bem.require(isinstance(target.get('snapshot'), str) and '\0' not in target['snapshot'] and
                0 < len(target['snapshot'].encode('utf-8')) <= 256,
                'Invalid target snapshot')
    caps = m['required_capabilities']
    bem.require('multi-resource-targets' in caps, 'Multi-resource target capability mismatch')
    resources = target['resources']
    bem.require(isinstance(resources, list) and 0 < len(resources) <= MAX_RESOURCES, 'Invalid target resources')
    ids, names, assets, resource_lods = set(), set(), set(), {}
    for resource in resources:
        bem.require(isinstance(resource, dict) and set(resource) ==
                    {'id', 'name', 'asset_path', 'platforms', 'lod'}, 'Invalid resource fields')
        rid = bem.identity(resource['id'])
        bem.require(rid not in ids, 'Duplicate resource ID'); ids.add(rid)
        name = bem.identity(resource['name'])
        bem.require(name == name.lower(), 'Resource root must be normalized lowercase')
        path = _path(resource['asset_path'], 'resource asset path')
        bem.require(path == path.lower() and path.startswith('assets/') and path.endswith('.prefab') and
                    PurePosixPath(path).stem == name, 'Resource path/root mismatch')
        platforms = resource['platforms']
        bem.require(isinstance(platforms, list) and 0 < len(platforms) <= len(PLATFORMS) and
                    all(p in PLATFORMS for p in platforms) and len(platforms) == len(set(platforms)),
                    'Invalid resource platforms')
        for platform in platforms:
            bem.require((platform, name) not in names and (platform, path) not in assets,
                        'Duplicate resource identity on platform')
            names.add((platform, name)); assets.add((platform, path))
        bem.require(type(resource['lod']) is int and 0 <= resource['lod'] <= 3, 'Invalid resource LOD')
        resource_lods[rid] = resource['lod']
    components = target['components']
    bem.require(isinstance(components, list) and 0 < len(components) <= 64, 'Invalid target components')
    meshes, paths, occupied = set(), set(), set()
    static = False
    for index, component in enumerate(components):
        bem.require(type(component['id']) is int and component['id'] == index and type(component['original_index_count']) is int and
                    component['original_index_count'] > 0 and component['original_index_count'] % 3 == 0,
                    'Invalid target identity')
        rid, kind = component['resource'], component['renderer_kind']
        bem.require(rid in ids and kind in KINDS, 'Invalid component resource/renderer kind')
        occupied.add(rid)
        path = _path(component['renderer_path'], 'renderer path', empty=True)
        known_lod = re.match(r'^Mesh_all/lod([0-3])(?:/|$)', path)
        bem.require(known_lod is None or int(known_lod[1]) == resource_lods[rid],
                    'Renderer path LOD differs from resource LOD')
        mesh_name = component['mesh_name']
        bem.require(isinstance(mesh_name, str) and '\0' not in mesh_name and 0 < len(mesh_name.encode('utf-8')) <= 256,
                    'Invalid mesh name')
        bem.require((rid, mesh_name) not in meshes, 'Duplicate mesh identity within resource')
        bem.require((rid, path) not in paths, 'Duplicate renderer path within resource')
        meshes.add((rid, mesh_name)); paths.add((rid, path))
        bones, materials = component['bone_names'], component['materials']
        bem.require(isinstance(bones, list) and len(bones) <= 65536 and
                    isinstance(materials, list) and len(materials) <= 256 and
                    all(isinstance(v, str) and '\0' not in v and 0 < len(v.encode('utf-8')) <= 256 for v in bones + materials),
                    'Invalid target donor table')
        bem.require('bone_name_aliases' not in component, 'BEM 1.4 uses resource-local bone identities')
        if kind == 'static':
            static = True
            bem.require(not bones, 'Static component must not have bones')
    bem.require(occupied == ids, 'Every resource must have target components')
    static |= any(mesh.get('renderer_kind') == 'static' for mesh in m['meshes'])
    bem.require(not static or 'static-meshes' in caps, 'Static mesh capability mismatch')


def validate_bindings(m):
    components = m['target']['components']
    owners = []
    for mesh in m['meshes']:
        kind = mesh['renderer_kind']
        bem.require(kind in KINDS, 'Invalid mesh renderer kind')
        donors = set()
        for bone in mesh['bones']:
            index = bone['component']
            bem.require(type(index) is int and 0 <= index < len(components), 'Invalid bone donor component')
            donor = components[index]
            bem.require(donor['renderer_kind'] == 'skinned', 'Bone donor must be skinned')
            donors.add(donor['resource'])
        for draw in mesh['draws']:
            index = draw['material_component']
            bem.require(type(index) is int and 0 <= index < len(components), 'Invalid material donor component')
            donors.add(components[index]['resource'])
        bem.require(len(donors) == 1, 'Mesh donors must belong to one resource')
        owners.append(next(iter(donors)))
    for rule in m['component_rules']:
        component = components[rule['target']]
        for candidate in rule['candidates']:
            if candidate['operation'] != 'replace': continue
            mid = candidate['mesh']
            bem.require(component['renderer_kind'] == m['meshes'][mid]['renderer_kind'],
                        'Replacement renderer kind differs from target')
            bem.require(component['resource'] == owners[mid], 'Replacement donors belong to another resource')


def validate_manifest(m, payload_count, minor=None):
    bem.require(minor in (None, 4), 'Explicit resource targets require BEM 1.4')
    bem.require('option_groups' in m, 'BEM 1.4 requires a composable project')
    import bem_v13
    bem_v13.validate_manifest(m, payload_count, 4)
    validate_bindings(m)


def selection_plan(m, options=None, *, resource=None, platform=None, parameters=None):
    """Return a selected plan, optionally restricted to an explicit resource/platform.

    Resource filtering rebuilds the payload closure so sibling prefabs are not
    unnecessarily decoded. Global component and mesh indices stay unchanged.
    """
    import bem_v11
    import bem_v13
    bem.require(platform is None or platform in PLATFORMS, 'Unknown target platform')
    resources = {r['id']: r for r in m['target']['resources']}
    bem.require(resource is None or resource in resources, 'Unknown target resource')
    selected = {rid for rid, r in resources.items() if (resource is None or rid == resource) and
                (platform is None or platform in r['platforms'])}
    bem.require(bool(selected), 'Resource is unavailable on requested platform')
    plan = bem_v11.selection_plan(m, options, 4, parameters)
    plan['resources'] = [r['id'] for r in m['target']['resources'] if r['id'] in selected]
    plan['components'] = [c for c in plan['components'] if m['target']['components'][c['target']]['resource'] in selected]
    textures, payloads = set(), set()
    for component in plan['components']:
        if component['operation'] == 'replace':
            mesh = m['meshes'][component['mesh']]
            payloads.update(s['payload'] for s in mesh['streams'])
            for draw in component['draws']:
                payloads.add(draw['indices']); textures.update(draw['textures'])
        for material in component.get('material_overrides', []):
            textures.update(material['textures'])
    payloads.update(m['textures'][t]['payload'] for t in textures)
    if m.get('parameters'):
        _, channels = bem_v13.selected_deformations(m, plan, parameters)
        for _, frames in channels:
            payloads.update(f['payload'] for f, _ in frames if 'payload' in f)
    plan.update(textures=sorted(textures), payloads=sorted(payloads))
    return plan
