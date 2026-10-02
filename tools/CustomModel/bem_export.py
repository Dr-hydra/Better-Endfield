"""Normalize creator output to the oldest BEM format that carries its features."""
import copy
import bem_v1 as bem


def prepare_export(manifest, payloads):
    """Upgrade fixed appearances when extended resources require composable BEM.

    This is an explicit creator operation, not a permissive package reader. Old
    fixed packages keep their layout. Every upgraded draw owns its exact index
    range, so selecting an appearance does not read another draw's indices.
    """
    manifest = copy.deepcopy(manifest)
    payloads = list(payloads)
    aliases = any(c.get('bone_name_aliases') for c in manifest['target']['components'])
    slots = bool(manifest.get('texture_slots'))
    skin32 = any(m['streams'][2]['stride'] == 32 for m in manifest['meshes'])
    overrides = any(c.get('material_overrides') for a in manifest.get('appearances', []) for c in a['components'])
    extended_caps = any(c in ('texture-slots', 'resource-bone-aliases') for c in manifest['required_capabilities'])
    shapes = bool(manifest.get('parameters') or manifest.get('mesh_deformations') or
                  any(c in ('body-parameters', 'mesh-position-deltas') for c in manifest['required_capabilities']))
    if 'option_groups' not in manifest and (aliases or slots or skin32 or overrides or extended_caps or shapes):
        appearances = manifest.pop('appearances')
        default = manifest.pop('default_appearance_id')
        bem.require(0 < len(appearances) <= 64, 'Expected 1..64 appearances')
        bem.require(default in [a['id'] for a in appearances], 'Default appearance missing')
        group = 'appearance'
        manifest['option_groups'] = [dict(id=group, name='外观', default=default,
            choices=[{k: v for k, v in a.items() if k in ('id', 'name', 'description', 'preview')} for a in appearances])]
        rules = [dict(target=i, candidates=[]) for i in range(len(manifest['target']['components']))]
        for appearance in appearances:
            bem.require([c['target'] for c in appearance['components']] == list(range(len(rules))),
                        'Appearance must specify every target component in order')
            for component in appearance['components']:
                candidate = {k: v for k, v in component.items() if k != 'target'}
                candidate['when'] = {'eq': [group, appearance['id']]}
                rules[component['target']]['candidates'].append(candidate)
        manifest['component_rules'] = rules
        for mesh in manifest['meshes']:
            if 'indices' not in mesh: continue
            raw = payloads[mesh.pop('indices')]
            size = mesh['index_size']
            count = mesh.pop('index_count')
            bem.require(len(raw) == count * size, 'Index buffer length mismatch')
            end = 0
            for draw in mesh['draws']:
                start = draw.pop('start')
                bem.require(start == end and draw['count'] > 0 and draw['count'] % 3 == 0,
                            'Draws must partition IB')
                end += draw['count']
                bem.require(end <= count, 'Draw indices outside buffer')
                indices = raw[start * size:end * size]
                try: reference = payloads.index(indices)
                except ValueError:
                    reference = len(payloads)
                    payloads.append(indices)
                draw['indices'] = reference
            bem.require(end == count, 'Draws do not cover IB')
        manifest['required_capabilities'] = [c for c in manifest['required_capabilities'] if c != 'fixed-appearances']
        if 'composable-options' not in manifest['required_capabilities']:
            manifest['required_capabilities'].append('composable-options')
    if 'option_groups' in manifest:
        caps = manifest['required_capabilities']
        for capability, used in (('resource-bone-aliases', aliases), ('texture-slots', slots),
            ('keep-material-textures', any(c.get('material_overrides') for r in manifest['component_rules'] for c in r['candidates']))):
            if used and capability not in caps: caps.append(capability)
    return manifest, payloads


def prepare_builder(builder):
    builder.m, builder.payloads = prepare_export(builder.m, builder.payloads)
    return builder


def package_overrides(manifest, package):
    if not package: return
    bem.require(set(package) <= {'id', 'name', 'author', 'version'}, 'Unknown package parameter')
    for key, value in package.items():
        bem.require(isinstance(value, str) and bool(value), 'Empty package parameter: ' + key)
        manifest['package_id' if key == 'id' else key] = value
    bem.identity(manifest['package_id'])
