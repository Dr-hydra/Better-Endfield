import re
from pathlib import Path
from capstone import Cs, CS_ARCH_ARM64, CS_MODE_ARM
base=Path(__file__).parent
text=(base/'pipeline-audit-runtime.log').read_text(encoding='utf-8-sig')
md=Cs(CS_ARCH_ARM64,CS_MODE_ARM)
out=[]
roots={}
seen=set()
for m in re.finditer(r'mesh-code root=(HGRenderPipeline\.\w+) offset=(0x[0-9a-f]+) bytes=([0-9a-f]+)',text):
    name,off,raw=m.groups(); off=int(off,16)
    roots.setdefault(name,off)
    if (name,off) in seen: continue
    seen.add((name,off))
    out.append(name+' @ '+hex(off))
    for i in md.disasm(bytes.fromhex(raw),off):
        out.append(hex(i.address)+': '+i.mnemonic+' '+i.op_str)
        if i.mnemonic=='ret': break
(base/'pipeline-disassembly.txt').write_text('\n'.join(out))
print(roots)
print('windows',len(seen))
