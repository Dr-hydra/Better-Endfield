"""Read-only, bounded native Mesh disassembly; never recursively follows calls."""
from pathlib import Path
import bisect, json, re, struct, sys
sys.path = [p for p in sys.path if p != str(Path(__file__).resolve().parent)]
import pefile, capstone

ROOT = Path(__file__).resolve().parent
pe = pefile.PE(r'E:\Endfield Game\UnityPlayer.dll')
data = pe.get_memory_mapped_image()
base = pe.OPTIONAL_HEADER.ImageBase
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_64)
md.detail = True
ranges = [(e.struct.BeginAddress, e.struct.EndAddress) for e in pe.DIRECTORY_ENTRY_EXCEPTION]
ranges.sort()
starts = [x[0] for x in ranges]
names = {}
for off in range(0x19e4e40, 0x19eca29, 8):
    ptr = struct.unpack_from('<Q', data, off)[0] - base
    fn = struct.unpack_from('<Q', data, off - 0x7bf0)[0] - base
    if 0 < ptr < len(data) and 0 < fn < len(data):
        name = data[ptr:ptr+200].split(b'\0')[0].decode('ascii', errors='replace')
        names[fn] = name
for off in range(0x1a51ba0, 0x1a53278, 8):
    ptr = struct.unpack_from('<Q', data, off)[0] - base
    fn = struct.unpack_from('<Q', data, off + 0x16e0)[0] - base
    if 0 < ptr < len(data) and 0 < fn < len(data):
        names[fn] = data[ptr:ptr+220].split(b'\0')[0].decode('ascii', errors='replace')
targets = ['HasBoneWeights', 'GetBonesPerVertexValue', 'InternalSetBoneWeights',
           'SetVertexBufferParamsFromPtr', 'InternalSetVertexBufferData',
           'InternalSetIndexBufferData', 'UploadMeshDataImpl', 'set_bindposes',
           'GetAllBoneWeightsArraySize', 'MarkModified', 'Internal_Create']
selected = {r:n for r,n in names.items() if n in ['UnityEngine.Mesh::'+x for x in targets]}
selected.update({r:n for r,n in names.items() if n.endswith(('HGMeshBufferAllocator::GarbageCollect','HGGraphicsUtils::PrepareGeometryJobRenderersForRendering'))})
if len(sys.argv)>1:
    selected = {int(x,16): names.get(int(x,16),'native_'+x) for x in sys.argv[1].split(',')}
results = []
for rva, name in selected.items():
    pos=bisect.bisect_right(starts,rva)-1
    begin,end=ranges[pos]
    # Leaf entries may lack pdata. Limit to 96 bytes and stop at first ret/jmp.
    leaf=not begin<=rva<end
    if leaf: begin,end=rva,rva+96
    if end-rva>0x5000: continue
    lines=[];calls=[];queue=[rva];seen=set()
    while queue and len(seen)<1800:
        ea=queue.pop()
        if ea in seen or not rva <= ea < rva+0x5000:continue
        seen.add(ea)
        ins=next(md.disasm(data[ea:ea+15],ea,count=1),None)
        if ins is None:continue
        line=f'{ins.address:#x} {ins.mnemonic} {ins.op_str}'
        if ins.mnemonic in ('call','jmp') and ins.operands[0].type==capstone.x86.X86_OP_IMM:
            t=ins.operands[0].imm
            calls.append({'at':hex(ins.address),'to':hex(t),'name':names.get(t,'')})
            if t in names:line+=' ; '+names[t]
        lines.append((ea,line))
        if ins.mnemonic.startswith('j') and ins.operands[0].type==capstone.x86.X86_OP_IMM:
            t=ins.operands[0].imm
            ti=bisect.bisect_right(starts,t)-1
            if t==rva or t not in names:
                queue.append(t)
        if ins.mnemonic not in ('ret','jmp','int3','ud2'):
            queue.append(ea+ins.size)
    lines=[line for _,line in sorted(lines)]
    results.append({'name':name,'rva':hex(rva),'end':hex(end),'leaf':leaf,'calls':calls,'asm':lines})
out=ROOT / ('mesh-native-'+('follow' if len(sys.argv)>1 else 'entry')+'.json')
out.write_text(json.dumps(results,ensure_ascii=False,indent=2),encoding='utf-8')
archive=ROOT / ('mesh-native-'+('-'.join(hex(r)[2:] for r in selected) if len(sys.argv)>1 else 'entries')+'.json')
archive.write_text(out.read_text(encoding='utf-8'),encoding='utf-8')
for r in results:
    print(r['name'],r['rva'],'instructions',len(r['asm']),'calls',','.join(c['to'] for c in r['calls']))
print('saved',out)
