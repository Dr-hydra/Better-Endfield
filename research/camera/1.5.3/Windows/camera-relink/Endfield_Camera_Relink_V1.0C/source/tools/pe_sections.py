# -*- coding: utf-8 -*-
# 比较两个 PE 文件的各节哈希 (构建不可逐字节复现, 但 .text 应一致)
import hashlib
import struct
import sys


def sections(path):
    with open(path, 'rb') as f:
        data = f.read()
    if data[:2] != b'MZ':
        raise SystemExit('not PE: ' + path)
    pe = struct.unpack_from('<I', data, 0x3C)[0]
    if data[pe:pe + 4] != b'PE\0\0':
        raise SystemExit('no PE sig: ' + path)
    nsec = struct.unpack_from('<H', data, pe + 6)[0]
    opt_size = struct.unpack_from('<H', data, pe + 20)[0]
    base = pe + 24 + opt_size
    out = {}
    for i in range(nsec):
        off = base + i * 40
        name = data[off:off + 8].rstrip(b'\0').decode('ascii', 'replace')
        vsize, vaddr, rawsize, rawptr = struct.unpack_from('<IIII', data, off + 8)
        out[name] = hashlib.sha256(data[rawptr:rawptr + rawsize]).hexdigest()
    return out


a, b = sys.argv[1], sys.argv[2]
sa, sb = sections(a), sections(b)
print('%-10s %-18s %-18s %s' % ('section', a.split('\\')[-1][:16], b.split('\\')[-1][:16], 'result'))
allsame = True
for name in sorted(set(sa) | set(sb)):
    ha, hb = sa.get(name, '-'), sb.get(name, '-')
    same = ha == hb
    if name == '.text' and not same:
        allsame = False
    print('%-10s %-18s %-18s %s' % (name, ha[:16], hb[:16], 'same' if same else 'DIFF'))
print()
print('.text identical:', sa.get('.text') == sb.get('.text'))
