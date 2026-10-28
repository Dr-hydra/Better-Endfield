"""Narrow offline audit of actions descriptors against the installed PE/metadata.

Resolves local C++ string constants before matching, unlike the literal-only
audit. Never launches the game, rewrites binaries, or computes content hashes.
"""
from __future__ import annotations
import argparse
import collections
import json
import re
import struct
import sys
from pathlib import Path


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--repo', type=Path, required=True)
    parser.add_argument('--game', type=Path, required=True)
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    sys.path.insert(0, str(args.repo / 'tools/HookInlineScan'))
    from extract_targeted_metadata import Pe, Metadata
    path = args.repo / 'native/modules/actions/module.cpp'
    source = path.read_text(encoding='utf-8')
    literals = dict(re.findall(r'constexpr char\s+(\w+)\[\]\s*=\s*"([^"]*)"', source))

    def value(token):
        token = token.strip()
        if token == 'nullptr':
            return None
        if token.startswith('"'):
            return json.loads(token)
        if token.isdigit():
            return int(token)
        return literals[token]

    def tokens(text):
        return re.findall(r'"(?:\\.|[^"\\])*"|\b\w+\b', text)

    descriptors = []
    for kind, array, enum in [('method', 'g_methods', 'MethodId'), ('field', 'g_fields', 'FieldId')]:
        ids = [x.strip() for x in re.search(r'enum ' + enum + r'\s*\{(.*?)\}', source, re.S)[1].split(',')]
        match = re.search(r'\b' + array + r'\[\]\s*=\s*\{(.*?)\n\};', source, re.S)
        entries = list(re.finditer(r'\{\{(.*?)\}\}', match[1], re.S))
        assert len(entries) == len(ids) - 1, (kind, len(entries), len(ids))
        for ident, entry in zip(ids, entries):
            parts = [value(x) for x in tokens(entry[1])]
            asm, ns, cls, name = parts[:4]
            d = dict(kind=kind, id=ident, asm=asm, cls=ns+'.'+cls if ns else cls, name=name,
                     source_line=source.count('\n', 0, match.start(1)+entry.start())+1)
            if kind == 'method':
                d.update(types=parts[4].split('|') if parts[4] else [], ret=parts[5], count=parts[6])
            else:
                d['type'] = parts[4]
            descriptors.append(d)

    constants = []
    for match in re.finditer(r'\b(constant|other_constant|static_field)\(([^;\n]*)\)', source):
        raw = match[2]
        if '?' in raw:
            continue
        try:
            vals = [value(x) for x in tokens(raw)]
        except KeyError:
            continue
        if match[1] == 'other_constant' and len(vals) == 4:
            asm, ns, cls, name = vals
        elif len(vals) == 3:
            asm = literals['kGame']
            ns, cls, name = vals
        else:
            continue
        constants.append(dict(kind='constant' if match[1] != 'static_field' else 'static-field',
                              asm=asm, cls=ns+'.'+cls, name=name,
                              source_line=source.count('\n', 0, match.start())+1))
    constants.extend(dict(kind='constant', asm=literals['kGame'], cls='Beyond.Gameplay.ModelPartEnum', name=x)
                     for x in ['MeshGroup1', 'MeshGroup2'])

    selected = {x['cls'].rsplit('.', 1)[-1] for x in descriptors+constants}
    selected.update(['Dictionary`2', 'ObjectPtr`1'])
    pe = Pe(args.game / 'GameAssembly.dll')
    metadata = Metadata(args.game / 'Endfield_Data/il2cpp_data/Metadata/global-metadata.dat', pe)
    methods, classes = metadata.extract(selected, include_ifix=False)
    for cls in classes:
        row = metadata.types[cls['type_index']]
        for index, field in enumerate(cls['fields']):
            field_pos = metadata.sections[11][0] + (row[8]+index)*12
            type_index = struct.unpack_from('<i', metadata.data, field_pos+4)[0]
            attributes = pe.unpack('<QI', metadata.typeptr(type_index))[1] & 0xffff
            field['attributes'] = hex(attributes)
            field['static'] = bool(attributes & 0x10)
    class_lookup = {(x['asm'], x['cls']): x for x in classes}
    method_lookup = collections.defaultdict(list)
    for method in methods:
        method_lookup[(method['asm'], method['cls'], method['name'])].append(method)

    # IL2CPP runtime type display omits the generic declaration's arity suffix.
    # Preserve metadata type text alongside comparison-normalized text.
    def normalize_type(typename):
        return re.sub(r'`\d+(?=<)', '', typename.replace('/', '.').replace('+', '.'))

    results = []
    matched_methods = {}
    for descriptor in descriptors+constants:
        record = dict(descriptor)
        key = (record['asm'], record['cls'])
        if record['kind'] == 'method':
            candidates = method_lookup.get((*key, record['name']), [])
            exact = [x for x in candidates if x['count'] == record['count'] and
                     list(map(normalize_type, x['types'])) == record['types'] and
                     normalize_type(x['ret']) == record['ret']]
            record['status'] = 'matched' if len(exact) == 1 and exact[0]['rva'] else 'missing' if not candidates else 'signature-or-entry-review'
            if exact:
                record['actual'] = [dict(types=x['types'], ret=x['ret'], rva=hex(x['rva']), modifiers=x['modifiers'], token=hex(x['token'])) for x in exact]
                matched_methods[record['id']] = exact[0]
            elif candidates:
                record['candidates'] = candidates
            else:
                record['class_method_names'] = [x['name'] for x in methods if (x['asm'], x['cls']) == key]
        else:
            fields = class_lookup.get(key, {}).get('fields', [])
            candidates = [x for x in fields if x['name'] == record['name']]
            exact = [x for x in candidates if 'type' not in record or normalize_type(x['type']) == record['type']]
            record['status'] = 'matched' if len(exact) == 1 else 'missing' if not candidates else 'type-review'
            if candidates:
                record['actual'] = candidates
            if record['kind'] == 'field' and exact and (exact[0]['offset'] is None or exact[0]['offset'] < 0):
                record['status'] = 'instance-offset-review'
            if record['kind'] == 'constant' and exact and 'value' not in exact[0]:
                record['status'] = 'constant-value-review'
            if record['kind'] == 'static-field' and exact and (not exact[0]['static'] or exact[0]['type'] != 'System.Int32'):
                record['status'] = 'static-int32-review'
            if record['kind'] == 'field' and exact and exact[0]['static']:
                record['status'] = 'unexpected-static-field'
        results.append(record)

    instance_ids = ['Start','PreLate','Interrupt','ForceStop','ForceCurrent','Release','SetDashing',
                    'FlowTick','PerformClear','AddCommand','CrossFade','TryInterruptCommand','DoExit','ShowObject',
                    'AudioMonoPost','AudioMonoComponent','EffectDuration','EffectFinish','EffectStop',
                    'EffectActionPlay','EffectManualFollow','MeshGroupShow','VisualPerformId','VisualPerformEntity','PoseTail']
    static_ids = ['ShouldInterrupt','AudioEntityPost','AudioHash','UnityAlive',
                  'JsspsiAudioObjectId','JsspsiStopSound']
    for ident, expected_static in [(x, False) for x in instance_ids]+[(x, True) for x in static_ids]:
        actual = matched_methods.get(ident)
        status = 'matched' if actual and ('static' in actual['modifiers']) == expected_static else 'abi-review'
        results.append(dict(kind='static-instance', id=ident, expected_static=expected_static, status=status,
                            actual_modifiers=actual['modifiers'] if actual else None))
    dynamic = [x for x in methods if (x['cls'] == 'System.Collections.Generic.Dictionary`2' and x['name'] in ['ContainsKey','get_Item']) or
               (x['cls'] == 'Beyond.ObjectPtr`1' and x['name'] == 'Lock')]
    pe_header = struct.unpack_from('<I', pe.data, 0x3c)[0]
    export_rva = struct.unpack_from('<I', pe.data, pe_header+24+112)[0]
    export_pos = pe.offset(export_rva)
    name_count = struct.unpack_from('<I', pe.data, export_pos+24)[0]
    names_rva = struct.unpack_from('<I', pe.data, export_pos+32)[0]
    exports = set()
    for index in range(name_count):
        name_rva = pe.unpack('<I', names_rva+index*4)[0]
        start = pe.offset(name_rva)
        exports.add(pe.data[start:pe.data.index(b'\0', start)].decode('ascii'))
    for name in sorted(set(re.findall(r'"(il2cpp_[^"]+)"', source))):
        results.append(dict(kind='pe-export', name=name, status='matched' if name in exports else 'missing'))
    summary = dict(collections.Counter(x['kind']+':'+x['status'] for x in results))
    files = {str(path.relative_to(args.game)):dict(size=path.stat().st_size, mtime_ns=path.stat().st_mtime_ns)
             for path in [args.game/'GameAssembly.dll', args.game/'Endfield_Data/il2cpp_data/Metadata/global-metadata.dat']}
    report = dict(scope=__doc__, source=dict(game=str(args.game), files=files, metadata_type_count=metadata.type_count),
                  summary=summary, results=results, dynamic_generic_method_definitions=dynamic,
                  limits=['Native runtime invocation and hook execution are not tested.',
                          'Character template IDs, perform IDs, VFX names, pose timing and action logic IDs live in game assets, not PE metadata.',
                          'Static readonly animator hashes are initialized at runtime; this audit verifies their field presence and type only.',
                          'Generic method definitions do not establish runtime generic instantiation addresses.'])
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, ensure_ascii=False, indent=2)+'\n', encoding='utf-8')
    print(json.dumps(dict(summary=summary, review=[x for x in results if x['status'] != 'matched'],
                          constants=[dict(cls=x['cls'], name=x['name'], value=x.get('actual',[{}])[0].get('value')) for x in results if x['kind']=='constant'],
                          dynamic_generic_count=len(dynamic), output=str(args.output)), ensure_ascii=False, indent=2))


if __name__ == '__main__':
    main()
