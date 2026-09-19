"""Join a hash/LOD Mod to offline native evidence; emit an unapproved profile draft.

Unique index-count/LOD matches are candidates, never runtime declaration proof.
Lower-LOD bone remaps require identical Transform identities and bindposes.
"""
import argparse
import json
import math
import re
from pathlib import Path

from convert_efmi_poc import Source
from convert_hash_lod import select_ini
from efmi_source import sections, value
from efmi_lod_source import ENTRY
from hash_lod_lowering import selected_draws, skeleton_ranges, texture_overrides


def equal_matrix(a, b):
    return len(a) == len(b) == 16 and all(
        math.isfinite(x) and math.isfinite(y) and abs(x-y) <= 1e-5 for x, y in zip(a, b))


def remap_bones(source, target, meshes):
    """Same hierarchy object, not same name or equal palette length."""
    by_id = {}
    for index, bone in enumerate(target['bones']):
        if bone['id'] in by_id:
            raise ValueError('ambiguous target bone identity')
        by_id[bone['id']] = index
    indices = []
    for index, bone in enumerate(source['bones']):
        if bone['id'] not in by_id:
            raise ValueError(f"source bone absent from LOD0: {bone['name']}")
        mapped = by_id[bone['id']]
        if not equal_matrix(meshes[source['mesh_id']]['bindposes'][index],
                            meshes[target['mesh_id']]['bindposes'][mapped]):
            raise ValueError(f"bindpose differs: {bone['name']}")
        indices.append(mapped)
    return indices


def paired_resource(source, target, meshes):
    relative = lambda path: path.split('/', 1)[-1]
    errors = []
    if source['mesh_id'] != target['mesh_id']:
        errors.append('world/UI Mesh identity differs')
    if [relative(b['path']) for b in source['bones']] != [relative(b['path']) for b in target['bones']]:
        errors.append('world/UI bone path/order differs')
    if [m['id'] for m in source['materials']] != [m['id'] for m in target['materials']]:
        errors.append('world/UI material slots differ')
    if not source['offline_references_complete'] or not target['offline_references_complete']:
        errors.append('native references incomplete')
    return errors


def prepare(database, sec, world, ui):
    if world not in database['resources'] or ui not in database['resources']:
        raise ValueError('world/UI resource roots must both exist in the database')
    meshes = database['meshes']
    entries, blocked = [], []
    for name, body in sec.items():
        match = ENTRY.fullmatch(name)
        if not match: continue
        lod, identity, count, first = match.groups()
        if value(body, 'hash') != identity or value(body, 'match_index_count') != count or value(body, 'match_first_index') != first:
            raise ValueError('source entry identity mismatch')
        group = re.search(r'\\component_id\s*=\s*(\d+)', body)
        if not group: raise ValueError('entry has no explicit merged skeleton group')
        candidates = [r for r in database['renderers'] if r['resource_root'] == world
                      and f'/lod{lod}/' in r['path'] and r['original_index_count'] == int(count)]
        if len(candidates) != 1:
            raise ValueError(f'{name}: expected unique native count/LOD candidate, found {len(candidates)}')
        entries.append({'section': name, 'hash': identity, 'lod': int(lod), 'group': int(group[1]),
                        'native': candidates[0]})
    lod0 = [e for e in entries if e['lod'] == 0]
    if not lod0: raise ValueError('no LOD0 entries')
    components, mappings, groups, evidence_rows = {}, {}, {}, []
    for cid, entry in enumerate(lod0):
        native = entry['native']; mesh = meshes[native['mesh_id']]
        companions = [r for r in database['renderers'] if r['resource_root'] == ui
                      and r['mesh_name'] == native['mesh_name'] and '/lod0/' in r['path']]
        errors = paired_resource(native, companions[0], meshes) if len(companions) == 1 else ['missing/ambiguous UI companion']
        blocked.extend(f"{entry['hash']}: {error}" for error in errors)
        runtime = native.get('runtime_layout')
        components[str(cid)] = {'mesh_name': native['mesh_name'],
            'original_index_count': native['original_index_count'],
            'bone_names': [b['name'] for b in native['bones']],
            'materials': [m['name'] for m in native['materials']],
            'strides': runtime['strides'] if runtime else [],
            'attributes': runtime['attributes'] if runtime else [],
            'evidence': f"offline renderer {native['id']}; runtime layout pending"}
        if not runtime: blocked.append(f"{entry['hash']}: full runtime declaration not observed")
        if len(native['materials']) > len(mesh['submeshes']):
            blocked.append(f"{entry['hash']}: {len(native['materials'])} material slots for {len(mesh['submeshes'])} submesh; preserve repeated material draws explicitly")
        callback = re.search(r'Callback_Component_DrawCustom\s*=\s*ref\s+(\S+)', sec[entry['section']])
        if not callback: raise ValueError('missing explicit draw callback')
        draws = selected_draws(sec, callback[1])
        states = []
        for draw in draws:
            if draw['bindings'] not in states: states.append(draw['bindings'])
        strides = [int(value(sec[draws[0]['buffers'][f'vb{i}']], 'stride')) for i in range(3)]
        mappings[entry['hash']] = {'component': cid, 'input_strides': strides,
            'skin': 'unorm16x4_uint16x4' if strides[2] == 16 else None,
            'material_rules': [{'bindings': state, 'component': None, 'slot': None,
                                'evidence': '', 'textures': []} for state in states]}
        blocked.append(f"{entry['hash']}: verify {len(states)} material state(s), including global overrides")
        evidence_rows.append({'component': cid, 'hash': entry['hash'], 'world_renderer': native['id'],
            'world_path': native['path'], 'ui_renderer': companions[0]['id'] if len(companions) == 1 else None,
            'world_ui_errors': errors, 'serialized_layout': mesh['serialized_layout'],
            'materials': native['materials'], 'submeshes': mesh['submeshes'], 'draws': draws})
        if (len(draws) == 1 and len(mesh['submeshes']) == 1 and len(native['materials']) > 1):
            evidence_rows[-1]['material_replay_candidate'] = {
                'component': cid, 'slot': 0, 'repeat_material_slots': list(range(1, len(native['materials']))),
                'evidence': 'native renderer material slots exceed its single submesh; source selects one draw',
                'texture_mapping_verified': False}
    ranges = skeleton_ranges(sec)
    for entry in entries:
        native, group = entry['native'], entry['group']
        if group not in ranges or len(native['bones']) != ranges[group][1]:
            raise ValueError(f'group {group}: native palette/range count differs')
        name = re.sub(r'_lod\d+$', '_lod0', native['mesh_name'])
        targets = [(cid, e['native']) for cid, e in enumerate(lod0) if e['native']['mesh_name'] == name]
        if len(targets) != 1: raise ValueError(f'group {group}: ambiguous LOD0 family')
        cid, target = targets[0]
        indices = remap_bones(native, target, meshes)
        mapping = {'component': cid, 'indices': indices,
                   'evidence': f"INI group {group}; renderer {native['id']} -> {target['id']}; identical Transform IDs and bindposes"}
        if str(group) in groups and groups[str(group)] != mapping:
            raise ValueError('conflicting source skeleton group')
        groups[str(group)] = mapping
    overrides = texture_overrides(sec)
    if overrides: blocked.append(f'{len(overrides)} global texture overrides need native texture identities')
    return {'schema': 1, 'kind': 'native_profile_preparation', 'conversion_ready': False,
            'source_snapshot': database['source'].get('snapshot'), 'world_resource': world, 'ui_resource': ui,
            'blockers': blocked, 'components': evidence_rows, 'global_texture_overrides': overrides,
            'profile_draft': {'schema': 2, 'verified': False,
                'evidence': 'Offline candidate joins only; see preparation blockers',
                'components': components, 'entries': mappings, 'skeleton_groups': groups,
                'texture_overrides': {o['hash']: {'resource': o['resource'], 'evidence': '', 'disposition': None}
                                      for o in overrides}}}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('source', type=Path)
    parser.add_argument('--database', type=Path, required=True)
    parser.add_argument('--world', required=True)
    parser.add_argument('--ui', required=True)
    parser.add_argument('--ini')
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    src = Source(args.source)
    try:
        ini = select_ini(src, args.ini)
        report = prepare(json.loads(args.database.read_text(encoding='utf-8-sig')),
                         sections(src.read_exact(ini).decode('utf-8-sig')), args.world, args.ui)
        report['source_ini'] = ini
    finally:
        src.close()
    args.output.parent.mkdir(parents=True, exist_ok=True)
    temporary = args.output.with_suffix(args.output.suffix + '.tmp')
    temporary.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    temporary.replace(args.output)
    print(json.dumps({'components': len(report['components']),
                      'groups': len(report['profile_draft']['skeleton_groups']), 'blockers': report['blockers']}))


if __name__ == '__main__':
    main()
