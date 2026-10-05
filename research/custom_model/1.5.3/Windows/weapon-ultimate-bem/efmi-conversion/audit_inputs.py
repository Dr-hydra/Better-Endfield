"""Read explicitly selected EFMI inputs and original game identities; never execute source INIs."""
import json
from pathlib import Path
import re
import struct
import sys

REPO = Path(__file__).resolve().parents[6]
sys.path.insert(0, str(REPO / 'tools/CustomModel'))
from efmi_source import sections, value
from parse_native_models import parse
from import_efmi_identities import FORMATS, crc32c

ROOT = Path(__file__).resolve().parent
SOURCES = {
    'M0178': Path('G:/zmd/庄方宜/庄方宜-大招状态-终极状态 by 幽魂小猫/[DaisyMeow]Zhuang Fangyi Ult 1.5FIX/ZUlt'),
    'M0184': Path('G:/zmd/庄方宜/庄方宜-心灵+大招形态/庄方宜/大招形态'),
}


def load():
    raw = json.loads((ROOT / 'native-identities.json').read_text(encoding='utf-8-sig'))
    graph = parse(raw)
    objects = {o['id']: o for o in raw['objects']}
    return raw, graph, objects


def texture_identities(objects):
    result = {}
    for obj in objects.values():
        if obj['type'] != 'Texture2D': continue
        identity = obj.get('resource_identity', {})
        pair = identity.get('format'), identity.get('color_space')
        if pair not in FORMATS: continue
        levels = identity.get('levels', [])
        for index, level in enumerate(levels):
            descriptor = [level['width'], level['height'], len(levels) - index, 1, FORMATS[pair][0], 1, 0, 0, 8, 0, 0]
            source = f"{crc32c(int(level['legacy_crc32c'], 16), struct.pack('<11I', *descriptor)):08x}"
            result.setdefault(source, set()).add(obj['name'])
    return result


def audit():
    raw, graph, objects = load(); tids = texture_identities(objects)
    rows = [r for r in graph['renderers'] if r['resource_root'] == 'chr_0030_zhuangfy_ult_postmodel'
            and '/lod0/' in r['path']]
    result = {}
    for task, root in SOURCES.items():
        ini = sections((root / 'mod.ini').read_text(encoding='utf-8-sig'))
        textures = []
        for name, body in ini.items():
            if not name.startswith('TextureOverride_Texture'): continue
            identity = value(body, 'hash'); replacement = value(body, 'this')
            textures.append(dict(section=name, identity=identity, resource=replacement,
                                 native_names=sorted(tids.get(identity, []))))
        components = []
        for name, body in ini.items():
            if not re.fullmatch(r'TextureOverride_Component\d+', name): continue
            identity, count = value(body, 'hash'), int(value(body, 'match_index_count'))
            matches = [r for r in rows if objects[r['mesh_id']].get('resource_identity', {}).get('data_crc32c') == identity
                       and r['original_index_count'] == count]
            components.append(dict(section=name, identity=identity, count=count,
                                   native_matches=[r['mesh_name'] for r in matches]))
        result[task] = dict(root=str(root), components=components, textures=textures)
    (ROOT / 'input-audit.json').write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({t: dict(components=len(r['components']), matched=sum(len(c['native_matches']) == 1 for c in r['components']),
                               textures=len(r['textures']), unresolved=[x['section'] for x in r['textures'] if not x['native_names']])
                      for t, r in result.items()}, ensure_ascii=False))


if __name__ == '__main__': audit()
