"""Report bounded DXGI/color/mip descriptor alternatives; no Mod script runs."""
import json
import struct
from audit_inputs import ROOT, SOURCES, load, sections, value, re, crc32c, texture_identities
from convert_efmi_poc import parse_dds


def audit():
    _, graph, objects = load()
    raw = json.loads((ROOT / 'native-textures.json').read_text(encoding='utf-8-sig'))
    byte_files = {o['id']: ROOT / 'native-textures' / o['raw_texture_file']
                  for o in raw['objects'] if 'raw_texture_file' in o}
    formats = {'BC7': (97, 98, 99), 'BC5': (82, 83), 'RGBA32': (27, 28, 29), 'DXT1': (70, 71, 72)}
    candidates = {}
    for obj in objects.values():
        if obj['type'] != 'Texture2D': continue
        identity = obj.get('resource_identity', {}); levels = identity.get('levels', [])
        for index, level in enumerate(levels):
            for dxgi in formats.get(identity.get('format'), []):
                for mips in sorted({1, len(levels)-index}):
                    desc = [level['width'], level['height'], mips, 1, dxgi, 1, 0, 0, 8, 0, 0]
                    key = f"{crc32c(int(level['legacy_crc32c'], 16), struct.pack('<11I', *desc)):08x}"
                    candidates.setdefault(key, []).append(dict(name=obj['name'], mip=index, descriptor=desc))
    result = {}
    for task, root in SOURCES.items():
        ini = sections((root / 'mod.ini').read_text(encoding='utf-8-sig')); entries = []
        for name, body in ini.items():
            if not name.startswith('Resource_Texture'): continue
            filename = value(body, 'filename'); identity = re.search(r't=([0-9a-f]+)', filename)[1]
            w,h,mips,fmt,srgb,data = parse_dds(filename, (root/filename).read_bytes())
            same = [objects[oid]['name'] for oid, path in byte_files.items()
                    if objects[oid]['width'] == w and objects[oid]['height'] == h and path.stat().st_size == len(data)
                    and path.read_bytes() == data]
            entries.append(dict(resource=name, identity=identity, dimensions=[w,h,mips], format=fmt,
                                descriptor_matches=candidates.get(identity, []), exact_full_payload_matches=same))
        result[task] = entries
    (ROOT/'texture-variant-audit.json').write_text(json.dumps(result,ensure_ascii=False,indent=2),encoding='utf-8')
    print(json.dumps({t: [dict(resource=x['resource'],dims=x['dimensions'],matches=x['descriptor_matches'],same=x['exact_full_payload_matches'])
                         for x in rows if not texture_identities(objects).get(x['identity'])] for t, rows in result.items()},ensure_ascii=False))


if __name__=='__main__': audit()
