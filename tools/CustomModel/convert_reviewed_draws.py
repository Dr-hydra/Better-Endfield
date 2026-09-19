"""Lower an explicitly reviewed source program to existing BEMPC25 callbacks.

This is a bounded recipe adapter, not a GPU/INI interpreter. Every source program
and shader must match the reviewed text; only declared static draws are emitted.
Native layouts and identities must still be verified against real observations.
"""
import argparse
import copy
import json
from pathlib import Path, PurePosixPath
import re

from convert_efmi_poc import Source, parse_dds
from efmi_source import sections, value
from finalize_native_profile import exact_native_mips
from hash_lod_lowering import convert, texture_overrides
from parse_native_models import apply_observations
from prepare_native_profile import paired_resource


def canonical_ini(text):
    return '\n'.join(' '.join(line.split(';', 1)[0].split()) for line in text.splitlines()
                     if line.split(';', 1)[0].strip())


def canonical_shader(text):
    return re.sub(r'\s+', '', re.sub(r'/\*.*?\*/|//[^\n]*', '', text, flags=re.S))


def relative(name):
    path = PurePosixPath(name.replace('\\', '/'))
    if path.is_absolute() or '..' in path.parts or ':' in str(path):
        raise ValueError('unsafe recipe resource path')
    return str(path)


def validate_source(src, recipe):
    ini = relative(recipe['ini'])
    if canonical_ini(src.read_exact(ini).decode('utf-8-sig')) != recipe['source_program']:
        raise ValueError('source INI differs from reviewed program; re-audit required')
    for name, expected in recipe['shader_programs'].items():
        if canonical_shader(src.read_exact(relative(name)).decode('utf-8-sig')) != expected:
            raise ValueError('source shader differs from reviewed program: '+name)
    return ini, sections(src.read_exact(ini).decode('utf-8-sig'))


class NormalizedSource:
    def __init__(self, source, ini, text):
        self.source, self.ini, self.text = source, ini, text.encode('utf-8')
        self.names = source.names

    def read_exact(self, name):
        return self.text if name == self.ini else self.source.read_exact(name)


def prepare(src, recipe, database, observations, native_textures, texture_dir):
    ini, sec = validate_source(src, recipe)
    if recipe.get('schema') != 1 or not recipe.get('evidence') or not recipe.get('semantic_notes'):
        raise ValueError('reviewed recipe evidence required')
    if native_textures.get('snapshot') != database['source']['snapshot']:
        raise ValueError('native texture snapshot differs')
    apply_observations(database, observations)
    observed_rows = {r['id']: r for r in observations['renderers']}
    if len(observed_rows) != len(observations['renderers']):
        raise ValueError('duplicate observations')
    profile = {'schema': 2, 'verified': True, 'render_verified': False,
               'evidence': recipe['evidence']+'; '+observations['evidence'],
               'source_snapshot': database['source']['snapshot'], 'components': {},
               'entries': {}, 'skeleton_groups': {}, 'texture_overrides': {},
               'semantic_notes': recipe['semantic_notes']}
    if recipe.get('allow_uint32_indices') is True:
        profile['allow_uint32_indices'] = True
    native_rows = {}
    for cid, entry in enumerate(recipe['entries']):
        pair = []
        for root in (recipe['world_resource'], recipe['ui_resource']):
            matches = [r for r in database['renderers'] if r['resource_root'] == root
                       and r['mesh_name'] == entry['native_mesh'] and '/lod0/' in r['path']
                       and r['original_index_count'] == entry['original_index_count']]
            if len(matches) != 1: raise ValueError('ambiguous/missing native renderer')
            pair.append(matches[0])
        w, u = pair
        for native in pair:
            live = observed_rows.get(native['id'])
            if not live or not live.get('complete'):
                raise ValueError('complete observation required for each native renderer')
            if live['bones'] != [{'name': b['name'], 'path': b['path']} for b in native['bones']]:
                raise ValueError('observed bone identity/order differs')
            if [m['name'] for m in live['materials']] != [m['name'] for m in native['materials']]:
                raise ValueError('observed material slots differ')
        errors = paired_resource(w, u, database['meshes'])
        if errors: raise ValueError('; '.join(errors))
        runtime = w.get('runtime_layout')
        if not runtime or not u.get('runtime_layout'):
            raise ValueError('both resources require observed declarations')
        if any(runtime[k] != u['runtime_layout'][k] for k in ('strides', 'attributes')):
            raise ValueError('native runtime declarations differ')
        if database['meshes'][w['mesh_id']]['bindposes'] != database['meshes'][u['mesh_id']]['bindposes']:
            raise ValueError('bindposes differ')
        native_rows[cid] = w
        profile['components'][str(cid)] = {'mesh_name': w['mesh_name'],
            'original_index_count': w['original_index_count'], 'strides': runtime['strides'],
            'attributes': runtime['attributes'], 'bone_names': [b['name'] for b in w['bones']],
            'materials': [m['name'] for m in w['materials']], 'evidence': profile['evidence']}

    # Declarations remain source data; all executable lists are replaced by
    # explicit reviewed callback draws. Original source files are never edited.
    normalized = {name: body for name, body in sec.items() if name.startswith('Resource')}
    overrides = texture_overrides(sec)
    evidence = {'global_textures': [], 'materials': [], 'semantic_notes': recipe['semantic_notes'],
                'render_verified': False}
    def raw_resource(name):
        filename = value(sec.get(name, ''), 'filename')
        if not filename: raise ValueError('resource missing filename')
        return src.read_exact(relative(str(PurePosixPath(ini).parent/relative(filename))))
    if set(recipe['global_textures']) != {o['hash'] for o in overrides}:
        raise ValueError('global texture inventory differs')
    for ov in overrides:
        target = recipe['global_textures'][ov['hash']]
        original = target['native_texture']
        if target['disposition'] == 'preserve_native':
            candidates = [t for t in native_textures['objects'] if t['type'] == 'Texture2D' and t['name'] == original]
            if len(candidates) != 1: raise ValueError('ambiguous native texture')
            t = candidates[0]; path = (texture_dir/t['raw_texture_file']).resolve()
            if path.parent != texture_dir.resolve(): raise ValueError('unsafe native texture file')
            formats = {t['graphics_format'] for r in observations['renderers'] for m in r['materials']
                       for t in m['textures'] if t['name'] == original}
            count = exact_native_mips(parse_dds(ov['resource'], raw_resource(ov['resource'])), t, path.read_bytes(), formats)
            profile['texture_overrides'][ov['hash']] = dict(resource=ov['resource'], disposition='preserve_native',
                reason='Exact native mip bytes; retain native full mip chain', evidence=f"{t['id']}; {count} bytes compared")
            evidence['global_textures'].append({'hash': ov['hash'], 'native': original, 'bytes_compared': count})
        elif target['disposition'] == 'material_rules':
            profile['texture_overrides'][ov['hash']] = dict(resource=ov['resource'], disposition='material_rules',
                original_texture=original, evidence=recipe['evidence'])
        else: raise ValueError('unknown global texture disposition')
        normalized[ov['section']] = sec[ov['section']]

    for cid, entry in enumerate(recipe['entries']):
        rules = []
        for rule in entry['material_rules']:
            rule = copy.deepcopy(rule); donor = native_rows[rule['component']]
            material = donor['materials'][rule['slot']]
            live = observed_rows[donor['id']]['materials'][rule['slot']]
            for tex in rule.get('textures', []):
                matching = [t for t in material['textures'] if t['property'] == tex['property'] and t['name']]
                if len(matching) != 1: raise ValueError('texture property has no native object')
                name = matching[0]['name']
                if sum(t['name'] == name for t in material['textures']) != 1 or not any(t['name'] == name for t in live['textures']):
                    raise ValueError('native texture pin is ambiguous or absent at runtime')
                tex['original_texture'] = name; del tex['property']
            if rule.pop('require_native_normal_disabled', False):
                floats = {p['Key']: p['Value'] for p in material['floats']}
                if '_NORMALMAP' in material['keywords'] or floats.get('_UseBumpMap') != 0:
                    raise ValueError('native skin normal keyword/state changed; re-audit')
                if any(t['name'] for t in material['textures'] if t['property'] == '_BumpMap'):
                    raise ValueError('native skin normal texture changed; re-audit')
            rules.append(rule)
            evidence['materials'].append({'component': cid, 'donor': rule['component'],
                                          'material': material['name'], 'textures': rule.get('textures', [])})
        native = profile['components'][str(cid)]
        profile['entries'][entry['hash']] = {'component': cid, 'input_strides': [16,12,12],
            'skin': 'unorm16x4_uint8x4', 'local_bones': {'component': cid,
                'indices': list(range(len(native['bone_names']))),
                'evidence': 'Reviewed copying/rebasing shader retains source renderer bone order; runtime native bones verified'},
            'material_rules': rules}
        section = f"TextureOverride_EntryPoint_LOD0.{entry['hash']}_{entry['original_index_count']}_0"
        callback = 'CommandListReviewed'+str(cid)
        normalized[section] = f"hash = {entry['hash']}\nmatch_first_index = 0\nmatch_index_count = {entry['original_index_count']}\nCommandList\\EFMIv1\\Callback_Component_DrawCustom = ref {callback}"
        lines = [f'{slot} = {res}' for slot,res in entry['buffers'].items()]
        for draw in entry['draws']:
            lines.extend(f'{slot} = {res}' for slot,res in draw['bindings'].items())
            lines.append(f"drawindexedinstanced = {draw['count']}, INSTANCE_COUNT, {draw['start']}, 0, FIRST_INSTANCE")
        normalized[callback] = '\n'.join(lines)
    text = '\n\n'.join(f'[{name}]\n{body}' for name,body in normalized.items())
    return NormalizedSource(src, ini, text), ini, profile, evidence


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('source', type=Path)
    for arg in ('recipe','database','observations','native-textures','texture-dir','output','report'):
        parser.add_argument('--'+arg, type=Path, required=True)
    args = parser.parse_args()
    load = lambda path: json.loads(path.read_text(encoding='utf-8-sig'))
    src = Source(args.source)
    try:
        normalized, ini, profile, evidence = prepare(src, load(args.recipe), load(args.database),
            load(args.observations), load(args.native_textures), args.texture_dir)
        payload, summary = convert(normalized, ini, profile)
        for path, data in ((args.report, {'conversion_ready': True, 'render_verified': False,
                           'native_output': summary, 'evidence': evidence}),
                          (args.report.with_suffix('.profile.json'), profile)):
            path.parent.mkdir(parents=True, exist_ok=True)
            temp = path.with_suffix(path.suffix+'.tmp')
            temp.write_text(json.dumps(data, ensure_ascii=False, indent=2, allow_nan=False), encoding='utf-8'); temp.replace(path)
        args.output.parent.mkdir(parents=True, exist_ok=True)
        temp = args.output.with_suffix(args.output.suffix+'.tmp'); temp.write_bytes(payload); temp.replace(args.output)
        print(json.dumps(summary)); print(f'Package bytes: {len(payload)}; render verification pending')
    finally:
        src.close()


if __name__ == '__main__': main()
