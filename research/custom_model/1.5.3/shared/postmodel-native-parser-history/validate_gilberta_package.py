"""Independent actual-package check against selected source triangles/weights."""
import json
import struct
import sys
from pathlib import Path

root=Path(__file__).resolve().parents[2]
sys.path.insert(0,str(root/'tools/CustomModel'))
from efmi_source import sections,value
from convert_efmi_poc import parse_dds

src=root/'artifacts/converter-new-mods/gilberta'
recipe=json.loads((root/'tools/CustomModel/profiles/gilberta-12.reviewed.json').read_text(encoding='utf-8'))
sec=sections((src/recipe['ini']).read_text(encoding='utf-8-sig'))
data=(Path(__file__).parent/'gilberta-default-v25.bempoc').read_bytes()
assert struct.unpack_from('<8s5I',data)==(b'BEMPC25\0',25,4,1,0,0)
offset=28
for cid,entry in enumerate(recipe['entries']):
    header=struct.unpack_from('<13I',data,offset);offset+=52
    component,original,vertices,index_count,maximum,streams,s0,s1,s2,index_bytes,palettes,flags,draw_count=header
    assert (component,original,streams,s0,s1,s2,index_bytes,flags)==(cid,entry['original_index_count'],3,16,12,12,2,0)
    vbs=[]
    for stride in (s0,s1,s2):
        vbs.append(memoryview(data)[offset:offset+vertices*stride]);offset+=vertices*stride
    indices=struct.unpack_from(f'<{index_count}H',data,offset);offset+=index_count*2
    offset+=4
    palette=[struct.unpack_from('<3I',data,offset+i*12) for i in range(palettes)];offset+=palettes*12
    draws=[struct.unpack_from('<6I',data,offset+i*24) for i in range(draw_count)];offset+=draw_count*24
    original_vbs=[(src/value(sec[entry['buffers']['vb'+str(i)]],'filename').replace('\\','/')).read_bytes() for i in range(3)]
    ib=(src/value(sec[entry['buffers']['ib']],'filename').replace('\\','/')).read_bytes()
    expected=list(entry['draws'])
    if cid==2:expected=expected*2
    assert len(expected)==len(draws)
    checked={}
    for index,(draw,source) in enumerate(zip(draws,expected)):
        start,count,donor,slot,material_crc,texture_mask=draw
        assert count==source['count']
        wanted_donor=3 if cid==3 or cid==0 and index<2 else cid
        assert donor==wanted_donor and texture_mask==(1 if wanted_donor==3 else 0)
        assert slot==(index if cid==2 else 0)
        source_indices=struct.unpack_from(f'<{count}I',ib,source['start']*4)
        for out,orig in zip(indices[start:start+count],source_indices):
            if out in checked: assert checked[out]==orig;continue
            checked[out]=orig
            assert vbs[0][out*16:(out+1)*16]==original_vbs[0][orig*16:(orig+1)*16]
            assert vbs[1][out*12:(out+1)*12]==original_vbs[1][orig*12:(orig+1)*12]
            before=struct.unpack_from('<4H4B',original_vbs[2],orig*12)
            after=struct.unpack_from('<4H4B',vbs[2],out*12)
            assert before[:4]==after[:4]
            for weight,old,new in zip(before[:4],before[4:],after[4:]):
                if weight:assert palette[new][:2]==(cid,old)
                else:assert new==0
    assert len(checked)==vertices
    print(f'C{cid}: every selected triangle, vertex stream and weighted bone matches source; donor/material mask checked')
texture=struct.unpack_from('<4I4i2I2I',data,offset);offset+=48
width,height,mips,length,fmt,srgb,_,_,mask,pin,name_len,kind=texture
offset+=name_len
dds=parse_dds('skin',(src/'Textures/Skin-DiffuseMap.dds').read_bytes())
assert (width,height,mips,fmt,srgb)==dds[:5] and mask==9 and kind==2
assert data[offset:offset+length]==dds[5] and offset+length==len(data)
print('Skin DDS payload exact; C0/C3 mask; exact EOF. No artifact hashes computed.')
