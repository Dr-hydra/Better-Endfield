import json
from pathlib import Path
import ida_auto
import ida_bytes
import ida_funcs
import ida_hexrays
import ida_name
import ida_segment
import ida_typeinf
import ida_ua
import idc

root = Path(__file__).parent
data = json.loads((root / 'slice.json').read_text())
ida_auto.enable_auto(False)
idc.set_inf_attr(idc.INF_FILETYPE, idc.FT_PE)
ida_typeinf.set_compiler_id(ida_typeinf.COMP_MS)

def segment(start, end, name):
    s = ida_segment.segment_t()
    s.start_ea, s.end_ea, s.bitness, s.perm = start, end, 2, 5
    ida_segment.add_segm_ex(s, name, 'CODE', ida_segment.ADDSEG_QUIET | ida_segment.ADDSEG_SPARSE)

def symbol(name, rva):
    return ''.join(c if c.isalnum() or c == '_' else '_' for c in name) + '_' + hex(rva)[2:]

def ctype(name):
    return {'System.Void': 'void', 'System.Boolean': 'bool', 'System.Single': 'float',
            'System.Double': 'double', 'System.Int32': 'int', 'System.UInt32': 'unsigned int'}.get(name, '__int64')

ends = [part['end'] for fn in data['functions'] for part in fn['parts']]
starts = [part['start'] for fn in data['functions'] for part in fn['parts']]
starts += [e['rva'] for e in data['external']]
ends += [e['rva'] + 1 for e in data['external']]
segment(min(starts), max(ends), 'selected_code')
for ext in data['external']:
    r = ext['rva']
    ida_bytes.put_bytes(r, b'\xc3')
    ida_ua.create_insn(r)
    ida_funcs.add_func(r, r + 1)
    ida_name.set_name(r, symbol(ext['name'], r), ida_name.SN_NOWARN)
    m = ext.get('method')
    if m:
        args = [] if 'static' in m.get('modifiers', []) else ['__int64 self']
        args += [ctype(t) + ' a' + str(i) for i, t in enumerate(m['types'])]
        args += ['__int64 method']
        idc.SetType(r, ctype(m['ret']) + ' __fastcall ' + symbol(ext['name'], r) + '(' + ','.join(args) + ');')
for fn in data['functions']:
    for part in fn['parts']:
        ida_bytes.put_bytes(part['start'], bytes.fromhex(part['hex']))
        for ins in part['ins']:
            ida_ua.create_insn(ins)
    first = fn['parts'][0]
    ida_funcs.add_func(first['start'], first['end'])
    f = ida_funcs.get_func(first['start'])
    for part in fn['parts'][1:]:
        print('tail', hex(fn['rva']), hex(part['start']), ida_funcs.append_func_tail(f, part['start'], part['end']))
    ida_name.set_name(fn['rva'], symbol(fn['name'], fn['rva']), ida_name.SN_NOWARN)
ida_hexrays.init_hexrays_plugin()
with (root / 'pseudocode.c').open('w', encoding='utf-8') as stream:
    for fn in data['functions']:
        stream.write('\n// ' + fn['name'] + ' @' + hex(fn['rva']) + '\n')
        try:
            stream.write(str(ida_hexrays.decompile(fn['rva'])) + '\n')
        except Exception as e:
            stream.write('// decompile failed: ' + str(e) + '\n')
idc.qexit(0)
