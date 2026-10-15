"""Read-only ELF equivalent of the production named Unity mesh-layout probe."""
from pathlib import Path
import bisect
import json
import struct
import sys
import numpy as np

repo = Path(__file__).resolve().parents[5]
source = repo/'inputs/1.6.4/Android/libunity.so'
data = source.read_bytes()
header = struct.unpack_from('<16sHHIQQQIHHHHHH', data)
segments = []
eh = None
for index in range(header[10]):
    kind, flags, raw, va, _, size, _, _ = struct.unpack_from('<II6Q', data, header[5]+index*header[9])
    if kind == 1:
        segments.append((va, flags, data[raw:raw+size]))
    elif kind == 0x6474e550:
        eh = (va, data[raw:raw+size])
assert eh and eh[1][:4] == b'\x01\x1b\x03\x3b', 'Unsupported EH frame header'
count = struct.unpack_from('<I', eh[1], 8)[0]
functions = sorted(eh[0]+struct.unpack_from('<i',eh[1],12+i*8)[0] for i in range(count))
def owner(address):
    return functions[bisect.bisect_right(functions,address)-1]

names = set()
for va, flags, blob in segments:
    if flags & 1:
        continue
    start = 0
    while True:
        found = blob.find(b'm_BonesPerVertex\0', start)
        if found < 0:
            break
        if found == 0 or blob[found-1] == 0:
            names.add(va+found)
        start = found+1

def address(page_word, add_word, position):
    displacement = ((page_word >> 29) & 3) | (((page_word >> 5) & 0x7ffff) << 2)
    if displacement & (1 << 20):
        displacement -= 1 << 21
    return (position & ~4095)+displacement*4096+((add_word >> 10) & 4095)

descriptor, reader = [], []
for va, flags, blob in segments:
    if not flags & 1 or va % 4:
        continue
    words = np.frombuffer(blob[:len(blob)//4*4], dtype='<u4')
    starts = np.nonzero((words & 0x9f00001f) == 0x90000001)[0]
    for index in starts:
        if index+16 > len(words):
            continue
        w = [int(x) for x in words[index:index+16]]
        position = va+int(index)*4
        if ((w[1]&0xffc0001f)==0x91000003 and (w[2]&0xffc003ff)==0x91000021 and
            (w[3]&0xffe0ffff)==0xaa0003e0 and w[4]==0x2a1f03e4 and (w[5]&0xfc000000)==0x94000000):
            mesh, context = (w[1]>>5)&31, (w[3]>>16)&31
            offset = (w[1]>>10)&4095
            if (19<=mesh<=28 and 19<=context<=28 and mesh!=context and offset and offset%4==0 and
                address(w[0],w[2],position) in names):
                descriptor.append({'site':position,'offset':offset,'function':owner(position)})
        if not ((w[1]&0xffc003ff)==0x91000021 and (w[2]&0xffc003ff)==0x910003e3 and
                (w[3]&0xffe0ffff)==0xaa0003e0 and (w[4]&0xffe0ffff)==0xaa0003e2 and w[5]==0x2a1f03e4 and
                (w[6]&0xfc000000)==0x94000000 and (w[7]&0xff00001f)==0x34000000 and w[8]==0x7100041f and
                (w[9]&0xffc00000)==0x91000000 and (w[10]&0xff00001f)==0x5400000a and
                (w[11]&0xffc003ff)==0xf94003e8 and (w[12]&0xff00001f)==0xb4000008 and
                (w[13]&0xffe0ffff)==0xaa0003e0 and (w[14]&0xffe0ffff)==0xaa0003e1 and w[15]==0xd63f0100):
            continue
        mesh, archive, offset = w[9]&31,(w[3]>>16)&31,(w[9]>>10)&4095
        if not (19<=mesh<=28 and 19<=archive<=28 and mesh!=archive and ((w[9]>>5)&31)==mesh and
                ((w[13]>>16)&31)==mesh and ((w[14]>>16)&31)==archive and offset and offset%4==0 and
                ((w[2]>>10)&4095)==(((w[11]>>10)&4095)*8) and address(w[0],w[1],position) in names):
            continue
        function = owner(position)
        if function != owner(position+60) or function < va:
            continue
        prologue = words[(function-va)//4:min(int(index),(function-va+64)//4)]
        if (0xaa0003e0|mesh) not in prologue or (0xaa0103e0|archive) not in prologue:
            continue
        reader.append({'site':position,'offset':offset,'function':function})

offsets = set(item['offset'] for item in descriptor+reader)
functions_seen = set(item['function'] for item in descriptor+reader)
result = {'source':str(source),'bytes':len(data),'version':'Android 1.6.4',
          'named_field_addresses':[hex(x) for x in sorted(names)],
          'descriptor_paths':descriptor,'reader_paths':reader,'agreeing_functions':len(functions_seen),
          'unique_offset':next(iter(offsets)) if len(offsets)==1 else None,
          'static_pass':len(offsets)==1 and len(functions_seen)>=2 and bool(descriptor) and bool(reader),
          'scope':'production descriptor/reader acceptance checks against installed ELF only; no live memory or setter writes'}
(Path(__file__).parent/'native-mesh-layout.json').write_text(json.dumps(result,indent=2)+'\n',encoding='utf-8')
print(json.dumps(result))
