"""Extract only SkillData/BuffData's reachable serializer schema for an update."""
import argparse
import json
import re
import struct
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parents[1]/'HookInlineScan'))
from extract_targeted_metadata import Metadata, Pe


def type_args(name):
    if name.endswith('[]'):
        return [name[:-2]]
    if '<' not in name:
        return []
    args = name[name.index('<')+1:-1]
    depth, start, result = 0, 0, []
    for i, ch in enumerate(args):
        depth += (ch == '<')-(ch == '>')
        if ch == ',' and depth == 0:
            result.append(args[start:i]); start = i+1
    return result+[args[start:]]


def extract(game, extra_types=()):
    m = Metadata(game/'Endfield_Data/il2cpp_data/Metadata/global-metadata.dat', Pe(game/'GameAssembly.dll'))
    names = {m.typename(i): i for i in range(len(m.types))}
    wrappers, wrapper_rows = {}, {}
    def read_methods(row):
        return [struct.unpack_from('<5iI4H', m.data, m.sections[5][0]+(row[9]+j)*32) for j in range(row[17])]
    for i, row in enumerate(m.types):
        if not m.string(row[0]).endswith('ForMemoryPack'):
            continue
        methods = read_methods(row)
        inst = next((v for v in methods if re.fullmatch(r'get_+instance', m.string(v[0]))), None)
        if not inst:
            continue
        underlying = m.resolve(inst[2])
        fields = []
        for v in methods:
            name = m.string(v[0])
            if name.startswith('set___') and not re.fullmatch(r'set_+instance', name):
                typ = struct.unpack_from('<i', m.data, m.sections[10][0]+v[3]*12+8)[0]
                fields.append([name[6:-2], m.resolve(typ)])
        entry = dict(wrapper=m.typename(i), fields=fields,
                     parent=m.resolve(row[4]) if row[4] >= 0 else '',
                     has_deserializer=any(m.string(v[0]) == 'Deserialize' for v in methods))
        wrappers[underlying] = entry
        wrapper_rows[entry['wrapper']] = entry
    def all_fields(row):
        parent = wrapper_rows.get(row['parent'])
        return (all_fields(parent) if parent else [])+row['fields']
    parents = {}
    for name, i in names.items():
        row = m.types[i]
        if row[4] >= 0:
            parents[name] = m.resolve(row[4])
    def descends(name, base):
        for _ in range(32):
            name = parents.get(name)
            if not name: return False
            if name == base: return True
        return False
    primitive_sizes = {'System.Boolean':1,'System.Byte':1,'System.SByte':1,'System.Int16':2,'System.UInt16':2,'System.Int32':4,'System.UInt32':4,'System.Int64':8,'System.UInt64':8,'System.Single':4,'System.Double':8,'System.Char':2}
    raw_sizes = {}
    def unmanaged_size(name):
        if name in primitive_sizes: return primitive_sizes[name]
        if name in raw_sizes: return raw_sizes[name]
        raw_sizes[name] = None
        if name not in names: return None
        index = names[name]
        row = m.types[index]
        if not row[25] & 1: return None
        for j in range(row[19]):
            n,t,token = struct.unpack_from('<iiI', m.data, m.sections[11][0]+(row[8]+j)*12)
            if m.pe.unpack('<QI',m.typeptr(t))[1]&0x10: continue
            if unmanaged_size(m.resolve(t)) is None: return None
        ptr = m.pe.unpack('<Q', m.registration[13]+index*8)[0]
        size = m.pe.unpack('<I',ptr)[0]-16
        if not 0 < size < 4096: raise ValueError(f'Invalid value size {name}: {size}')
        raw_sizes[name] = size
        return size
    queue = ['Beyond.Gameplay.Core.SkillData', 'Beyond.Gameplay.Core.BuffData', *extra_types]
    result = {}
    while queue:
        name = queue.pop()
        if name in result: continue
        if (name.endswith('[]') or '<' in name) and name not in wrappers:
            queue.extend(type_args(name)); continue
        if name in wrappers:
            row = wrappers[name]
            fields = all_fields(row)
            result[name] = dict(wrapper=row['wrapper'], fields=fields)
            queue.extend(t for f, t in fields)
            if not row['has_deserializer']:
                derived = sorted(n for n in wrappers if n != name and descends(n, name))
                result[name]['union_candidates'] = derived
                queue.extend(derived)
        elif name in names:
            row = m.types[names[name]]
            fields = []
            for j in range(row[19]):
                n, t, token = struct.unpack_from('<iiI', m.data, m.sections[11][0]+(row[8]+j)*12)
                fields.append([m.string(n), m.resolve(t)])
            if fields and fields[0][0] == 'value__':
                result[name] = dict(enum=fields[0][1])
            elif name.startswith(('System.', 'UnityEngine.')):
                result[name] = dict(native_fields=fields)
            else:
                result[name] = dict(unknown=True, fields=fields)
        else:
            result[name] = dict(unknown=True)
        size = unmanaged_size(name)
        if size is not None and name not in primitive_sizes:
            result[name]['raw_size'] = size
    return result


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--game', required=True, type=Path)
    parser.add_argument('--output', required=True, type=Path)
    parser.add_argument('--extra-types', type=Path)
    args = parser.parse_args()
    extra = json.loads(args.extra_types.read_text(encoding='utf-8')) if args.extra_types else []
    result = extract(args.game, extra)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(result, ensure_ascii=False, indent=2)+'\n', encoding='utf-8')
    print(json.dumps(dict(types=len(result), unions={n:len(v['union_candidates']) for n,v in result.items() if 'union_candidates' in v}, unknown=[n for n,v in result.items() if v.get('unknown')]), ensure_ascii=False))


if __name__ == '__main__': main()
