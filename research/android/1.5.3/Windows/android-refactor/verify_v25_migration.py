from pathlib import Path
import struct, sys
sys.path.insert(0, 'tools/CustomModel')
import bem_v1 as bem
from bem_tool import check_geometry
raw=Path('artifacts/BetterEndfield-win-x64/custom-model/zhuangfangyi-default-v25.bempoc').read_bytes()
m,p=bem.read_package(Path('artifacts/android-refactor/zhuangfangyi-default.bem'))
pos=28
for mesh in m['meshes']:
    cid,orig,vc,ic,mb,sc,s0,s1,s2,iz,nb,flags,nd=struct.unpack_from('<13I',raw,pos); pos+=52
    assert (vc,ic,iz)==(mesh['vertex_count'],mesh['index_count'],mesh['index_size'])
    for stream,stride in zip(mesh['streams'],[s0,s1,s2]):
        size=vc*stride
        assert p[stream['payload']]==raw[pos:pos+size]; pos+=size
    assert p[mesh['indices']]==raw[pos:pos+ic*iz]; pos+=ic*iz
    pos+=4
    for bone in mesh['bones']:
        donor,index,pin=struct.unpack_from('<3I',raw,pos); pos+=12
        assert (donor,index,pin)==(bone['component'],bone['index'],bem.crc(bone['name']))
    assert len(mesh['draws'])==nd
    for draw in mesh['draws']:
        start,count,donor,slot,pin,mask=struct.unpack_from('<6I',raw,pos); pos+=24
        assert (start,count,donor,slot,pin,mask)==(draw['start'],draw['count'],draw['material_component'],draw['material_slot'],bem.crc(draw['material_name']),sum(1<<t for t in draw['textures']))
    print(f'C{cid}: streams, indices, {nb} bones, {nd} draw ranges/materials/textures equal PC v25')
check_geometry(m,p)
fixed,fp=bem.read_package(Path('artifacts/android-refactor/zhuangfangyi-default-normal.bem'))
check_geometry(fixed,fp)
assert fixed['meshes']==m['meshes']
for mesh in m['meshes']:
    for stream in mesh['streams']: assert p[stream['payload']]==fp[stream['payload']]
    assert p[mesh['indices']]==fp[mesh['indices']]
print('PASS: original v25 draw semantics preserved after normal adaptation')
