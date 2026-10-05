"""Identify the source ps-t2 binding from the referenced native shader metadata.

Reads game shader descriptors only; never executes or rewrites Mod shaders.
The full descriptor report is generated with tools/CustomModel/inspect_shader_bindings.py.
"""
import json
from collections import Counter

from audit_inputs import ROOT, REPO, load


def audit():
    _, graph, objects = load()
    row = next(r for r in graph['renderers']
               if r['resource_root'] == 'chr_0030_zhuangfy_ult_postmodel'
               and r['mesh_name'] == 'S_actor_zhuangfy_body_03_lod0')
    material = objects[row['materials'][0]['id']]
    descriptors = json.loads((ROOT / 'character-shader-bindings.json').read_text(encoding='utf-8'))
    assert material['shader']['id'] == descriptors['shader_id']
    slots = Counter()
    matching = []
    for record in descriptors['parameters']:
        textures = [(group['name'], entry['pixel_slot'], entry['name'])
                    for group in record['groups'] for entry in group['bindings']
                    if entry['kind'] == 2]
        for group, slot, name in textures:
            if slot == 2:
                slots[(group, name)] += 1
        local = {slot: name for group, slot, name in textures if group == 'PerMaterial'}
        if all(local.get(slot) == name for slot, name in
               [(17, '_BaseMap'), (18, '_MetallicGlossMap'), (19, '_BumpMap')]):
            matching.append(dict(lod=record['lod'], program=record['program'],
                                 slot2=[dict(group=group, name=name)
                                        for group, slot, name in textures if slot == 2]))
    assert slots and all(group == 'Global' for group, _ in slots)
    assert matching and all(len(r['slot2']) == 1 and r['slot2'][0]['group'] == 'Global'
                            and r['slot2'][0]['name'] in ('_CSMShadowmapTex', '_PunctualLightShadowTexV2')
                            for r in matching)
    result = dict(schema=1, material=material['name'], shader_id=descriptors['shader_id'],
                  shader_name=descriptors['shader_name'],
                  metadata_source=str(REPO / 'research/custom_model/1.5.3/shared/'
                                      'postmodel-native-parser-history/shader-metadata'),
                  slot2_bindings=[dict(group=g, name=n, parameter_records=count)
                                  for (g, n), count in slots.items()],
                  matching_material_layouts=matching,
                  conclusion='M0178 Component0 ps-t2 is a global shadow resource, not a BEM material texture property',
                  limitations=['Does not prove a captured texture hash identifies a current live render target',
                               'Does not reproduce source global shadow replacement',
                               'No in-game visual validation'])
    (ROOT / 'shadow-slot-audit.json').write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps(dict(material=result['material'], slot2=result['slot2_bindings'],
                          matching_material_layouts=len(matching))))


if __name__ == '__main__':
    audit()
